using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using Fintrak.Data.MPR.Contracts;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Business.MPR.Contracts;
using Fintrak.Shared.Common;
using Fintrak.Shared.Common.Data;
using Fintrak.Shared.Common.Exceptions;
using Fintrak.Shared.Common.ServiceModel;
using Fintrak.Shared.MPR.Entities;
using systemCoreFramework = Fintrak.Shared.SystemCore.Framework;
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Presentation.WebClient.Models;
using Fintrak.Shared.MPR.Framework;

namespace Fintrak.Business.MPR.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class MPRIncomeManager : ManagerBase, IMPRIncomeService
    {
        public MPRIncomeManager()
        {
        }

        public MPRIncomeManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        /// <summary>
        /// </summary>
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_MPR";
        const string SOLUTION_ALIAS = "MPR";
        const string MODULE_NAME = "FIN_MPR_CORE";
        const string MODULE_ALIAS = "MPR CORE";

        const string GROUP_ADMINISTRATOR = "Administrator";
        const string GROUP_USER = "User";
        const string GROUP_SUPER_BUSINESS = "Super Business";
        const string GROUP_USER_ADMINISTRATOR = "Access Maintenance";

        [OperationBehavior(TransactionScopeRequired = true)]
        public override void RegisterModule()
        {
            ExecuteFaultHandledOperation(() =>
            {
                systemCoreData.ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.ISolutionRepository>();
                systemCoreData.IModuleRepository moduleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IModuleRepository>();
                systemCoreData.IMenuRepository menuRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IMenuRepository>();
                systemCoreData.IRoleRepository roleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IRoleRepository>();
                systemCoreData.IMenuRoleRepository menuRoleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IMenuRoleRepository>();

                using (TransactionScope ts = new TransactionScope())
                {
                    //check if module has been installed
                    systemCoreEntities.Module module = moduleRepository.Get().Where(c => c.Name == MODULE_NAME).FirstOrDefault();
                    if (module == null)
                    {
                        //check if module category exit
                        systemCoreEntities.Solution solution = solutionRepository.Get().Where(c => c.Name == SOLUTION_NAME).FirstOrDefault();
                        if (solution == null)
                        {
                            //register solution
                            solution = new systemCoreEntities.Solution()
                            {
                                Name = SOLUTION_NAME,
                                Alias = SOLUTION_ALIAS,
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
                        module = new systemCoreEntities.Module()
                        {
                            Name = MODULE_NAME,
                            Alias = MODULE_ALIAS,
                            SolutionId = solution.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        module = moduleRepository.Add(module);

                        //Role
                        var adminRole = new systemCoreEntities.Role()
                        {
                            SolutionId = solution.SolutionId,
                            Name = GROUP_ADMINISTRATOR,
                            Description = "For MPR solution unlimited users",
                            Type = systemCoreFramework.RoleType.Application,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        roleRepository.Add(adminRole);

                        var userRole = new systemCoreEntities.Role()
                        {
                            SolutionId = solution.SolutionId,
                            Name = GROUP_USER,
                            Description = "For MPR solution limited users",
                            Type = systemCoreFramework.RoleType.Application,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        roleRepository.Add(userRole);


                        var menuIndex = 0;

                        //register menu
                        var root = new systemCoreEntities.Menu()
                        {
                            Name = "MPR",
                            Alias = "MPR",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "mpr_image",
                            ModuleId = module.EntityId,
                            ParentId = null,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        root = menuRepository.Add(root);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = root.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = root.EntityId,
                            RoleId = userRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var teamManager = new systemCoreEntities.Menu()
                        {
                            Name = "TEAM_MANAGER",
                            Alias = "Team Manager",
                            Action = "",
                            ActionUrl = "",
                            Image = null,
                            ImageUrl = "team_manager_image",
                            ModuleId = module.EntityId,
                            ParentId = root.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        teamManager = menuRepository.Add(teamManager);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = teamManager.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        var actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CLASSIFICATION_TYPE",
                            Alias = "Classification Types",
                            Action = "CLASSIFICATION_TYPE",
                            ActionUrl = "mpr-teamclassificationtype-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CLASSIFICATION",
                            Alias = "Classification",
                            Action = "CLASSIFICATION",
                            ActionUrl = "mpr-teamclassification-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "TEAM_DEFINITION",
                            Alias = "Team Definition",
                            Action = "TEAM_DEFINITION",
                            ActionUrl = "mpr-teamdefinition-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "TEAMS",
                            Alias = "Teams",
                            Action = "TEAMS",
                            ActionUrl = "mpr-team-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "OFFICERS_PROFILE",
                            Alias = "Officer's Profile",
                            Action = "OFFICERS_PROFILE",
                            ActionUrl = "mpr-accountofficerdetail-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "USER_MIS",
                            Alias = "User's MIS",
                            Action = "USER_MIS",
                            ActionUrl = "mpr-usermis-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "BRANCH_DEFAULT_MIS",
                            Alias = "Branch Default MIS",
                            Action = "BRANCH_DEFAULT_MIS",
                            ActionUrl = "mpr-branchdefaultmis-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "MANAGEMENT_TREE",
                            Alias = "Management Tree",
                            Action = "MANAGEMENT_TREE",
                            ActionUrl = "mpr-managementtree-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "ACCOUNT_MIS",
                            Alias = "Account MIS",
                            Action = "ACCOUNT_MIS",
                            ActionUrl = "mpr-accountmis-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "MIS_REPLACEMENT",
                            Alias = "MIS Replacement",
                            Action = "MIS_REPLACEMENT",
                            ActionUrl = "mpr-misreplacement-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "MPR_SETUP",
                            Alias = "General Settings",
                            Action = "MPR_SETUP",
                            ActionUrl = "mpr-mprsetup-edit",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "CUSTOMER_ACCOUNT",
                            Alias = "Customer Account",
                            Action = "CUSTOMER_ACCOUNT",
                            ActionUrl = "mpr-custaccount-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });
                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "MEMO_UNITS",
                            Alias = "Memo Units",
                            Action = "MEMO_UNITS",
                            ActionUrl = "mpr-memounit-list",
                            Image = null,
                            ImageUrl = "action_image",
                            ModuleId = module.EntityId,
                            ParentId = teamManager.EntityId,
                            Position = menuIndex += 1,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        };

                        menuRepository.Add(actionMenu);

                        menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        {
                            MenuId = actionMenu.EntityId,
                            RoleId = adminRole.EntityId,
                            Active = true,
                            Deleted = false,
                            CreatedBy = "Auto",
                            CreatedOn = DateTime.Now,
                            UpdatedBy = "Auto",
                            UpdatedOn = DateTime.Now
                        });
                    }

                    ts.Complete();
                }

            });

        }



        #region Income Product

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeProductsTable Updateincomeproducttable(IncomeProductsTable incomeproducttable)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableRepository iptRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableRepository>();

                IncomeProductsTable updatedEntity = null;

                if (incomeproducttable.ProductID == 0)
                {
                    updatedEntity = iptRepository.Add(incomeproducttable);
                }
                else
                    updatedEntity = iptRepository.Update(incomeproducttable);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Deleteincomeproducttable(int productid)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableRepository iptRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableRepository>();

                iptRepository.Remove(productid);
            });
        }

        public IncomeProductsTable Getincomeproducttable(int productid)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableRepository iptRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableRepository>();

                IncomeProductsTable iptEntity = iptRepository.Get(productid);
                if (iptEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeProduct with ProductId of {0} is not in database", productid));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return iptEntity;
            });
        }
        public IncomeProductsTable[] GetAllincomeproducttable()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableRepository iptRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableRepository>();

                IEnumerable<IncomeProductsTable> ipt = iptRepository.Get();

                return ipt.ToArray();
            });
        }


        public IncomeProductsTable[] GetincomeproducttableUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableRepository iptRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableRepository>();

                IEnumerable<IncomeProductsTable> ipt = iptRepository.GetIncomeProductBySearchValue(searchvalue);

               

                return ipt.ToArray();
            });
        }

        #endregion

        #region Caption

        [OperationBehavior(TransactionScopeRequired = true)]
        public Caption UpdateCaption(Caption caption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaptionRepository captionRepository = _DataRepositoryFactory.GetDataRepository<ICaptionRepository>();

                Caption updatedEntity = null;

                if (caption.CaptionId == 0)
                {
                    updatedEntity = captionRepository.Add(caption);
                }
                else
                    updatedEntity = captionRepository.Update(caption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCaption(int CaptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaptionRepository captionRepository = _DataRepositoryFactory.GetDataRepository<ICaptionRepository>();

                captionRepository.Remove(CaptionId);
            });
        }

        public Caption GetCaption(int CaptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaptionRepository captionRepository = _DataRepositoryFactory.GetDataRepository<ICaptionRepository>();

                Caption captionEntity = captionRepository.Get(CaptionId);
                if (captionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Caption with CaptionId of {0} is not in database", CaptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return captionEntity;
            });
        }
        public Caption[] GetAllCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaptionRepository captionRepository = _DataRepositoryFactory.GetDataRepository<ICaptionRepository>();

                IEnumerable<Caption> captions = captionRepository.Get();

                return captions.ToArray();
            });
        }

        #endregion

        #region PLCaption2

        [OperationBehavior(TransactionScopeRequired = true)]
        public PLCaption2 UpdatePLCaption2(PLCaption2 plcaption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaption2Repository plcaption2Repository = _DataRepositoryFactory.GetDataRepository<IPLCaption2Repository>();

                PLCaption2 updatedEntity = null;

                if (plcaption.PL_CaptionId == 0)
                {
                    updatedEntity = plcaption2Repository.Add(plcaption);
                }
                else
                    updatedEntity = plcaption2Repository.Update(plcaption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePLCaption2(int PLCaptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaption2Repository plcaption2Repository = _DataRepositoryFactory.GetDataRepository<IPLCaption2Repository>();

                plcaption2Repository.Remove(PLCaptionId);
            });
        }

        public PLCaption2 GetPLCaption2(int PLCaptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaption2Repository plcaption2Repository = _DataRepositoryFactory.GetDataRepository<IPLCaption2Repository>();

                PLCaption2 plcaptionEntity = plcaption2Repository.Get(PLCaptionId);
                if (plcaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PLCaption with CaptionId of {0} is not in database", PLCaptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return plcaptionEntity;
            });
        }
        public PLCaption2[] GetAllPLCaption2()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPLCaption2Repository plcaption2Repository = _DataRepositoryFactory.GetDataRepository<IPLCaption2Repository>();

                IEnumerable<PLCaption2> plcaptions = plcaption2Repository.Get();

                return plcaptions.ToArray();
            });
        }

        #endregion

        #region PPRCaption

        [OperationBehavior(TransactionScopeRequired = true)]
        public PPRCaption UpdatePPRCaption(PPRCaption pprcaption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPPRCaptionRepository pprcaptionRepository = _DataRepositoryFactory.GetDataRepository<IPPRCaptionRepository>();

                PPRCaption updatedEntity = null;

                if (pprcaption.PPR_CaptionId == 0)
                {
                    updatedEntity = pprcaptionRepository.Add(pprcaption);
                }
                else
                    updatedEntity = pprcaptionRepository.Update(pprcaption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePPRCaption(int PPRCaptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPPRCaptionRepository pprcaptionRepository = _DataRepositoryFactory.GetDataRepository<IPPRCaptionRepository>();

                pprcaptionRepository.Remove(PPRCaptionId);
            });
        }

        public PPRCaption GetPPRCaption(int PPRCaptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPPRCaptionRepository pprcaptionRepository = _DataRepositoryFactory.GetDataRepository<IPPRCaptionRepository>();

                PPRCaption pprcaptionEntity = pprcaptionRepository.Get(PPRCaptionId);
                if (pprcaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PPRCaption with CaptionId of {0} is not in database", PPRCaptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return pprcaptionEntity;
            });
        }
        public PPRCaption[] GetAllPPRCaption()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPPRCaptionRepository pprcaptionRepository = _DataRepositoryFactory.GetDataRepository<IPPRCaptionRepository>();

                IEnumerable<PPRCaption> pprcaptions = pprcaptionRepository.Get();

                return pprcaptions.ToArray();
            });
        }

        #endregion

        #region Income CommFee Line Caption

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeCommFeeLineCaption UpdateIncomeCommFeeLineCaption(IncomeCommFeeLineCaption ICFLcaption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCommFeeLineCaptionRepository icflcaptionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCommFeeLineCaptionRepository>();

                IncomeCommFeeLineCaption updatedEntity = null;

                if (ICFLcaption.ID == 0)
                {
                    updatedEntity = icflcaptionRepository.Add(ICFLcaption);
                }
                else
                    updatedEntity = icflcaptionRepository.Update(ICFLcaption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeCommFeeLineCaption(int ICFLcaptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCommFeeLineCaptionRepository icflcaptionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCommFeeLineCaptionRepository>();

                icflcaptionRepository.Remove(ICFLcaptionId);
            });
        }

        public IncomeCommFeeLineCaption GetIncomeCommFeeLineCaption(int ICFLcaptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCommFeeLineCaptionRepository icflcaptionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCommFeeLineCaptionRepository>();

                IncomeCommFeeLineCaption icflcaptionEntity = icflcaptionRepository.Get(ICFLcaptionId);
                if (icflcaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeCommFeeLineCaption with CaptionId of {0} is not in database", ICFLcaptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return icflcaptionEntity;
            });
        }
        public IncomeCommFeeLineCaption[] GetAllIncomeCommFeeLineCaption()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCommFeeLineCaptionRepository icflcaptionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCommFeeLineCaptionRepository>();

                IEnumerable<IncomeCommFeeLineCaption> icflcaptions = icflcaptionRepository.Get();

                return icflcaptions.ToArray();
            });
        }

        public IncomeCommFeeLineCaption[] GetIncomeCommFeeLineCaptionUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCommFeeLineCaptionRepository icflcaptionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCommFeeLineCaptionRepository>();

                IEnumerable<IncomeCommFeeLineCaption> icfl = icflcaptionRepository.GetIncomeCommFeeLineCaptionBySearchValue(searchvalue);



                return icfl.ToArray();
            });
        }

        #endregion

        #region IncomeLine Caption

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeLineCapton UpdateIncomeLineCapton(IncomeLineCapton ilcaption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeLineCaptonRepository ilcaptionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeLineCaptonRepository>();

                IncomeLineCapton updatedEntity = null;

                if (ilcaption.IncomeLineCaptonId == 0)
                {
                    updatedEntity = ilcaptionRepository.Add(ilcaption);
                }
                else
                    updatedEntity = ilcaptionRepository.Update(ilcaption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeLineCapton(int ilCaptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeLineCaptonRepository ilcaptionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeLineCaptonRepository>();

                ilcaptionRepository.Remove(ilCaptionId);
            });
        }

        public IncomeLineCapton GetIncomeLineCapton(int ilCaptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeLineCaptonRepository ilcaptionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeLineCaptonRepository>();

                IncomeLineCapton ilcaptionEntity = ilcaptionRepository.Get(ilCaptionId);
                if (ilcaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeLine Caption with Id of {0} is not in database", ilCaptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ilcaptionEntity;
            });
        }
        public IncomeLineCapton[] GetAllIncomeLineCaptons()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeLineCaptonRepository ilcaptionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeLineCaptonRepository>();

                IEnumerable<IncomeLineCapton> ilcaptions = ilcaptionRepository.Get();

                return ilcaptions.ToArray();
            });
        }

        #endregion

        #region  Income Products table Unit

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeProductstableUnit UpdateIncomeProductstableUnit(IncomeProductstableUnit iptUnit)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductstableUnitRepository iptUnitRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductstableUnitRepository>();

                IncomeProductstableUnit updatedEntity = null;

                if (iptUnit.ID == 0)
                {
                    updatedEntity = iptUnitRepository.Add(iptUnit);
                }
                else
                    updatedEntity = iptUnitRepository.Update(iptUnit);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeProductstableUnit(int iptUnitId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductstableUnitRepository iptUnitRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductstableUnitRepository>();

                iptUnitRepository.Remove(iptUnitId);
            });
        }

        public IncomeProductstableUnit GetIncomeProductstableUnit(int iptUnitId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductstableUnitRepository iptUnitRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductstableUnitRepository>();

                IncomeProductstableUnit iptUnitEntity = iptUnitRepository.Get(iptUnitId);
                if (iptUnitEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeLine Caption with Id of {0} is not in database", iptUnitId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return iptUnitEntity;
            });
        }
        public IncomeProductstableUnit[] GetAllIncomeProductstableUnits()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductstableUnitRepository iptUnitRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductstableUnitRepository>();

                IEnumerable<IncomeProductstableUnit> iptUnits = iptUnitRepository.Get();

                return iptUnits.ToArray();
            });
        }

        public IncomeProductstableUnit[] GetincomeproducttableunitUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductstableUnitRepository iptRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductstableUnitRepository>();

                IEnumerable<IncomeProductstableUnit> ipt = iptRepository.GetIncomeProductUnitBySearchValue(searchvalue);



                return ipt.ToArray();
            });
        }

        #endregion

        #region  Income Products Table Treasury

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeProductsTableTreasury UpdateIncomeProductsTableTreasury(IncomeProductsTableTreasury iptTreasury)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableTreasuryRepository iptTreasuryRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableTreasuryRepository>();

                IncomeProductsTableTreasury updatedEntity = null;

                if (iptTreasury.ID == 0)
                {
                    updatedEntity = iptTreasuryRepository.Add(iptTreasury);
                }
                else
                    updatedEntity = iptTreasuryRepository.Update(iptTreasury);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeProductsTableTreasury(int iptTreasuryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableTreasuryRepository iptTreasuryRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableTreasuryRepository>();

                iptTreasuryRepository.Remove(iptTreasuryId);
            });
        }

        public IncomeProductsTableTreasury GetIncomeProductsTableTreasury(int iptTreasuryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableTreasuryRepository iptTreasuryRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableTreasuryRepository>();

                IncomeProductsTableTreasury iptTreasuryEntity = iptTreasuryRepository.Get(iptTreasuryId);
                if (iptTreasuryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Income product table treasury with Id of {0} is not in database", iptTreasuryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return iptTreasuryEntity;
            });
        }
        public IncomeProductsTableTreasury[] GetAllIncomeProductsTableTreasury()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableTreasuryRepository iptTreasuryRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableTreasuryRepository>();

                IEnumerable<IncomeProductsTableTreasury> iptTreasury = iptTreasuryRepository.Get();

                return iptTreasury.ToArray();
            });
        }

        public IncomeProductsTableTreasury[] GetIncomeProductsTableTreasuryUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableTreasuryRepository iptRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableTreasuryRepository>();

                IEnumerable<IncomeProductsTableTreasury> ipt = iptRepository.GetIncomeProductsTableTreasuryBySearchValue(searchvalue);

                return ipt.ToArray();
            });
        }

        #endregion

        #region  Income NEA Mapping

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeNEAMapping UpdateIncomeNEAMapping(IncomeNEAMapping incomeNEAmapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeNEAMappingRepository incomeNEAmappingRepository = _DataRepositoryFactory.GetDataRepository<IIncomeNEAMappingRepository>();

                IncomeNEAMapping updatedEntity = null;

                if (incomeNEAmapping.ID == 0)
                {
                    updatedEntity = incomeNEAmappingRepository.Add(incomeNEAmapping);
                }
                else
                    updatedEntity = incomeNEAmappingRepository.Update(incomeNEAmapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeNEAMapping(int incomeNEAmappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeNEAMappingRepository incomeNEAmappingRepository = _DataRepositoryFactory.GetDataRepository<IIncomeNEAMappingRepository>();

                incomeNEAmappingRepository.Remove(incomeNEAmappingId);
            });
        }

        public IncomeNEAMapping GetIncomeNEAMapping(int incomeNEAmappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeNEAMappingRepository incomeNEAmappingRepository = _DataRepositoryFactory.GetDataRepository<IIncomeNEAMappingRepository>();

                IncomeNEAMapping incomeNEAmappingEntity = incomeNEAmappingRepository.Get(incomeNEAmappingId);
                if (incomeNEAmappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Income NEA Mapping with Id of {0} is not in database", incomeNEAmappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeNEAmappingEntity;
            });
        }
        public IncomeNEAMapping[] GetAllIncomeNEAMapping()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeNEAMappingRepository incomeNEAmappingRepository = _DataRepositoryFactory.GetDataRepository<IIncomeNEAMappingRepository>();

                IEnumerable<IncomeNEAMapping> incomeNEAmapping = incomeNEAmappingRepository.Get();

                return incomeNEAmapping.ToArray();
            });
        }

        //public IncomeNEAMappingInfo[] GetIncomeNEAMappingUsingSearchValue(string searchvalue)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IIncomeNEAMappingRepository incomeNEAmappingRepository = _DataRepositoryFactory.GetDataRepository<IIncomeNEAMappingRepository>();

        //        IEnumerable<IncomeNEAMappingInfo> inm = incomeNEAmappingRepository.GetIncomeNEAMappingBySearchValue(searchvalue);

        //        return inm.ToArray();
        //    });
        //}

        //public IncomeNEAMappingInfo[] GetIncomeNEAMapping()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IIncomeNEAMappingRepository incomeNEAmappingRepository = _DataRepositoryFactory.GetDataRepository<IIncomeNEAMappingRepository>();

        //        IEnumerable<IncomeNEAMappingInfo> inm = incomeNEAmappingRepository.GetIncomeNEAMapping();

        //        return inm.ToArray();
        //    });
        //}

        public IncomeNEAMappingData[] GetIncomeNEAMappingUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeNEAMappingRepository inmRepository = _DataRepositoryFactory.GetDataRepository<IIncomeNEAMappingRepository>();

                List<IncomeNEAMappingData> inm = new List<IncomeNEAMappingData>();

                IEnumerable<IncomeNEAMappingInfo> inm2 = inmRepository.GetIncomeNEAMappingBySearchValue(searchvalue);

                foreach (var a in inm2)
                {
                    var dr = new IncomeNEAMappingData()
                    {
                        Id = a.Id,
                        Category_Code = a.Category_Code,
                        CATEGORY_DESCRIPTION = a.CATEGORY_DESCRIPTION,
                        Product_Code = a.Product_Code,
                        Class = a.Class,
                        Caption = a.Caption,
                        AssetType = a.AssetType,
                    };
                    inm.Add(dr);
                }

                return inm.ToArray();
            });
        }

        public IncomeNEAMappingData[] GetFullIncomeNEAMapping()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeNEAMappingRepository inmRepository = _DataRepositoryFactory.GetDataRepository<IIncomeNEAMappingRepository>();

                List<IncomeNEAMappingData> inm = new List<IncomeNEAMappingData>();

                IEnumerable<IncomeNEAMappingInfo> inm2 = inmRepository.GetFullIncomeNEAMapping();

                foreach (var a in inm2)
                {
                    var dr = new IncomeNEAMappingData()
                    {
                        Id = a.Id,
                        Category_Code = a.Category_Code,
                        CATEGORY_DESCRIPTION = a.CATEGORY_DESCRIPTION,
                        Product_Code = a.Product_Code,
                        Class = a.Class,
                        Caption = a.Caption,
                        AssetType = a.AssetType,
                    };
                    inm.Add(dr);
                }

                return inm.ToArray();
            });
        }

        #endregion

        #region  KBL MIS Product Category Info

        [OperationBehavior(TransactionScopeRequired = true)]
        public KBL_MISProductCategoryInfo UpdateKBL_MISProductCategoryInfo(KBL_MISProductCategoryInfo misproductcategory)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IKBL_MISProductCategoryInfoRepository misproductcategoryRepository = _DataRepositoryFactory.GetDataRepository<IKBL_MISProductCategoryInfoRepository>();

                KBL_MISProductCategoryInfo updatedEntity = null;

                if (misproductcategory.Id == 0)
                {
                    updatedEntity = misproductcategoryRepository.Add(misproductcategory);
                }
                else
                    updatedEntity = misproductcategoryRepository.Update(misproductcategory);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteKBL_MISProductCategoryInfo(int misproductcategoryId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IKBL_MISProductCategoryInfoRepository misproductcategoryRepository = _DataRepositoryFactory.GetDataRepository<IKBL_MISProductCategoryInfoRepository>();

                misproductcategoryRepository.Remove(misproductcategoryId);
            });
        }

        public KBL_MISProductCategoryInfo GetKBL_MISProductCategoryInfo(int misproductcategoryId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IKBL_MISProductCategoryInfoRepository misproductcategoryRepository = _DataRepositoryFactory.GetDataRepository<IKBL_MISProductCategoryInfoRepository>();

                KBL_MISProductCategoryInfo misproductcategoryEntity = misproductcategoryRepository.Get(misproductcategoryId);
                if (misproductcategoryEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MIS Product CategoryId with Id of {0} is not in database", misproductcategoryId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return misproductcategoryEntity;
            });
        }
        public KBL_MISProductCategoryInfo[] GetAllKBL_MISProductCategoryInfo()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IKBL_MISProductCategoryInfoRepository misproductcategoryRepository = _DataRepositoryFactory.GetDataRepository<IKBL_MISProductCategoryInfoRepository>();

                IEnumerable<KBL_MISProductCategoryInfo> misproductcategory = misproductcategoryRepository.Get();

                return misproductcategory.ToArray();
            });
        }

        #endregion

        #region IncomeCaptionPosition

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeCaptionPosition UpdateIncomeCaptionPosition(IncomeCaptionPosition incomecaption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCaptionPositionRepository IncomeCaptionPositionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCaptionPositionRepository>();

                IncomeCaptionPosition updatedEntity = null;

                if (incomecaption.ID == 0)
                {
                    updatedEntity = IncomeCaptionPositionRepository.Add(incomecaption);
                }
                else
                    updatedEntity = IncomeCaptionPositionRepository.Update(incomecaption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeCaptionPosition(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCaptionPositionRepository IncomeCaptionPositionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCaptionPositionRepository>();

                IncomeCaptionPositionRepository.Remove(ID);
            });
        }

        public IncomeCaptionPosition GetIncomeCaptionPosition(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCaptionPositionRepository IncomeCaptionPositionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCaptionPositionRepository>();

                IncomeCaptionPosition plcaptionEntity = IncomeCaptionPositionRepository.Get(ID);
                if (plcaptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PLCaption with CaptionId of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return plcaptionEntity;
            });
        }
        public IncomeCaptionPosition[] GetAllIncomeCaptionPosition()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCaptionPositionRepository IncomeCaptionPositionRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCaptionPositionRepository>();

                IEnumerable<IncomeCaptionPosition> plcaptions = IncomeCaptionPositionRepository.Get();

                return plcaptions.ToArray();
            });
        }

        #endregion

        #region GroupCaptions

        [OperationBehavior(TransactionScopeRequired = true)]
        public GroupCaptions UpdateGroupCaptions(GroupCaptions groupCaptions)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGroupCaptionsRepository groupCaptionsRepository = _DataRepositoryFactory.GetDataRepository<IGroupCaptionsRepository>();

                GroupCaptions updatedEntity = null;

                if (groupCaptions.GroupCaptionID == 0)
                {
                    updatedEntity = groupCaptionsRepository.Add(groupCaptions);
                }
                else
                    updatedEntity = groupCaptionsRepository.Update(groupCaptions);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGroupCaptions(int GroupCaptionID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGroupCaptionsRepository groupCaptionsRepository = _DataRepositoryFactory.GetDataRepository<IGroupCaptionsRepository>();

                groupCaptionsRepository.Remove(GroupCaptionID);
            });
        }

        public GroupCaptions GetGroupCaptions(int GroupCaptionID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGroupCaptionsRepository groupCaptionsRepository = _DataRepositoryFactory.GetDataRepository<IGroupCaptionsRepository>();

                GroupCaptions groupCaptionsEntity = groupCaptionsRepository.Get(GroupCaptionID);
                if (groupCaptionsEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("GroupCaptions with GroupCaptionID of {0} is not in database", GroupCaptionID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return groupCaptionsEntity;
            });
        }
        public GroupCaptions[] GetAllGroupCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGroupCaptionsRepository groupCaptionsRepository = _DataRepositoryFactory.GetDataRepository<IGroupCaptionsRepository>();

                IEnumerable<GroupCaptions> groupCaptions = groupCaptionsRepository.Get();

                return groupCaptions.ToArray();
            });
        }

        #endregion

        #region Income Product ALT

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeProductsTableALT Updateincomeproducttablealt(IncomeProductsTableALT incomeproducttablealt)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableALTRepository iptRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableALTRepository>();

                IncomeProductsTableALT updatedEntity = null;

                if (incomeproducttablealt.ProductID == 0)
                {
                    updatedEntity = iptRepository.Add(incomeproducttablealt);
                }
                else
                    updatedEntity = iptRepository.Update(incomeproducttablealt);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Deleteincomeproducttablealt(int productid)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableALTRepository iptRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableALTRepository>();

                iptRepository.Remove(productid);
            });
        }

        public IncomeProductsTableALT Getincomeproducttablealt(int productid)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableALTRepository iptRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableALTRepository>();

                IncomeProductsTableALT iptEntity = iptRepository.Get(productid);
                if (iptEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeProduct with ProductId of {0} is not in database", productid));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return iptEntity;
            });
        }
        public IncomeProductsTableALT[] GetAllincomeproducttablealt()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableALTRepository iptRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableALTRepository>();

                IEnumerable<IncomeProductsTableALT> ipt = iptRepository.Get();

                return ipt.ToArray();
            });
        }


        public IncomeProductsTableALT[] GetincomeproducttablealtUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeProductsTableALTRepository iptRepository = _DataRepositoryFactory.GetDataRepository<IIncomeProductsTableALTRepository>();

                IEnumerable<IncomeProductsTableALT> ipt = iptRepository.GetIncomeProductBySearchValue(searchvalue);



                return ipt.ToArray();
            });
        }

        #endregion

    }
}
