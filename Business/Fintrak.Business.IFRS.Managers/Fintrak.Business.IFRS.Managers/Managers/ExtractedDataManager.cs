using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Linq;
using System.Configuration;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.IFRS.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Data;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;
using Fintrak.Data.Core.Contracts;
using Fintrak.Shared.Common.Utils;
using Fintrak.Shared.Core.Entities;
using Fintrak.Data.IFRS;
using Fintrak.Shared.SystemCore.Entities;
using systemCoreFramework = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Framework;
using System.Data.SqlClient;

namespace Fintrak.Business.IFRS.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]

    public class ExtractedDataManager : ManagerBase, IExtractedDataService
    {
        public ExtractedDataManager()
        {
        }

        public ExtractedDataManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_IFRS";
        const string SOLUTION_ALIAS = "IFRS";
        const string MODULE_NAME = "FIN_IFRS_EXTRACTED_DATA";
        const string MODULE_ALIAS = "IFRS Extracted Data";

        const string GROUP_ADMINISTRATOR = "Administrator";
        const string GROUP_USER = "User";

        [OperationBehavior(TransactionScopeRequired = true)]
       
        public override void RegisterModule()
        {
            ExecuteFaultHandledOperation(() =>
            {
                ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();
                IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<IModuleRepository>();
                IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<IMenuRepository>();
                IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<IRoleRepository>();
                IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<IMenuRoleRepository>();

                using (TransactionScope ts = new TransactionScope())
                {
                    //check if module has been installed
                    Module module = moduleRepository.Get().Where(c => c.Name == ExtractedDataModuleDefinition.MODULE_NAME).FirstOrDefault();

                    var register = false;
                    if (module == null)
                        register = true;
                    else
                        register = module.CanUpdate;

                    if (register)
                    {
                        //check if module category exit
                        Solution solution = solutionRepository.Get().Where(c => c.Name == ExtractedDataModuleDefinition.SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new Solution()
                            {
                                Name = ExtractedDataModuleDefinition.SOLUTION_NAME,
                                Alias = ExtractedDataModuleDefinition.SOLUTION_ALIAS,
                                Active = true,
                                Deleted = false,
                                CreatedBy = "Auto",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "Auto",
                                UpdatedOn = DateTime.Now
                            };

                            solution = solutionRepository.Add(solution);
                        }

                        //register module
                        if (module == null)
                        {
                            module = new Module()
                            {
                                Name = ExtractedDataModuleDefinition.MODULE_NAME,
                                Alias = ExtractedDataModuleDefinition.MODULE_ALIAS,
                                SolutionId = solution.EntityId,
                                Active = true,
                                Deleted = false,
                                CreatedBy = "Auto",
                                CreatedOn = DateTime.Now,
                                UpdatedBy = "Auto",
                                UpdatedOn = DateTime.Now
                            };

                            module = moduleRepository.Add(module);
                        }

                        //Roles
                        var existingRoles = roleRepository.Get().Where(c => c.SolutionId == solution.SolutionId && c.Type == RoleType.Application).ToList();
                        var updatedRoles = new List<Role>();

                        foreach (var role in ExtractedDataModuleDefinition.GetRoles())
                        {
                            var localRole = existingRoles.Where(c => c.Name == role.Name).FirstOrDefault();

                            if (localRole == null)
                            {
                                localRole = new Role() { Name = role.Name, Description = role.Description, SolutionId = solution.SolutionId, Type = RoleType.Application, Active = true, Deleted = false, CreatedBy = "Auto", CreatedOn = DateTime.Now, UpdatedBy = "Auto", UpdatedOn = DateTime.Now };

                                localRole = roleRepository.Add(localRole);
                            }
                            else
                            {
                                localRole.Description = role.Description;
                                localRole.UpdatedOn = DateTime.Now;
                                localRole = roleRepository.Update(localRole);
                            }

                            updatedRoles.Add(localRole);
                        }

                        //Menus
                        var existingMenus = menuRepository.Get().Where(c => c.ModuleId == module.ModuleId).ToList();
                        var updatedMenus = new List<Menu>();

                        var menuIndex = 0;

                        foreach (var menu in ExtractedDataModuleDefinition.GetMenus())
                        {
                            menuIndex += 1;
                            Menu parentMenu = null;

                            int? parentId = null;
                            if (!string.IsNullOrEmpty(menu.Parent))
                            {
                                if (string.IsNullOrEmpty(menu.ParentModule))
                                {
                                    parentMenu = existingMenus.Where(c => c.Name == menu.Parent).FirstOrDefault();

                                    if (parentMenu == null)
                                        parentMenu = menuRepository.Get().Where(c => c.ModuleId == module.ModuleId && c.Name == menu.Parent).FirstOrDefault();  
                                }
                                else
                                {
                                    var parentModule = moduleRepository.Get().Where(c => c.Name == menu.ParentModule).FirstOrDefault();

                                    if (parentModule != null)
                                        parentMenu = menuRepository.Get().Where(c => c.ModuleId == parentModule.ModuleId && c.Name == menu.Parent).FirstOrDefault(); 
                                }

                                if (parentMenu != null)
                                    parentId = parentMenu.MenuId;
                            }

                            var localMenu = existingMenus.Where(c => c.Name == menu.Name).FirstOrDefault();

                            if (localMenu == null)
                            {
                                localMenu = new Menu() { Name = menu.Name, Alias = menu.Alias, Action = menu.Action, ActionUrl = menu.ActionUrl, ImageUrl = menu.ImageUrl, ModuleId = module.ModuleId, Position = menuIndex, ParentId = parentId, Active = true, Deleted = false, CreatedBy = "Auto", CreatedOn = DateTime.Now, UpdatedBy = "Auto", UpdatedOn = DateTime.Now };

                                localMenu = menuRepository.Add(localMenu);
                            }
                            else
                            {
                                localMenu.Alias = menu.Alias;
                                localMenu.Action = menu.Action;
                                localMenu.ActionUrl = menu.ActionUrl;
                                localMenu.ImageUrl = menu.ImageUrl;
                                localMenu.ModuleId = module.ModuleId;
                                localMenu.Position = menuIndex;
                                localMenu.ParentId = parentId;
                                localMenu.UpdatedOn = DateTime.Now;

                                localMenu = menuRepository.Update(localMenu);
                            }

                            updatedMenus.Add(localMenu);
                        }

                        //MenuRoles
                        var menuIds = updatedMenus.Select(c => c.MenuId).Distinct().ToArray();
                        var existingMenuRoles = menuRoleRepository.Get().Where(c => menuIds.Contains(c.MenuId)).ToList();

                        foreach (var menuRole in ExtractedDataModuleDefinition.GetMenuRoles())
                        {
                            var myMenu = updatedMenus.Where(c => c.Name == menuRole.MenuName).FirstOrDefault();
                            var myRole = updatedRoles.Where(c => c.Name == menuRole.RoleName).FirstOrDefault();

                            var localMenuRole = existingMenuRoles.Where(c => c.MenuId == myMenu.MenuId && c.RoleId == myRole.RoleId).FirstOrDefault();

                            if (localMenuRole == null)
                            {
                                localMenuRole = new MenuRole() { MenuId = myMenu.MenuId, RoleId = myRole.RoleId, Active = true, Deleted = false, CreatedBy = "Auto", CreatedOn = DateTime.Now, UpdatedBy = "Auto", UpdatedOn = DateTime.Now };

                                menuRoleRepository.Add(localMenuRole);
                            }
                            else
                            {
                                localMenuRole.MenuId = myMenu.MenuId;
                                localMenuRole.RoleId = myRole.RoleId;
                                localMenuRole.UpdatedOn = DateTime.Now;

                                menuRoleRepository.Update(localMenuRole);
                            }


                        }
                    }

                    ts.Complete();
                }

            });

        }

        #region IFRSBonds operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IFRSBonds UpdateIFRSBonds(IFRSBonds IFRSBonds)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSBondsRepository IFRSBondsRepository = _DataRepositoryFactory.GetDataRepository<IIFRSBondsRepository>();

                IFRSBonds updatedEntity = null;

                if (IFRSBonds.BondId == 0)
                    updatedEntity = IFRSBondsRepository.Add(IFRSBonds);
                else
                    updatedEntity = IFRSBondsRepository.Update(IFRSBonds);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIFRSBonds(int bondId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSBondsRepository IFRSBondsRepository = _DataRepositoryFactory.GetDataRepository<IIFRSBondsRepository>();

                IFRSBondsRepository.Remove(bondId);
            });
        }

        public IFRSBonds GetIFRSBonds(int bondId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSBondsRepository IFRSBondsRepository = _DataRepositoryFactory.GetDataRepository<IIFRSBondsRepository>();

                IFRSBonds IFRSBondsEntity = IFRSBondsRepository.Get(bondId);
                if (IFRSBondsEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IFRSBonds with ID of {0} is not in database", bondId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IFRSBondsEntity;
            });
        }


        public IFRSBonds[] GetAllIFRSBonds()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSBondsRepository ifrsBondRepository = _DataRepositoryFactory.GetDataRepository<IIFRSBondsRepository>();

                IEnumerable<IFRSBonds> ifrsBonds = ifrsBondRepository.Get().ToArray();

                return ifrsBonds.ToArray();
            });
        }


        public IFRSBonds[] GetBondsByClassification(string classification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSBondsRepository ifrsBondRepository = _DataRepositoryFactory.GetDataRepository<IIFRSBondsRepository>();

                IEnumerable<IFRSBonds> ifrsBonds = ifrsBondRepository.Get().Where(c => c.Classification == classification).ToArray();

                return ifrsBonds.ToArray();
            });
        }

        public IFRSBonds[] GetbondsByMaturityDate(DateTime matureDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSBondsRepository ifrsBondRepository = _DataRepositoryFactory.GetDataRepository<IIFRSBondsRepository>();

                IEnumerable<IFRSBonds> ifrsBonds = ifrsBondRepository.Get().Where(c => c.MaturityDate == matureDate).ToArray();

                return ifrsBonds.ToArray();
            });
        }

        public void UpdatebondsByMaturityDate(DateTime matureDate, decimal cmprice)
        {

            var connectionString = GetDataConnection();

            int status = 0;
          
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_market_yield_update", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaturityDate",
                    Value = matureDate,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MarketYield",
                    Value = cmprice,
                });

                con.Open();
                
               status= cmd.ExecuteNonQuery();
                
                con.Close();
            }

           
        }

        #endregion

        #region IFRSTbills operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IFRSTbills UpdateIFRSTbills(IFRSTbills IFRSTbills)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSTbillsRepository IFRSTbillsRepository = _DataRepositoryFactory.GetDataRepository<IIFRSTbillsRepository>();

                IFRSTbills updatedEntity = null;

                if (IFRSTbills.TbillId == 0)
                    updatedEntity = IFRSTbillsRepository.Add(IFRSTbills);
                else
                    updatedEntity = IFRSTbillsRepository.Update(IFRSTbills);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIFRSTbills(int tbillId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSTbillsRepository IFRSTbillsRepository = _DataRepositoryFactory.GetDataRepository<IIFRSTbillsRepository>();

                IFRSTbillsRepository.Remove(tbillId);
            });
        }

        public IFRSTbills GetIFRSTbills(int tbillId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSTbillsRepository IFRSTbillsRepository = _DataRepositoryFactory.GetDataRepository<IIFRSTbillsRepository>();

                IFRSTbills IFRSTbillsEntity = IFRSTbillsRepository.Get(tbillId);
                if (IFRSTbillsEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IFRSTbills with ID of {0} is not in database", tbillId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return IFRSTbillsEntity;
            });
        }


        public IFRSTbills[] GetAllIFRSTbills()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSTbillsRepository ifrsTbillRepository = _DataRepositoryFactory.GetDataRepository<IIFRSTbillsRepository>();

                IEnumerable<IFRSTbills> ifrsTbills = ifrsTbillRepository.Get().ToArray();

                return ifrsTbills.ToArray();
            });
        }


        public IFRSTbills[] GetTbillsByClassification(string classification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSTbillsRepository ifrsTbillRepository = _DataRepositoryFactory.GetDataRepository<IIFRSTbillsRepository>();

                IEnumerable<IFRSTbills> ifrsTbills = ifrsTbillRepository.Get().Where(c => c.Classification == classification).ToArray();

                return ifrsTbills.ToArray();
            });
        }
        public IFRSTbills[] GetTbillsByMaturityDate(DateTime matureDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSTbillsRepository ifrsTbillRepository = _DataRepositoryFactory.GetDataRepository<IIFRSTbillsRepository>();

                IEnumerable<IFRSTbills> ifrsTbills = ifrsTbillRepository.Get().Where(c => c.MaturityDate == matureDate).ToArray();

                return ifrsTbills.ToArray();
            });
        }

        public void UpdateTbillsByMaturityDate(DateTime matureDate, decimal cmprice)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_market_yield_update_tbill", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MaturityDate",
                    Value = matureDate,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "MarketYield",
                    Value = cmprice,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }
        
        #endregion

        #region LoanPry operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanPry UpdateLoanPry(LoanPry loanPry)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPryRepository loanPryRepository = _DataRepositoryFactory.GetDataRepository<ILoanPryRepository>();

                LoanPry updatedEntity = null;

                if (loanPry.PryId == 0)
                    updatedEntity = loanPryRepository.Add(loanPry);
                else
                    updatedEntity = loanPryRepository.Update(loanPry);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanPry(int pryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPryRepository loanPryRepository = _DataRepositoryFactory.GetDataRepository<ILoanPryRepository>();

                loanPryRepository.Remove(pryId);
            });
        }

        public LoanPry GetLoanPry(int pryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPryRepository loanPryRepository = _DataRepositoryFactory.GetDataRepository<ILoanPryRepository>();

                LoanPry loanPryEntity = loanPryRepository.Get(pryId);
                if (loanPryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanPry with ID of {0} is not in database", pryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanPryEntity;
            });
        }

        public LoanPryData[] GetAllLoanPry()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoanPryRepository loanPryRepository = _DataRepositoryFactory.GetDataRepository<ILoanPryRepository>();
                List<LoanPryData> loanPryd = new List<LoanPryData>();
                IEnumerable<LoanPryInfo> loanPryInfos = loanPryRepository.GetLoanPrys().ToArray();

                foreach (var loanPryInfo in loanPryInfos)
                {
                    loanPryd.Add(
                        new LoanPryData
                        {
                            PryId=loanPryInfo.LoanPry.EntityId,
                            RefNo = loanPryInfo.LoanPry.RefNo,
                            AccountNo = loanPryInfo.LoanPry.AccountNo,
                            Amount = loanPryInfo.LoanPry.Amount,
                            ProductCode = loanPryInfo.LoanPry.ProductCode,
                            ProductName = loanPryInfo.LoanPry.ProductName,
                            Currency = loanPryInfo.LoanPry.Currency,
                            ExchangeRate = loanPryInfo.LoanPry.ExchangeRate,
                            FirstRepaymentdate = loanPryInfo.LoanPry.FirstRepaymentdate,
                            InterestFirstRepayDate = loanPryInfo.LoanPry.InterestFirstRepayDate,
                            PrincipalRepayFreq=loanPryInfo.LoanPry.PrincipalRepayFreq,
                            InterestRepayFreq = loanPryInfo.LoanPry.InterestRepayFreq,
                            MaturityDate = loanPryInfo.LoanPry.MaturityDate,
                            Rate = loanPryInfo.LoanPry.Rate,
                            Schedule_Type = loanPryInfo.ScheduleType.Code,
                            ScheduleName = loanPryInfo.ScheduleType.Name,
                            ValueDate = loanPryInfo.LoanPry.ValueDate,
                            Active = loanPryInfo.LoanPry.Active
                        });
                }

                return loanPryd.ToArray();
            });
        }

        public LoanPry[] GetLoanPryByScheduleType(string schType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPryRepository loanPryRepository = _DataRepositoryFactory.GetDataRepository<ILoanPryRepository>();

                IEnumerable<LoanPry> loanPrys = loanPryRepository.Get().Where(c => c.Schedule_Type == schType).ToArray();

                return loanPrys.ToArray();
            });
        }

        #endregion

        #region LoanPryMoratorium operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanPryMoratorium UpdateLoanPryMoratorium(LoanPryMoratorium loanPryMoratorium)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPryMoratoriumRepository loanPryMoratoriumRepository = _DataRepositoryFactory.GetDataRepository<ILoanPryMoratoriumRepository>();

                LoanPryMoratorium updatedEntity = null;

                if (loanPryMoratorium.LoanPryMoratoriumId == 0)
                    updatedEntity = loanPryMoratoriumRepository.Add(loanPryMoratorium);
                else
                    updatedEntity = loanPryMoratoriumRepository.Update(loanPryMoratorium);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanPryMoratorium(int loanPryMoratoriumId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPryMoratoriumRepository loanPryMoratoriumRepository = _DataRepositoryFactory.GetDataRepository<ILoanPryMoratoriumRepository>();

                loanPryMoratoriumRepository.Remove(loanPryMoratoriumId);
            });
        }

        public LoanPryMoratorium GetLoanPryMoratorium(int loanPryMoratoriumId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanPryMoratoriumRepository loanPryMoratoriumRepository = _DataRepositoryFactory.GetDataRepository<ILoanPryMoratoriumRepository>();

                LoanPryMoratorium loanPryMoratoriumEntity = loanPryMoratoriumRepository.Get(loanPryMoratoriumId);
                if (loanPryMoratoriumEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanPryMoratorium with ID of {0} is not in database", loanPryMoratoriumId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanPryMoratoriumEntity;
            });
        }

        public LoanPryMoratorium[] GetAllLoanPryMoratorium()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                ILoanPryMoratoriumRepository loanPryMoratoriumRepository = _DataRepositoryFactory.GetDataRepository<ILoanPryMoratoriumRepository>();
                
                IEnumerable<LoanPryMoratorium> loanPryMoratorium = loanPryMoratoriumRepository.Get().ToArray();                    

                return loanPryMoratorium.ToArray();
            });
        }

       

        #endregion

        #region RawLoanDetails operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RawLoanDetails UpdateRawLoanDetails(RawLoanDetails loanDetails)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRawLoanDetailsRepository loanDetailsRepository = _DataRepositoryFactory.GetDataRepository<IRawLoanDetailsRepository>();

                RawLoanDetails updatedEntity = null;

                if (loanDetails.LoanDetailId == 0)
                    updatedEntity = loanDetailsRepository.Add(loanDetails);
                else
                    updatedEntity = loanDetailsRepository.Update(loanDetails);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRawLoanDetails(int loanDetailId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRawLoanDetailsRepository loanDetailsRepository = _DataRepositoryFactory.GetDataRepository<IRawLoanDetailsRepository>();

                loanDetailsRepository.Remove(loanDetailId);
            });
        }

        public RawLoanDetails GetRawLoanDetails(int loanDetailId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRawLoanDetailsRepository loanDetailsRepository = _DataRepositoryFactory.GetDataRepository<IRawLoanDetailsRepository>();

                RawLoanDetails loanDetailsEntity = loanDetailsRepository.Get(loanDetailId);
                if (loanDetailsEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("RawLoanDetails with ID of {0} is not in database", loanDetailId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanDetailsEntity;
            });
        }

        public RawLoanDetails[] GetAllRawLoanDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRawLoanDetailsRepository loandetailsRepository = _DataRepositoryFactory.GetDataRepository<IRawLoanDetailsRepository>();

                IEnumerable<RawLoanDetails> loandetails = loandetailsRepository.Get().ToArray();
                               
                return loandetails.ToArray();
            });
        }


        public void UpdateLoanClassNotch(string refNo, string rating, int stage)
        {

            var connectionString = GetDataConnection();

            int status = 0;
            if (rating == null)
            {
                rating = "CCC";
            }
            if (stage == null)
            {
                stage = 1;
            }
               
            else
                

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("ifrs_spp_Update_classification_notch", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Refno",
                    Value = refNo,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Rating",
                    Value = rating,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Stage",
                    Value = stage,
                });
                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        #endregion

        #region IntegralFee operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IntegralFee UpdateIntegralFee(IntegralFee integralFee)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIntegralFeeRepository integralFeeRepository = _DataRepositoryFactory.GetDataRepository<IIntegralFeeRepository>();

                IntegralFee updatedEntity = null;

                if (integralFee.IntegralFeeId == 0)
                    updatedEntity = integralFeeRepository.Add(integralFee);
                else
                    updatedEntity = integralFeeRepository.Update(integralFee);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIntegralFee(int integralFeeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIntegralFeeRepository integralFeeRepository = _DataRepositoryFactory.GetDataRepository<IIntegralFeeRepository>();

                integralFeeRepository.Remove(integralFeeId);
            });
        }

        public IntegralFee GetIntegralFee(int integralFeeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIntegralFeeRepository integralFeeRepository = _DataRepositoryFactory.GetDataRepository<IIntegralFeeRepository>();

                IntegralFee integralFeeEntity = integralFeeRepository.Get(integralFeeId);
                if (integralFeeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IntegralFee with ID of {0} is not in database", integralFeeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return integralFeeEntity;
            });
        }


        public IntegralFee[] GetAllIntegralFee()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIntegralFeeRepository integralFeeRepository = _DataRepositoryFactory.GetDataRepository<IIntegralFeeRepository>();

                IEnumerable<IntegralFee> integralFees = integralFeeRepository.Get().ToArray();

                return integralFees.ToArray();
            });
        }

        #endregion

        #region IfrsCustomer operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsCustomer UpdateIfrsCustomer(IfrsCustomer ifrsCustomer)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerRepository ifrsCustomerRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerRepository>();

                IfrsCustomer updatedEntity = null;

                if (ifrsCustomer.CustomerId == 0)
                    updatedEntity = ifrsCustomerRepository.Add(ifrsCustomer);
                else
                    updatedEntity = ifrsCustomerRepository.Update(ifrsCustomer);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsCustomer(int customerId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerRepository ifrsCustomerRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerRepository>();

                ifrsCustomerRepository.Remove(customerId);
            });
        }

        public IfrsCustomer GetIfrsCustomer(int ifrsCustomerId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerRepository ifrsCustomerRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerRepository>();

                IfrsCustomer ifrsCustomerEntity = ifrsCustomerRepository.Get(ifrsCustomerId);
                if (ifrsCustomerEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsCustomer with ID of {0} is not in database", ifrsCustomerId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsCustomerEntity;
            });
        }


        public IfrsCustomer[] GetAllIfrsCustomer()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerRepository ifrsCustomerRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerRepository>();

                IEnumerable<IfrsCustomer> ifrsCustomers = ifrsCustomerRepository.Get().ToArray();

                return ifrsCustomers.ToArray();
            });
        }


        public IfrsCustomer[] GetIfrsCustomerByRating(string rating)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerRepository ifrsCustomerRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerRepository>();

                IEnumerable<IfrsCustomer> ifrsCustomers = ifrsCustomerRepository.Get().Where(c=> c.CreditRating == rating).ToArray();

                return ifrsCustomers.ToArray();
            });
        }
        #endregion

        #region IfrsCustomerAccount operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsCustomerAccount UpdateIfrsCustomerAccount(IfrsCustomerAccount ifrsCustomerAccount)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerAccountRepository ifrsCustomerAccountRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerAccountRepository>();

                IfrsCustomerAccount updatedEntity = null;

                if (ifrsCustomerAccount.CustAccountId == 0)
                    updatedEntity = ifrsCustomerAccountRepository.Add(ifrsCustomerAccount);
                else
                    updatedEntity = ifrsCustomerAccountRepository.Update(ifrsCustomerAccount);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsCustomerAccount(int custAccountId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerAccountRepository ifrsCustomerAccountRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerAccountRepository>();

                ifrsCustomerAccountRepository.Remove(custAccountId);
            });
        }

        public IfrsCustomerAccount GetIfrsCustomerAccount(int custAccountId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerAccountRepository ifrsCustomerAccountRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerAccountRepository>();

                IfrsCustomerAccount ifrsCustomerAccountEntity = ifrsCustomerAccountRepository.Get(custAccountId);
                if (ifrsCustomerAccountEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsCustomerAccount with ID of {0} is not in database", custAccountId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsCustomerAccountEntity;
            });
        }


        public IfrsCustomerAccount[] GetAllIfrsCustomerAccount()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerAccountRepository ifrsCustomerAccountRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerAccountRepository>();

                IEnumerable<IfrsCustomerAccount> ifrsCustomerAccounts = ifrsCustomerAccountRepository.Get().ToArray();

                return ifrsCustomerAccounts.ToArray();
            });
        }

        public string[] GetDistinctSector()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsCustomerAccountRepository customerAccountRepository = _DataRepositoryFactory.GetDataRepository<IIfrsCustomerAccountRepository>();

                IEnumerable<string> listOfsector = customerAccountRepository.GetDistinctSector();

                return listOfsector.ToArray();
            });
        }
        #endregion
        

        #region UnMappedProducts operations

        public UnMappedProduct[] GetAllUnMappedProducts()
        {

            var connectionString = GetDataConnection();

            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var unMappedProducts = new List<UnMappedProduct>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_check_ifrs_product", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var unMappedProduct = new UnMappedProduct();

                    if (reader["ProductCode"] != DBNull.Value)
                        unMappedProduct.ProductCode = reader["ProductCode"].ToString();

                    if (reader["ProductName"] != DBNull.Value)
                        unMappedProduct.ProductName = reader["ProductName"].ToString();

                    if (reader["InGlobalProduct"] != DBNull.Value)
                        unMappedProduct.InGlobalProduct = bool.Parse(reader["InGlobalProduct"].ToString());

                    if (reader["InIFRSProduct"] != DBNull.Value)
                        unMappedProduct.InIFRSProduct = bool.Parse(reader["InIFRSProduct"].ToString());
                    
                    unMappedProducts.Add(unMappedProduct);
                }

                con.Close();
            }

            return unMappedProducts.ToArray();
        }



        #endregion

        #region Borrowings operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Borrowings UpdateBorrowings(Borrowings borrowing)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBorrowingsRepository borrowingRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingsRepository>();

                Borrowings updatedEntity = null;

                if (borrowing.BorrowingId == 0)
                    updatedEntity = borrowingRepository.Add(borrowing);
                else
                    updatedEntity = borrowingRepository.Update(borrowing);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBorrowings(int borrowingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBorrowingsRepository borrowingRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingsRepository>();

                borrowingRepository.Remove(borrowingId);
            });
        }

        public Borrowings GetBorrowings(int borrowingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBorrowingsRepository borrowingRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingsRepository>();

                Borrowings borrowingEntity = borrowingRepository.Get(borrowingId);
                if (borrowingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Borrowings with ID of {0} is not in database", borrowingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return borrowingEntity;
            });
        }

        public Borrowings[] GetAllBorrowings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);
                IBorrowingsRepository borrowingRepository = _DataRepositoryFactory.GetDataRepository<IBorrowingsRepository>();
        
                IEnumerable<Borrowings> borrowings = borrowingRepository.Get().ToArray();               

                return borrowings.ToArray();
            });
        }

       

        #endregion

        #region OffBalanceSheetExposure operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public OffBalanceSheetExposure UpdateOffBalanceSheetExposure(OffBalanceSheetExposure OffBalanceSheetExposure)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOffBalanceSheetExposureRepository offBalanceSheetExposureRepository = _DataRepositoryFactory.GetDataRepository<IOffBalanceSheetExposureRepository>();

                OffBalanceSheetExposure updatedEntity = null;

                if (OffBalanceSheetExposure.ObeId == 0)
                    updatedEntity = offBalanceSheetExposureRepository.Add(OffBalanceSheetExposure);
                else
                    updatedEntity = offBalanceSheetExposureRepository.Update(OffBalanceSheetExposure);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOffBalanceSheetExposure(int obeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOffBalanceSheetExposureRepository offBalanceSheetExposureRepository = _DataRepositoryFactory.GetDataRepository<IOffBalanceSheetExposureRepository>();

                offBalanceSheetExposureRepository.Remove(obeId);
            });
        }

        public OffBalanceSheetExposure GetOffBalanceSheetExposure(int obeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOffBalanceSheetExposureRepository offBalanceSheetExposureRepository = _DataRepositoryFactory.GetDataRepository<IOffBalanceSheetExposureRepository>();


                OffBalanceSheetExposure offBalanceSheetExposureEntity = offBalanceSheetExposureRepository.Get(obeId);
                if (offBalanceSheetExposureEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OffBalanceSheetExposure with ID of {0} is not in database", obeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return offBalanceSheetExposureEntity;
            });
        }


        public OffBalanceSheetExposure[] GetAllOffBalanceSheetExposure()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOffBalanceSheetExposureRepository offBalanceSheetExposureRepository = _DataRepositoryFactory.GetDataRepository<IOffBalanceSheetExposureRepository>();

                IEnumerable<OffBalanceSheetExposure> offBalanceSheetExposure = offBalanceSheetExposureRepository.Get().ToArray();

                return offBalanceSheetExposure.ToArray();
            });
        }


        public OffBalanceSheetExposure[] GetOffBalanceSheetExposureByPortfolio(string portfolio)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOffBalanceSheetExposureRepository offBalanceSheetExposureRepository = _DataRepositoryFactory.GetDataRepository<IOffBalanceSheetExposureRepository>();

                IEnumerable<OffBalanceSheetExposure> offBalanceSheetExposure = offBalanceSheetExposureRepository.Get().Where(c => c.Portfolio == portfolio).ToArray();

                return offBalanceSheetExposure.ToArray();
            });
        }




        #endregion

        #region Placement operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Placement UpdatePlacement(Placement placement)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementRepository placementRepository = _DataRepositoryFactory.GetDataRepository<IPlacementRepository>();

                Placement updatedEntity = null;

                if (placement.Placement_Id == 0)
                    updatedEntity = placementRepository.Add(placement);
                else
                    updatedEntity = placementRepository.Update(placement);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePlacement(int Placement_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementRepository placementRepository = _DataRepositoryFactory.GetDataRepository<IPlacementRepository>();

                placementRepository.Remove(Placement_Id);
            });
        }

        public Placement GetPlacement(int Placement_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementRepository placementRepository = _DataRepositoryFactory.GetDataRepository<IPlacementRepository>();

                Placement placementEntity = placementRepository.Get(Placement_Id);
                if (placementEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Placement with ID of {0} is not in database", Placement_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return placementEntity;
            });
        }

        public Placement[] GetAllPlacements()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementRepository placementRepository = _DataRepositoryFactory.GetDataRepository<IPlacementRepository>();

                IEnumerable<Placement> placements = placementRepository.Get().ToArray();

                return placements.ToArray();
            });
        }

        //public Placement[] GetPlacementByRefNo(string RefNo)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IPlacementRepository placementRepository = _DataRepositoryFactory.GetDataRepository<IPlacementRepository>();

        //        return placementRepository.GetPlacementByRefNo(RefNo).ToArray();
        //    });
        //}



        #endregion

        #region LoanInterestRate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanInterestRate UpdateLoanInterestRate(LoanInterestRate loanInterestRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanInterestRateRepository loanInterestRateRepository = _DataRepositoryFactory.GetDataRepository<ILoanInterestRateRepository>();

                LoanInterestRate updatedEntity = null;

                if (loanInterestRate.LoanInterestRate_Id == 0)
                    updatedEntity = loanInterestRateRepository.Add(loanInterestRate);
                else
                    updatedEntity = loanInterestRateRepository.Update(loanInterestRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanInterestRate(int LoanInterestRate_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanInterestRateRepository loanInterestRateRepository = _DataRepositoryFactory.GetDataRepository<ILoanInterestRateRepository>();

                loanInterestRateRepository.Remove(LoanInterestRate_Id);
            });
        }

        public LoanInterestRate GetLoanInterestRate(int LoanInterestRate_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanInterestRateRepository loanInterestRateRepository = _DataRepositoryFactory.GetDataRepository<ILoanInterestRateRepository>();

                LoanInterestRate loanInterestRateEntity = loanInterestRateRepository.Get(LoanInterestRate_Id);
                if (loanInterestRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanInterestRate with ID of {0} is not in database", LoanInterestRate_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanInterestRateEntity;
            });
        }

        public LoanInterestRate[] GetAllLoanInterestRates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanInterestRateRepository loanInterestRateRepository = _DataRepositoryFactory.GetDataRepository<ILoanInterestRateRepository>();

                IEnumerable<LoanInterestRate> loanInterestRates = loanInterestRateRepository.Get();

                return loanInterestRates.ToArray();
            });
        }

        //public LoanInterestRate[] GetLoanInterestRateByRefNo(string RefNo)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        ILoanInterestRateRepository loanInterestRateRepository = _DataRepositoryFactory.GetDataRepository<ILoanInterestRateRepository>();

        //        return loanInterestRateRepository.GetLoanInterestRateByRefNo(RefNo).ToArray();
        //    });
        //}



        #endregion

        #region Helper

        protected override bool AllowAccessToOperation(string solutionName, List<string> groupNames)
        {
            systemCoreData.IUserRoleRepository accountRoleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IUserRoleRepository>();
            var accountRoles = accountRoleRepository.GetUserRoleInfo(solutionName, _LoginName, groupNames);

            if (accountRoles == null || accountRoles.Count() <= 0)
            {
                AuthorizationValidationException ex = new AuthorizationValidationException(string.Format("Access denied for {0}.", _LoginName));
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }

            return true;
        }

        public string GetDataConnection()
        {
            string connectionString = "";

            if (!string.IsNullOrEmpty(DataConnector.CompanyCode))
            {
                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();
                var companydb = databaseRepository.Get().Where(c => c.CompanyCode == DataConnector.CompanyCode).FirstOrDefault();

                if (companydb == null)
                    throw new Exception("Unable to load company database.");

                connectionString = string.Format("Data Source= {0};Initial Catalog={1};User ={2};Password={3};Integrated Security={4}", companydb.ServerName, companydb.DatabaseName, companydb.UserName, companydb.Password, companydb.IntegratedSecurity);
            }

            return connectionString;
        }


        #endregion

    }
}
