using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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
using systemCoreEntities = Fintrak.Shared.SystemCore.Entities;
using systemCoreData = Fintrak.Data.SystemCore.Contracts;
using Fintrak.Data.SystemCore.Contracts;
using Fintrak.Shared.SystemCore.Entities;
using System.Data.SqlClient;

namespace Fintrak.Business.IFRS.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class FinstatManager : ManagerBase, IFinstatService
    {
        public FinstatManager()
        {
        }

        public FinstatManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        /// <summary>
        /// </summary>
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_IFRS";
        const string SOLUTION_ALIAS = "IFRS";
        const string MODULE_NAME = "FIN_FINSTAT";
        const string MODULE_ALIAS = "Finstat";

        //  private string a;
        const string GROUP_ADMINISTRATOR = "Administrator";
        const string GROUP_USER = "User";

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
                IGLTypeRepository glTypeRepository = _DataRepositoryFactory.GetDataRepository<IGLTypeRepository>();

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
                        var adminRole = roleRepository.Get().Where(c => c.Name == GROUP_ADMINISTRATOR && c.SolutionId == solution.SolutionId).FirstOrDefault();
                        var userRole = roleRepository.Get().Where(c => c.Name == GROUP_USER && c.SolutionId == solution.SolutionId).FirstOrDefault();

                        int menuIndex = 0;

                        //register menu
                        //get the root for finstat
                        var root = menuRepository.Get().Where(c => c.Alias == "Finstat").FirstOrDefault();

                        var actionMenu = new systemCoreEntities.Menu()
                         {
                             Name = "IFRS_REGISTRY",
                             Alias = "Registries",
                             Action = "IFRS_REGISTRY",
                             ActionUrl = "finstat-registry-list",
                             Image = null,
                             ImageUrl = "action_image",
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
                            Name = "GL_MAPPINGS",
                            Alias = "GL Mappings",
                            Action = "GL_MAPPINGS",
                            ActionUrl = "finstat-glmapping-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "UNMAPPED_GL",
                            Alias = "Un-Mapped GL",
                            Action = "UNMAPPED_GL",
                            ActionUrl = "finstat-unmappedgl-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "QUALITATIVE_NOTE",
                            Alias = "Qualitative Note",
                            Action = "QUALITATIVE_NOTE",
                            ActionUrl = "finstat-qualitativenote-list",
                            Image = null,
                            ImageUrl = "action_image",
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

                        //actionMenu = new systemCoreEntities.Menu()
                        //{
                        //    Name = "GL_TYPES",
                        //    Alias = "GL Types",
                        //    Action = "GL_TYPES",
                        //    ActionUrl = "finstat-gltype-list",
                        //    Image = null,
                        //    ImageUrl = "action_image",
                        //    ModuleId = module.EntityId,
                        //    ParentId = root.EntityId,
                        //    Position = menuIndex += 1,
                        //    Active = true,
                        //    Deleted = false,
                        //    CreatedBy = "Auto",
                        //    CreatedOn = DateTime.Now,
                        //    UpdatedBy = "Auto",
                        //    UpdatedOn = DateTime.Now
                        //};

                        //menuRepository.Add(actionMenu);

                        //menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        //{
                        //    MenuId = actionMenu.EntityId,
                        //    RoleId = adminRole.EntityId,
                        //    Active = true,
                        //    Deleted = false,
                        //    CreatedBy = "Auto",
                        //    CreatedOn = DateTime.Now,
                        //    UpdatedBy = "Auto",
                        //    UpdatedOn = DateTime.Now
                        //});

                        //actionMenu = new systemCoreEntities.Menu()
                        //{
                        //    Name = "INSTRUMENT_GL_MAP",
                        //    Alias = "Instrument GL Maps",
                        //    Action = "INSTRUMENT_GL_MAP",
                        //    ActionUrl = "finstat-instrumenttypeglmap-list",
                        //    Image = null,
                        //    ImageUrl = "action_image",
                        //    ModuleId = module.EntityId,
                        //    ParentId = root.EntityId,
                        //    Position = menuIndex += 1,
                        //    Active = true,
                        //    Deleted = false,
                        //    CreatedBy = "Auto",
                        //    CreatedOn = DateTime.Now,
                        //    UpdatedBy = "Auto",
                        //    UpdatedOn = DateTime.Now
                        //};

                        //menuRepository.Add(actionMenu);

                        //menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        //{
                        //    MenuId = actionMenu.EntityId,
                        //    RoleId = adminRole.EntityId,
                        //    Active = true,
                        //    Deleted = false,
                        //    CreatedBy = "Auto",
                        //    CreatedOn = DateTime.Now,
                        //    UpdatedBy = "Auto",
                        //    UpdatedOn = DateTime.Now
                        //});

                        //actionMenu = new systemCoreEntities.Menu()
                        //{
                        //    Name = "AUTO_POSTING_TEMPLATES",
                        //    Alias = "Auto Posting Templates",
                        //    Action = "AUTO_POSTING_TEMPLATES",
                        //    ActionUrl = "finstat-autopostingtemplate-list",
                        //    Image = null,
                        //    ImageUrl = "action_image",
                        //    ModuleId = module.EntityId,
                        //    ParentId = root.EntityId,
                        //    Position = menuIndex += 1,
                        //    Active = true,
                        //    Deleted = false,
                        //    CreatedBy = "Auto",
                        //    CreatedOn = DateTime.Now,
                        //    UpdatedBy = "Auto",
                        //    UpdatedOn = DateTime.Now
                        //};

                        //menuRepository.Add(actionMenu);

                        //menuRoleRepository.Add(new systemCoreEntities.MenuRole()
                        //{
                        //    MenuId = actionMenu.EntityId,
                        //    RoleId = adminRole.EntityId,
                        //    Active = true,
                        //    Deleted = false,
                        //    CreatedBy = "Auto",
                        //    CreatedOn = DateTime.Now,
                        //    UpdatedBy = "Auto",
                        //    UpdatedOn = DateTime.Now
                        //});

                        actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "TRIAL_BALANCE",
                            Alias = "Trial Balance",
                            Action = "TRIAL_BALANCE",
                            ActionUrl = "finstat-trialbalance-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "ADJUSTMENTS",
                            Alias = "Pass Adjustments",
                            Action = "ADJUSTMENTS",
                            ActionUrl = "finstat-adjustment-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "POSTING_DETAIL",
                            Alias = "IFRS Auto Posted Adjustment",
                            Action = "POSTING_DETAIL",
                            ActionUrl = "finstat-postingdetail-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "TRANSACTION_DETAIL",
                            Alias = "Transaction Detail",
                            Action = "TRANSACTION_DETAIL",
                            ActionUrl = "finstat-transactiondetail-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "FINSTAT_BUDGET",
                            Alias = "Finstat Budget",
                            Action = "FINSTAT_BUDGET",
                            ActionUrl = "finstat-budget-list",
                            Image = null,
                            ImageUrl = "action_image",
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
                            Name = "REVENUE_MAPPING",
                            Alias = "Revenue Mapping",
                            Action = "REVENUE_MAPPING",
                            ActionUrl = "finstat-revenueglmapping-list",
                            Image = null,
                            ImageUrl = "action_image",
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

                        //GLType

                        //glTypeRepository.Add(new GLType() { Name = "VolumeGL"  });
                        //glTypeRepository.Add(new GLType() { Name = "IncomeGL" });
                        //glTypeRepository.Add(new GLType() { Name = "IntrestRCGL" });
                        //glTypeRepository.Add(new GLType() { Name = "PremiumGL" });
                        //glTypeRepository.Add(new GLType() { Name = "DiscountGL" });

                    }

                    ts.Complete();
                }

            });

        }

        #region AutoPostingTemplate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public AutoPostingTemplate UpdateAutoPostingTemplate(AutoPostingTemplate autoPostingTemplate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAutoPostingTemplateRepository autoPostingTemplateRepository = _DataRepositoryFactory.GetDataRepository<IAutoPostingTemplateRepository>();

                AutoPostingTemplate updatedEntity = null;

                if (autoPostingTemplate.AutoPostingTemplateId == 0)
                    updatedEntity = autoPostingTemplateRepository.Add(autoPostingTemplate);
                else
                    updatedEntity = autoPostingTemplateRepository.Update(autoPostingTemplate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAutoPostingTemplate(int autoPostingTemplateId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAutoPostingTemplateRepository autoPostingTemplateRepository = _DataRepositoryFactory.GetDataRepository<IAutoPostingTemplateRepository>();

                autoPostingTemplateRepository.Remove(autoPostingTemplateId);
            });
        }

        public AutoPostingTemplate GetAutoPostingTemplate(int autoPostingTemplateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAutoPostingTemplateRepository autoPostingTemplateRepository = _DataRepositoryFactory.GetDataRepository<IAutoPostingTemplateRepository>();

                AutoPostingTemplate autoPostingTemplateEntity = autoPostingTemplateRepository.Get(autoPostingTemplateId);
                if (autoPostingTemplateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("AutoPostingTemplate with ID of {0} is not in database", autoPostingTemplateId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return autoPostingTemplateEntity;
            });
        }

        public AutoPostingTemplate[] GetAllAutoPostingTemplates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAutoPostingTemplateRepository autoPostingTemplateRepository = _DataRepositoryFactory.GetDataRepository<IAutoPostingTemplateRepository>();

                IEnumerable<AutoPostingTemplate> autoPostingTemplates = autoPostingTemplateRepository.Get().ToArray();

                return autoPostingTemplates.ToArray();
            });
        }

        #endregion

        #region GLMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public GLMapping UpdateGLMapping(GLMapping glMapping, int status)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLMappingRepository glMappingRepository = _DataRepositoryFactory.GetDataRepository<IGLMappingRepository>();

                GLMapping updatedEntity = null;

                if (glMapping.GLMappingId == 0)
                    updatedEntity = glMappingRepository.Add(glMapping);
                else
                    updatedEntity = glMappingRepository.Update(glMapping);
                //status = 1 meaning checked2;

                if (status == 1)
                {
                    InsertOtherCurrencies(glMapping.GLCode);
                }


                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGLMapping(int glMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLMappingRepository glMappingRepository = _DataRepositoryFactory.GetDataRepository<IGLMappingRepository>();

                glMappingRepository.Remove(glMappingId);
            });
        }

        public GLMapping GetGLMapping(int glMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLMappingRepository glMappingRepository = _DataRepositoryFactory.GetDataRepository<IGLMappingRepository>();

                GLMapping glMappingEntity = glMappingRepository.Get(glMappingId);
                if (glMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("GLMapping with ID of {0} is not in database", glMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return glMappingEntity;
            });
        }

        public GLMapping[] GetAllGLMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLMappingRepository glMappingRepository = _DataRepositoryFactory.GetDataRepository<IGLMappingRepository>();

                IEnumerable<GLMapping> glMappings = glMappingRepository.Get().ToArray();

                return glMappings.ToArray();
            });
        }

        public GLMappingData[] GetUnMappedGLs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITrialBalanceGapRepository trialBalanceRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceGapRepository>();
                IGLMappingRepository glMappingRepository = _DataRepositoryFactory.GetDataRepository<IGLMappingRepository>();
                IIFRSRegistryRepository ifrsRegsitryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRegistryRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                //var gls = glMappingRepository.Get().Select(c => c.GLCode).Distinct();
                //var trialGls = trialBalanceRepository.GetTrialBalances(runDate.SolutionRunDate.RunDate).Where(c => !gls.Contains(c.GLCode)
                //    );

                var gls = glMappingRepository.Get().Select(c => c.GLCode.Substring(3, c.GLCode.Length - 3)).Distinct();
                var trialGls = trialBalanceRepository.GetTrialBalances(runDate.SolutionRunDate.RunDate).Where(c => !gls.Contains(c.GLCode.Substring(3, c.GLCode.Length - 3))
                    && !c.GLCode.Contains("ADJ") && !c.GLCode.Contains("IDJ") && !c.GLCode.Contains("IFR") && !c.GLCode.Contains("UFR")
                    );
                //c => c.GLCode.Substring(3, c.GLCode.Length)
                //(c => !gls.Contains(c.GLCode) && !c.GLCode.Contains("ADJ")
                List<GLMappingData> glMappings = new List<GLMappingData>();

                foreach (var item in trialGls)
                {
                    glMappingRepository.Get().Select(c => c.GLCode).Distinct();
                    string parentCode = item.Sub_GL;
                    var cCode = glMappingRepository.Get().Where(d => d.GLParentCode == parentCode).FirstOrDefault();
                    var cap = cCode != null ? cCode.CaptionCode : "N/A";
                    var mainCaption = ifrsRegsitryRepository.Get().Where(r => r.Code == cap).FirstOrDefault();

                    glMappings.Add(new GLMappingData
                    {
                        GLCode = item.GLCode.Substring(3, item.GLCode.Length - 3),
                        GLDescription = item.Description,
                        CaptionCode = mainCaption != null ? mainCaption.Code : string.Empty,
                        MainCaption = mainCaption != null ? mainCaption.Caption : string.Empty,
                        SubCaption = cCode != null ? cCode.SubCaption : string.Empty,
                        SubCaption1 = cCode != null ? cCode.SubCaption1 : string.Empty,
                        CompanyCode = cCode != null ? cCode.CompanyCode : string.Empty,
                        SubPosition = cCode != null ? cCode.SubPosition : 1,
                        GLParentCode = parentCode != null ? parentCode : string.Empty

                    });
                }
                return glMappings.ToArray();
            });
        }

        public GLMappingData[] GetGLMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLMappingRepository glMappingRepository = _DataRepositoryFactory.GetDataRepository<IGLMappingRepository>();


                List<GLMappingData> glMappings = new List<GLMappingData>();
                IEnumerable<GLMappingInfo> glMappingInfos = glMappingRepository.GetGLMappings().ToArray();

                foreach (var glMappingInfo in glMappingInfos)
                {
                    glMappings.Add(
                        new GLMappingData
                        {
                            GLMappingId = glMappingInfo.GLMapping.EntityId,
                            GLCode = glMappingInfo.GLMapping.GLCode,
                            GLParentCode = glMappingInfo.GLMapping.GLParentCode,
                            GLDescription = glMappingInfo.GLMapping.GLDescription,
                            CaptionCode = glMappingInfo.GLMapping.CaptionCode,
                            MainCaption = glMappingInfo.IFRSRegistry.Caption,
                            SubCaption = glMappingInfo.GLMapping.SubCaption,
                            SubCaption1 = glMappingInfo.GLMapping.SubCaption1,
                            SubCaption2 = glMappingInfo.GLMapping.SubCaption2,
                            SubCaption3 = glMappingInfo.GLMapping.SubCaption3,
                            SubCaption4 = glMappingInfo.GLMapping.SubCaption4,
                            SubPosition = glMappingInfo.GLMapping.SubPosition,
                            CompanyCode = glMappingInfo.GLMapping.CompanyCode,
                            CanSwitch = glMappingInfo.GLMapping.CanSwitch, //!= null ? glMappingInfo.GLMapping.CanSwitch : bool.false,
                            Active = glMappingInfo.GLMapping.Active
                        });
                }

                return glMappings.ToArray();
            });
        }


        public GLMapping[] GetSubSubCaption(string caption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLMappingRepository glMappingRepository = _DataRepositoryFactory.GetDataRepository<IGLMappingRepository>();
                List<GLMapping> glmappings = glMappingRepository.GetSubSubCaption(caption);


                return glmappings.ToArray();
            });
        }


        public GLMappingData[] GetUnMappedGLbyGLCode(string glCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITrialBalanceGapRepository trialBalanceRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceGapRepository>();
                IGLMappingRepository glMappingRepository = _DataRepositoryFactory.GetDataRepository<IGLMappingRepository>();
                IIFRSRegistryRepository ifrsRegsitryRepository = _DataRepositoryFactory.GetDataRepository<IIFRSRegistryRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                var trialGls = trialBalanceRepository.GetTrialBalances(runDate.SolutionRunDate.RunDate).Where(c => c.GLCode.Substring(3, c.GLCode.Length - 3) == glCode);

                List<GLMappingData> glMappings = new List<GLMappingData>();

                foreach (var item in trialGls)
                {
                    string parentCode = item.Sub_GL;
                    var cCode = glMappingRepository.Get().Where(d => d.GLParentCode == parentCode).FirstOrDefault();
                    var cap = cCode != null ? cCode.CaptionCode : "N/A";
                    var mainCaption = ifrsRegsitryRepository.Get().Where(r => r.Code == cap).FirstOrDefault();
                    //var mainCaption = ifrsRegsitryRepository.Get().Where(r => r.Code == cCode.CaptionCode).FirstOrDefault();

                    glMappings.Add(new GLMappingData
                    {
                        //GLCode = item.GLCode.Substring(3, item.GLCode.Length - 3),
                        GLCode = item.GLCode,
                        GLDescription = item.Description,
                        CaptionCode = mainCaption != null ? mainCaption.Code : string.Empty,
                        MainCaption = mainCaption != null ? mainCaption.Caption : string.Empty,
                        SubCaption = cCode != null ? cCode.SubCaption : string.Empty,
                        SubCaption1 = cCode != null ? cCode.SubCaption1 : string.Empty,
                        CompanyCode = cCode != null ? cCode.CompanyCode : string.Empty,
                        SubPosition = cCode != null ? cCode.SubPosition : 1,
                        GLParentCode = parentCode != null ? parentCode : string.Empty
                    });
                }
                return glMappings.ToArray();
            });
        }
        #endregion

        #region GLType operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public GLType UpdateGLType(GLType glType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLTypeRepository glTypeRepository = _DataRepositoryFactory.GetDataRepository<IGLTypeRepository>();

                GLType updatedEntity = null;

                if (glType.GLTypeId == 0)
                    updatedEntity = glTypeRepository.Add(glType);
                else
                    updatedEntity = glTypeRepository.Update(glType);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGLType(int glTypeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLTypeRepository glTypeRepository = _DataRepositoryFactory.GetDataRepository<IGLTypeRepository>();

                glTypeRepository.Remove(glTypeId);
            });
        }

        public GLType GetGLType(int glTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLTypeRepository glTypeRepository = _DataRepositoryFactory.GetDataRepository<IGLTypeRepository>();

                GLType glTypeEntity = glTypeRepository.Get(glTypeId);
                if (glTypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("GLType with ID of {0} is not in database", glTypeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return glTypeEntity;
            });
        }

        public GLType[] GetAllGLTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLTypeRepository glTypeRepository = _DataRepositoryFactory.GetDataRepository<IGLTypeRepository>();

                IEnumerable<GLType> glTypes = glTypeRepository.Get().ToArray();

                return glTypes.ToArray();
            });
        }

        #endregion

        #region InstrumentType operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public InstrumentType UpdateInstrumentType(InstrumentType instrumentType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInstrumentTypeRepository instrumentTypeRepository = _DataRepositoryFactory.GetDataRepository<IInstrumentTypeRepository>();

                InstrumentType updatedEntity = null;

                if (instrumentType.InstrumentTypeId == 0)
                    updatedEntity = instrumentTypeRepository.Add(instrumentType);
                else
                    updatedEntity = instrumentTypeRepository.Update(instrumentType);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteInstrumentType(int instrumentTypeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInstrumentTypeRepository instrumentTypeRepository = _DataRepositoryFactory.GetDataRepository<IInstrumentTypeRepository>();

                instrumentTypeRepository.Remove(instrumentTypeId);
            });
        }

        public InstrumentType GetInstrumentType(int instrumentTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInstrumentTypeRepository instrumentTypeRepository = _DataRepositoryFactory.GetDataRepository<IInstrumentTypeRepository>();

                InstrumentType instrumentTypeEntity = instrumentTypeRepository.Get(instrumentTypeId);
                if (instrumentTypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("InstrumentType with ID of {0} is not in database", instrumentTypeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return instrumentTypeEntity;
            });
        }


        public InstrumentTypeData[] GetAllInstrumentTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInstrumentTypeRepository instrumentTypeRepository = _DataRepositoryFactory.GetDataRepository<IInstrumentTypeRepository>();


                List<InstrumentTypeData> instrumentTypes = new List<InstrumentTypeData>();
                IEnumerable<InstrumentTypeInfo> instrumentTypeInfos = instrumentTypeRepository.GetInstrumentTypes().ToArray();

                foreach (var instrumentTypeInfo in instrumentTypeInfos)
                {
                    instrumentTypes.Add(
                        new InstrumentTypeData
                        {
                            InstrumentTypeId = instrumentTypeInfo.InstrumentType.EntityId,
                            Name = instrumentTypeInfo.InstrumentType.Name,
                            Instrument = instrumentTypeInfo.InstrumentType.Instrument,
                            InstrumentName = instrumentTypeInfo.InstrumentType.Instrument.ToString(),
                            ParentId = instrumentTypeInfo.InstrumentType.ParentId,
                            ParentName = instrumentTypeInfo.Parent != null ? instrumentTypeInfo.Parent.Name : ""
                        });
                }

                return instrumentTypes.ToArray();
            });
        }


        #endregion

        #region InstrumentTypeGLMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public InstrumentTypeGLMap UpdateInstrumentTypeGLMap(InstrumentTypeGLMap instrumentTypeGLMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInstrumentTypeGLMapRepository instrumentTypeGLMapRepository = _DataRepositoryFactory.GetDataRepository<IInstrumentTypeGLMapRepository>();

                InstrumentTypeGLMap updatedEntity = null;

                if (instrumentTypeGLMap.InstrumentTypeGLMapId == 0)
                    updatedEntity = instrumentTypeGLMapRepository.Add(instrumentTypeGLMap);
                else
                    updatedEntity = instrumentTypeGLMapRepository.Update(instrumentTypeGLMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteInstrumentTypeGLMap(int instrumentTypeGLMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInstrumentTypeGLMapRepository instrumentTypeGLMapRepository = _DataRepositoryFactory.GetDataRepository<IInstrumentTypeGLMapRepository>();

                instrumentTypeGLMapRepository.Remove(instrumentTypeGLMapId);
            });
        }

        public InstrumentTypeGLMap GetInstrumentTypeGLMap(int instrumentTypeGLMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInstrumentTypeGLMapRepository instrumentTypeGLMapRepository = _DataRepositoryFactory.GetDataRepository<IInstrumentTypeGLMapRepository>();

                InstrumentTypeGLMap instrumentTypeGLMapEntity = instrumentTypeGLMapRepository.Get(instrumentTypeGLMapId);
                if (instrumentTypeGLMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("InstrumentTypeGLMap with ID of {0} is not in database", instrumentTypeGLMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return instrumentTypeGLMapEntity;
            });
        }

        public InstrumentTypeGLMapData[] GetAllInstrumentTypeGLMaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInstrumentTypeGLMapRepository instrumentTypeGLMapRepository = _DataRepositoryFactory.GetDataRepository<IInstrumentTypeGLMapRepository>();


                List<InstrumentTypeGLMapData> instrumentTypeGLMaps = new List<InstrumentTypeGLMapData>();
                IEnumerable<InstrumentTypeGLMapInfo> instrumentTypeGLMapInfos = instrumentTypeGLMapRepository.GetInstrumentTypeGLMaps().ToArray();

                foreach (var instrumentTypeGLMapInfo in instrumentTypeGLMapInfos)
                {
                    instrumentTypeGLMaps.Add(
                        new InstrumentTypeGLMapData
                        {
                            InstrumentTypeGLMapId = instrumentTypeGLMapInfo.InstrumentTypeGLMap.EntityId,
                            InstrumentTypeId = instrumentTypeGLMapInfo.InstrumentType.EntityId,
                            InstrumentTypeName = instrumentTypeGLMapInfo.InstrumentType.Name,
                            InstrumentName = instrumentTypeGLMapInfo.InstrumentType.Instrument.ToString(),
                            GLTypeId = instrumentTypeGLMapInfo.GLType.EntityId,
                            GLTypeName = instrumentTypeGLMapInfo.GLType.Name,
                            GLCode = instrumentTypeGLMapInfo.GLMapping.GLCode,
                            GLName = instrumentTypeGLMapInfo.GLMapping.GLDescription,
                            CompanyName = ""
                        });
                }

                return instrumentTypeGLMaps.ToArray();
            });
        }


        #endregion

        #region TrialBalanceGap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TrialBalanceGap UpdateTrialBalanceGap(TrialBalanceGap trialBalanceGap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITrialBalanceGapRepository trialBalanceGapRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceGapRepository>();

                TrialBalanceGap updatedEntity = null;

                if (trialBalanceGap.TrialBalanceGAPId == 0)
                    updatedEntity = trialBalanceGapRepository.Add(trialBalanceGap);
                else
                    updatedEntity = trialBalanceGapRepository.Update(trialBalanceGap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTrialBalanceGap(int trialBalanceGapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITrialBalanceGapRepository trialBalanceGapRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceGapRepository>();

                trialBalanceGapRepository.Remove(trialBalanceGapId);
            });
        }

        public TrialBalanceGap GetTrialBalanceGap(int trialBalanceGapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITrialBalanceGapRepository trialBalanceGapRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceGapRepository>();

                TrialBalanceGap trialBalanceGapEntity = trialBalanceGapRepository.Get(trialBalanceGapId);
                if (trialBalanceGapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TrialBalanceGap with ID of {0} is not in database", trialBalanceGapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return trialBalanceGapEntity;
            });
        }

        public TrialBalanceGap[] GetAllTrialBalanceGaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITrialBalanceGapRepository trialBalanceGapRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceGapRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                IEnumerable<TrialBalanceGap> trialBalanceGaps = trialBalanceGapRepository.GetTrialBalances(runDate.SolutionRunDate.RunDate).ToArray();

                return trialBalanceGaps.ToArray();
            });
        }

        public TrialBalanceGap[] GetTrialBalanceGapByGL(string glCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITrialBalanceGapRepository trialBalanceGapRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceGapRepository>();

                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                IEnumerable<TrialBalanceGap> trialBalanceGaps = trialBalanceGapRepository.Get().Where(c => c.GLCode == glCode || c.GLCode == "ADJ" + glCode.Substring(3) && c.TransDate == runDate.SolutionRunDate.RunDate).ToArray();

                return trialBalanceGaps.ToArray();
            });
        }

        #endregion

        #region TrialBalance operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TrialBalance UpdateTrialBalance(TrialBalance trialBalance)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITrialBalanceRepository trialBalanceRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceRepository>();

                TrialBalance updatedEntity = null;

                if (trialBalance.TrialBalanceId == 0)
                    updatedEntity = trialBalanceRepository.Add(trialBalance);
                else
                    updatedEntity = trialBalanceRepository.Update(trialBalance);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTrialBalance(int trialBalanceId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITrialBalanceRepository trialBalanceRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceRepository>();

                trialBalanceRepository.Remove(trialBalanceId);
            });
        }

        public TrialBalance GetTrialBalance(int trialBalanceId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITrialBalanceRepository trialBalanceRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceRepository>();

                TrialBalance trialBalanceEntity = trialBalanceRepository.Get(trialBalanceId);
                if (trialBalanceEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TrialBalance with ID of {0} is not in database", trialBalanceId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return trialBalanceEntity;
            });
        }

        public TrialBalance[] GetAllTrialBalances()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITrialBalanceRepository trialBalanceRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceRepository>();

                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                IEnumerable<TrialBalance> trialBalances = trialBalanceRepository.GetTrialBalances(runDate.SolutionRunDate.RunDate).ToArray();

                return trialBalances.ToArray();
            });
        }

        public TrialBalance[] GetTrialBalanceByGL(string glCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITrialBalanceRepository trialBalanceRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceRepository>();

                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                IEnumerable<TrialBalance> trialBalances = trialBalanceRepository.Get().Where(c => c.GLCode == glCode || c.GLCode == "ADJ" + glCode.Substring(3) && c.TransDate == runDate.SolutionRunDate.RunDate).ToArray();

                return trialBalances.ToArray();
            });
        }

        #endregion

        #region TrialBalanceConsolidated operations

        public TrialBalanceConsolidated[] GetAllTrialBalanceConsolidated()
        {

            var connectionString = GetDataConnection();

            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var trialBalanceConsolidated = new List<TrialBalanceConsolidated>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_group_trial_balance_gap", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var trialBalanceConsolidateds = new TrialBalanceConsolidated();

                    if (reader["BranchCode"] != DBNull.Value)
                        trialBalanceConsolidateds.BranchCode = reader["BranchCode"].ToString();

                    if (reader["GLCode"] != DBNull.Value)
                        trialBalanceConsolidateds.GLCode = reader["GLCode"].ToString();

                    if (reader["Description"] != DBNull.Value)
                        trialBalanceConsolidateds.Description = reader["Description"].ToString();

                    if (reader["GLSubHeadCode"] != DBNull.Value)
                        trialBalanceConsolidateds.GLSubHeadCode = reader["GLSubHeadCode"].ToString();

                    if (reader["Currency"] != DBNull.Value)
                        trialBalanceConsolidateds.Currency = reader["Currency"].ToString();

                    if (reader["ExchangeRate"] != DBNull.Value)
                        trialBalanceConsolidateds.ExchangeRate = double.Parse(reader["ExchangeRate"].ToString());

                    if (reader["Debit"] != DBNull.Value)
                        trialBalanceConsolidateds.Debit = decimal.Parse(reader["Debit"].ToString());

                    if (reader["Credit"] != DBNull.Value)
                        trialBalanceConsolidateds.Credit = decimal.Parse(reader["Credit"].ToString());

                    if (reader["LCY_Debit"] != DBNull.Value)
                        trialBalanceConsolidateds.LCY_Debit = decimal.Parse(reader["LCY_Debit"].ToString());

                    if (reader["LCY_Credit"] != DBNull.Value)
                        trialBalanceConsolidateds.LCY_Credit = decimal.Parse(reader["LCY_Credit"].ToString());

                    if (reader["Balance"] != DBNull.Value)
                        trialBalanceConsolidateds.Balance = decimal.Parse(reader["Balance"].ToString());

                    if (reader["LCY_Balance"] != DBNull.Value)
                        trialBalanceConsolidateds.LCY_Balance = decimal.Parse(reader["LCY_Balance"].ToString());

                    if (reader["GLType"] != DBNull.Value)
                        trialBalanceConsolidateds.GLType = reader["GLType"].ToString();

                    if (reader["RevaluationDiff"] != DBNull.Value)
                        trialBalanceConsolidateds.RevaluationDiff = decimal.Parse(reader["RevaluationDiff"].ToString());

                    if (reader["TransDate"] != DBNull.Value)
                        trialBalanceConsolidateds.TransDate = DateTime.Parse(reader["TransDate"].ToString());

                    if (reader["CompanyCode"] != DBNull.Value)
                        trialBalanceConsolidateds.CompanyCode = reader["CompanyCode"].ToString();

                    if (reader["Sub_GL"] != DBNull.Value)
                        trialBalanceConsolidateds.Sub_GL = reader["Sub_GL"].ToString();

                    if (reader["AdjustmentCode"] != DBNull.Value)
                        trialBalanceConsolidateds.AdjustmentCode = reader["AdjustmentCode"].ToString();

                    trialBalanceConsolidated.Add(trialBalanceConsolidateds);
                }

                con.Close();
            }

            return trialBalanceConsolidated.ToArray();
        }



        #endregion

        #region TrialBalanceConsolidatedIFRS operations

        public TrialBalanceConsolidatedIFRS[] GetAllTrialBalanceConsolidatedIFRS()
        {

            var connectionString = GetDataConnection();

            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var ifrstrialBalanceConsolidated = new List<TrialBalanceConsolidatedIFRS>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_group_trial_balance", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var trialBalanceConsolidatedifrs = new TrialBalanceConsolidatedIFRS();

                    if (reader["BranchCode"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.BranchCode = reader["BranchCode"].ToString();

                    if (reader["GLCode"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.GLCode = reader["GLCode"].ToString();

                    if (reader["Description"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.Description = reader["Description"].ToString();

                    if (reader["GLSubHeadCode"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.GLSubHeadCode = reader["GLSubHeadCode"].ToString();

                    if (reader["Currency"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.Currency = reader["Currency"].ToString();

                    if (reader["ExchangeRate"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.ExchangeRate = double.Parse(reader["ExchangeRate"].ToString());

                    if (reader["Debit"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.Debit = decimal.Parse(reader["Debit"].ToString());

                    if (reader["Credit"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.Credit = decimal.Parse(reader["Credit"].ToString());

                    if (reader["LCY_Debit"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.LCY_Debit = decimal.Parse(reader["LCY_Debit"].ToString());

                    if (reader["LCY_Credit"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.LCY_Credit = decimal.Parse(reader["LCY_Credit"].ToString());

                    if (reader["Balance"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.Balance = decimal.Parse(reader["Balance"].ToString());

                    if (reader["LCY_Balance"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.LCY_Balance = decimal.Parse(reader["LCY_Balance"].ToString());

                    if (reader["GLType"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.GLType = reader["GLType"].ToString();

                    if (reader["RevaluationDiff"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.RevaluationDiff = decimal.Parse(reader["RevaluationDiff"].ToString());

                    if (reader["TransDate"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.TransDate = DateTime.Parse(reader["TransDate"].ToString());

                    if (reader["CompanyCode"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.CompanyCode = reader["CompanyCode"].ToString();

                    if (reader["Sub_GL"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.Sub_GL = reader["Sub_GL"].ToString();

                    if (reader["AdjustmentCode"] != DBNull.Value)
                        trialBalanceConsolidatedifrs.AdjustmentCode = reader["AdjustmentCode"].ToString();

                    ifrstrialBalanceConsolidated.Add(trialBalanceConsolidatedifrs);
                }

                con.Close();
            }

            return ifrstrialBalanceConsolidated.ToArray();
        }

        #endregion

        #region PostingDetail operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PostingDetail UpdatePostingDetail(PostingDetail postingDetail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPostingDetailRepository postingDetailRepository = _DataRepositoryFactory.GetDataRepository<IPostingDetailRepository>();

                PostingDetail updatedEntity = null;

                if (postingDetail.PostingDetailId == 0)
                    updatedEntity = postingDetailRepository.Add(postingDetail);
                else
                    updatedEntity = postingDetailRepository.Update(postingDetail);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePostingDetail(int postingDetailId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPostingDetailRepository postingDetailRepository = _DataRepositoryFactory.GetDataRepository<IPostingDetailRepository>();

                postingDetailRepository.Remove(postingDetailId);
            });
        }

        public PostingDetail GetPostingDetail(int postingDetailId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPostingDetailRepository postingDetailRepository = _DataRepositoryFactory.GetDataRepository<IPostingDetailRepository>();

                PostingDetail postingDetailEntity = postingDetailRepository.Get(postingDetailId);
                if (postingDetailEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PostingDetail with ID of {0} is not in database", postingDetailId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return postingDetailEntity;
            });
        }

        //public PostingDetail[] GetAllPostingDetails()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IPostingDetailRepository postingDetailRepository = _DataRepositoryFactory.GetDataRepository<IPostingDetailRepository>();

        //        IEnumerable<PostingDetail> postingDetails = postingDetailRepository.Get().ToArray();

        //        return postingDetails.ToArray();
        //    });
        //}

        //public PostingDetail[] GetPostingDetailsByType(ReportType reportType)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IPostingDetailRepository postingDetailRepository = _DataRepositoryFactory.GetDataRepository<IPostingDetailRepository>();

        //        IEnumerable<PostingDetail> postingDetails = postingDetailRepository.GetEntitiesByType(reportType);

        //        return postingDetails.ToArray();
        //    });
        //}

        public PostingDetailData[] GetPostingDetailsByType(ReportType reportType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPostingDetailRepository postingDetailRepository = _DataRepositoryFactory.GetDataRepository<IPostingDetailRepository>();


                List<PostingDetailData> postingDetails = new List<PostingDetailData>();
                IEnumerable<PostingDetailInfo> postingDetailsnfos = postingDetailRepository.GetPostingDetailByType(reportType).ToArray();

                foreach (var postingDetailInfo in postingDetailsnfos)
                {
                    postingDetails.Add(
                        new PostingDetailData
                        {
                            PostingDetailId = postingDetailInfo.PostingDetail.EntityId,
                            TransactionId = postingDetailInfo.PostingDetail.TransactionId,
                            TransDescription = postingDetailInfo.PostingDetail.TransDescription,
                            GLCode = postingDetailInfo.PostingDetail.GLCode,
                            GLDescription=postingDetailInfo.PostingDetail.GLDescription,
                            Indicator=postingDetailInfo.PostingDetail.Indicator,
                            GAAPAmount=postingDetailInfo.PostingDetail.GAAPAmount,
                            ReportType = postingDetailInfo.PostingDetail.ReportType,
                            ReportTypeName = postingDetailInfo.PostingDetail.ReportType.ToString(),
                            CompanyCode = postingDetailInfo.PostingDetail.CompanyCode,
                            ComputedAmount = postingDetailInfo.PostingDetail.ComputedAmount,
                            IFRSAmount = postingDetailInfo.PostingDetail.IFRSAmount,
                            RunDate = postingDetailInfo.PostingDetail.RunDate

                        });
                }

                return postingDetails.ToArray();
            });
        }

        #endregion

        #region GLAdjustment operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public GLAdjustment UpdateGLAdjustment(GLAdjustment glAdjustment)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLAdjustmentRepository glAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IGLAdjustmentRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                GLAdjustment updatedEntity = null;

                if (glAdjustment.GLAdjustmentId == 0)
                {
                    glAdjustment.Amount = glAdjustment.Amount;
                    //glAdjustment.Amount = glAdjustment.Indicator == Indicator.Debit ? -1 * glAdjustment.Amount : glAdjustment.Amount;
                    glAdjustment.RunDate = runDate.SolutionRunDate.RunDate;
                    updatedEntity = glAdjustmentRepository.Add(glAdjustment);
                }
                else
                    updatedEntity = glAdjustmentRepository.Update(glAdjustment);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void PostGLAdjustment(AdjustmentType adjustmentType)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICompanyRepository companyRepository = _DataRepositoryFactory.GetDataRepository<ICompanyRepository>();
                IBranchRepository branchRepository = _DataRepositoryFactory.GetDataRepository<IBranchRepository>();
                IGLAdjustmentRepository glAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IGLAdjustmentRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();
                ITrialBalanceGapRepository gapTrialRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceGapRepository>();
                ITrialBalanceRepository trialRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                var adjustmentCode = UniqueKeyGenerator.RNGCharacterMask(6, 8);

                var adjustments = glAdjustmentRepository.GetGLAdjustments(adjustmentType, runDate.SolutionRunDate.RunDate).Where(c => c.GLAdjustment.Posted == false);

                var branchCodes = adjustments.Select(c => c.GLAdjustment.CompanyCode).Distinct();

                var allowPosting = true;
                decimal sumAmount = 0;

                foreach (var branchCode in branchCodes)
                {
                    var branchAdjustments = adjustments.Where(c => c.GLAdjustment.CompanyCode == branchCode);

                    foreach (var adj in branchAdjustments)
                        sumAmount += adj.GLAdjustment.Indicator == Indicator.Debit ? -1 * adj.GLAdjustment.Amount : adj.GLAdjustment.Amount;

                    if (sumAmount > 0)
                    {
                        allowPosting = false;

                        Exception ex = new Exception(string.Format("Fail to post adjustment due unbalance branch {0} figures", branchCode));
                        throw new FaultException<Exception>(ex, ex.Message);
                    }
                }

                if (allowPosting)
                {

                    using (TransactionScope ts = new TransactionScope())
                    {
                        foreach (var branchCode in branchCodes)
                        {
                            var branch = branchRepository.Get().Where(c => c.Code == branchCode).FirstOrDefault();

                            var company = companyRepository.Get(branch.CompanyId);
                            var gls = adjustments.Where(c => c.GLAdjustment.CompanyCode == branchCode).Select(c => c.GLMapping.GLCode).Distinct();

                            foreach (var gl in gls)
                            {
                                var glAdjustments = adjustments.Where(c => c.GLMapping.GLCode == gl && c.GLAdjustment.CompanyCode == branchCode );
                                var glAdjustment = glAdjustments.FirstOrDefault();

                                var tempADJ = new List<TempAdjustment>();

                                foreach (var adjustment in glAdjustments)
                                {
                                    tempADJ.Add(new TempAdjustment() { Indicator = adjustment.GLAdjustment.Indicator, Amount = adjustment.GLAdjustment.Amount });

                                    adjustment.GLAdjustment.AdjustmentCode = adjustmentCode;
                                    adjustment.GLAdjustment.Posted = true;
                                    glAdjustmentRepository.Update(adjustment.GLAdjustment);
                                }

                                //foreach (var adjustment in glAdjustments)
                                //{
                                //    adjustment.GLAdjustment.Amount = adjustment.GLAdjustment.Indicator == Indicator.Debit ? (-1 * adjustment.GLAdjustment.Amount) : adjustment.GLAdjustment.Amount;
                                //}

                                var glSum = tempADJ.Sum(c => c.Indicator == Indicator.Debit ? (-1 * c.Amount) : c.Amount);

                                if (adjustmentType == AdjustmentType.GAAP)
                                {
                                    var gapTrial = new TrialBalanceGap()
                                    {
                                        BranchCode = glAdjustment.GLAdjustment.CompanyCode,
                                        GLCode = "ADJ" + glAdjustment.GLMapping.GLCode.Substring(3),
                                        Description = glAdjustment.GLMapping.GLDescription,
                                        GLSubHeadCode = glAdjustment.GLMapping.GLCode.Substring(3),
                                        Currency = glAdjustment.Currency.Symbol,
                                        ReportType=glAdjustment.GLAdjustment.ReportType,
                                        ExchangeRate = glAdjustment.Currency.Rate,
                                        Debit = glSum <= 0 ? GetNegativeAbsolute(glSum) : 0,
                                        Credit = glSum >= 0 ? GetNegativeAbsolute(glSum) : 0,
                                        LCY_Debit = glSum <= 0 ? GetNegativeAbsolute(glSum) : 0,
                                        LCY_Credit = glSum >= 0 ? GetNegativeAbsolute(glSum) : 0,
                                        // GLType = adjustment.GLAdjustment.
                                        RevaluationDiff = 0,
                                        TransDate = glAdjustment.GLAdjustment.RunDate,
                                        CompanyCode = company.Code,
                                        Sub_GL = string.Empty,
                                        AdjustmentCode = adjustmentCode,
                                    };

                                    gapTrial.Balance = gapTrial.Debit - gapTrial.Credit;
                                    gapTrial.LCY_Balance = gapTrial.Debit - gapTrial.Credit;

                                    gapTrialRepository.Add(gapTrial);

                                }
                                else if (adjustmentType == AdjustmentType.IFRS)
                                {
                                    var trial = new TrialBalance()
                                    {
                                        BranchCode = glAdjustment.GLAdjustment.CompanyCode,
                                        GLCode = "IDJ" + glAdjustment.GLMapping.GLCode.Substring(3),
                                        Description = glAdjustment.GLMapping.GLDescription,
                                        GLSubHeadCode = glAdjustment.GLMapping.GLCode.Substring(3),
                                        Currency = glAdjustment.Currency.Symbol,
                                        ExchangeRate = glAdjustment.Currency.Rate,
                                        Debit = glSum <= 0 ? GetNegativeAbsolute(glSum) : 0,
                                        Credit = glSum >= 0 ? GetNegativeAbsolute(glSum) : 0,
                                        LCY_Debit = glSum <= 0 ? GetNegativeAbsolute(glSum) : 0,
                                        LCY_Credit = glSum >= 0 ? GetNegativeAbsolute(glSum) : 0,
                                        ReportType = glAdjustment.GLAdjustment.ReportType,
                                        // GLType = adjustment.GLAdjustment.
                                        RevaluationDiff = 0,
                                        TransDate = glAdjustment.GLAdjustment.RunDate,
                                        CompanyCode = company.Code,
                                        Sub_GL = string.Empty,
                                        AdjustmentCode = adjustmentCode,
                                    };

                                    trial.Balance = trial.Debit - trial.Credit;
                                    trial.LCY_Balance = trial.Debit - trial.Credit;

                                    trialRepository.Add(trial);
                                }
                            }
                        }

                        ts.Complete();
                    }
                }
                else
                {
                    Exception ex = new Exception(string.Format("Fail to post adjustment"));
                    throw new FaultException<Exception>(ex, ex.Message);
                }
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGLAdjustment(int glAdjustmentId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLAdjustmentRepository glAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IGLAdjustmentRepository>();

                glAdjustmentRepository.Remove(glAdjustmentId);
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void ReverseGLAdjustmentByCode(AdjustmentType adjustmentType, string adjustmentCode)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLAdjustmentRepository glAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IGLAdjustmentRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();
                ITrialBalanceGapRepository gapTrialRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceGapRepository>();
                ITrialBalanceRepository trialRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                var adjustments = glAdjustmentRepository.GetGLAdjustments(adjustmentType, runDate.SolutionRunDate.RunDate).Where(c => c.GLAdjustment.AdjustmentCode == adjustmentCode);

                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (var adjustment in adjustments)
                    {
                        adjustment.GLAdjustment.Posted = false;
                        glAdjustmentRepository.Update(adjustment.GLAdjustment);
                    }

                    if (adjustmentType == AdjustmentType.GAAP)
                    {
                        var gapTrials = gapTrialRepository.GetTrialBalances(runDate.SolutionRunDate.RunDate).Where(c => c.AdjustmentCode == adjustmentCode);
                        foreach (var trial in gapTrials)
                            DeleteTrialBalanceGap(trial.TrialBalanceGAPId);
                    }
                    else if (adjustmentType == AdjustmentType.IFRS)
                    {
                        var trials = trialRepository.GetTrialBalances(runDate.SolutionRunDate.RunDate).Where(c => c.AdjustmentCode == adjustmentCode);
                        foreach (var trial in trials)
                            DeleteTrialBalance(trial.TrialBalanceId);
                    }

                    ts.Complete();
                }



            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGLAdjustmentByCode(AdjustmentType adjustmentType, string adjustmentCode)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLAdjustmentRepository glAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IGLAdjustmentRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();
                ITrialBalanceGapRepository gapTrialRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceGapRepository>();
                ITrialBalanceRepository trialRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                var adjustments = glAdjustmentRepository.GetGLAdjustments(adjustmentType, runDate.SolutionRunDate.RunDate).Where(c => c.GLAdjustment.AdjustmentCode == adjustmentCode);

                foreach (var adjustment in adjustments)
                    DeleteGLAdjustment(adjustment.GLAdjustment.GLAdjustmentId);

                if (adjustmentType == AdjustmentType.GAAP)
                {
                    var gapTrials = gapTrialRepository.GetTrialBalances(runDate.SolutionRunDate.RunDate).Where(c => c.AdjustmentCode == adjustmentCode);
                    foreach (var trial in gapTrials)
                        DeleteTrialBalanceGap(trial.TrialBalanceGAPId);
                }
                else if (adjustmentType == AdjustmentType.IFRS)
                {
                    var trials = trialRepository.GetTrialBalances(runDate.SolutionRunDate.RunDate).Where(c => c.AdjustmentCode == adjustmentCode);
                    foreach (var trial in trials)
                        DeleteTrialBalance(trial.TrialBalanceId);
                }

            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void PurgeGLAdjustment(AdjustmentType adjustmentType)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLAdjustmentRepository glAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IGLAdjustmentRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();
                ITrialBalanceGapRepository gapTrialRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceGapRepository>();
                ITrialBalanceRepository trialRepository = _DataRepositoryFactory.GetDataRepository<ITrialBalanceRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                var adjustments = glAdjustmentRepository.GetGLAdjustments(adjustmentType, runDate.SolutionRunDate.RunDate);
                var adjustmentCodes = adjustments.Select(c => c.GLAdjustment.AdjustmentCode).Distinct<string>();

                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (var adjustment in adjustments)
                        DeleteGLAdjustment(adjustment.GLAdjustment.GLAdjustmentId);

                    if (adjustmentType == AdjustmentType.GAAP)
                    {
                        var gapTrials = gapTrialRepository.GetTrialBalances(runDate.SolutionRunDate.RunDate).Where(c => adjustmentCodes.Contains(c.AdjustmentCode));
                        foreach (var trial in gapTrials)
                            DeleteTrialBalanceGap(trial.TrialBalanceGAPId);
                    }
                    else if (adjustmentType == AdjustmentType.IFRS)
                    {
                        var trials = trialRepository.GetTrialBalances(runDate.SolutionRunDate.RunDate).Where(c => adjustmentCodes.Contains(c.AdjustmentCode));
                        foreach (var trial in trials)
                            DeleteTrialBalance(trial.TrialBalanceId);
                    }

                    ts.Complete();
                }


            });
        }

        public GLAdjustment GetGLAdjustment(int glAdjustmentId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLAdjustmentRepository glAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IGLAdjustmentRepository>();

                GLAdjustment glAdjustmentEntity = glAdjustmentRepository.Get(glAdjustmentId);
                if (glAdjustmentEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("GLAdjustment with ID of {0} is not in database", glAdjustmentId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return glAdjustmentEntity;
            });
        }

        public GLAdjustmentData[] GetGLAdjustmentByStatus(AdjustmentType adjustmentType, bool posted)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLAdjustmentRepository glAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IGLAdjustmentRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                List<GLAdjustmentData> glAdjustments = new List<GLAdjustmentData>();
                IEnumerable<GLAdjustmentInfo> glAdjustmentInfos = glAdjustmentRepository.GetGLAdjustments(adjustmentType, runDate.SolutionRunDate.RunDate).Where(c => c.GLAdjustment.Posted == posted).ToArray();

                foreach (var glAdjustmentInfo in glAdjustmentInfos)
                {
                    glAdjustments.Add(
                        new GLAdjustmentData
                        {
                            GLAdjustmentId = glAdjustmentInfo.GLAdjustment.EntityId,
                            AdjustmentCode = glAdjustmentInfo.GLAdjustment.AdjustmentCode,
                            GLCode = glAdjustmentInfo.GLMapping.GLCode,
                            GLName = glAdjustmentInfo.GLMapping.GLDescription,
                            Amount = glAdjustmentInfo.GLAdjustment.Amount,
                            Narration = glAdjustmentInfo.GLAdjustment.Narration,
                            Indicator = glAdjustmentInfo.GLAdjustment.Indicator,
                            IndicatorName = glAdjustmentInfo.GLAdjustment.Indicator.ToString(),
                            AdjustmentType = glAdjustmentInfo.GLAdjustment.AdjustmentType,
                            AdjustmentTypeName = glAdjustmentInfo.GLAdjustment.AdjustmentType.ToString(),
                            ReportType = glAdjustmentInfo.GLAdjustment.ReportType,
                            ReportTypeName = glAdjustmentInfo.GLAdjustment.ReportType.ToString(),
                            //CurrencyId = glAdjustmentInfo.GLAdjustment.CurrencyId,
                            CurrencyName = glAdjustmentInfo.Currency.Symbol,
                            RunDate = glAdjustmentInfo.GLAdjustment.RunDate,
                            CompanyCode = glAdjustmentInfo.Branch.Code,
                            Posted = glAdjustmentInfo.GLAdjustment.Posted,
                            Active = glAdjustmentInfo.GLAdjustment.Active
                        });
                }

                return glAdjustments.ToArray();
            });
        }

        public GLAdjustmentData[] GetGLAdjustments(AdjustmentType adjustmentType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGLAdjustmentRepository glAdjustmentRepository = _DataRepositoryFactory.GetDataRepository<IGLAdjustmentRepository>();
                ISolutionRunDateRepository runDateRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRunDateRepository>();

                var runDate = runDateRepository.GetSolutionRunDates().Where(c => c.SolutionRunDate.SolutionId == GetSolution(SOLUTION_NAME).EntityId).FirstOrDefault();

                List<GLAdjustmentData> glAdjustments = new List<GLAdjustmentData>();
                IEnumerable<GLAdjustmentInfo> glAdjustmentInfos = glAdjustmentRepository.GetGLAdjustments(adjustmentType, runDate.SolutionRunDate.RunDate).ToArray();

                foreach (var glAdjustmentInfo in glAdjustmentInfos)
                {
                    glAdjustments.Add(
                        new GLAdjustmentData
                        {
                            GLAdjustmentId = glAdjustmentInfo.GLAdjustment.EntityId,
                            AdjustmentCode = glAdjustmentInfo.GLAdjustment.AdjustmentCode,
                            GLCode = glAdjustmentInfo.GLMapping.GLCode,
                            GLName = glAdjustmentInfo.GLMapping.GLDescription,
                            Amount = glAdjustmentInfo.GLAdjustment.Amount,
                            Narration = glAdjustmentInfo.GLAdjustment.Narration,
                            Indicator = glAdjustmentInfo.GLAdjustment.Indicator,
                            IndicatorName = glAdjustmentInfo.GLAdjustment.Indicator.ToString(),
                            AdjustmentType = glAdjustmentInfo.GLAdjustment.AdjustmentType,
                            AdjustmentTypeName = glAdjustmentInfo.GLAdjustment.AdjustmentType.ToString(),
                            ReportType = glAdjustmentInfo.GLAdjustment.ReportType,
                            ReportTypeName = glAdjustmentInfo.GLAdjustment.ReportType.ToString(),
                            CurrencyName = glAdjustmentInfo.Currency.Symbol,
                            RunDate = glAdjustmentInfo.GLAdjustment.RunDate,
                            CompanyCode = glAdjustmentInfo.Branch != null ? glAdjustmentInfo.Branch.Code : string.Empty,
                            Posted = glAdjustmentInfo.GLAdjustment.Posted,
                            Active = glAdjustmentInfo.GLAdjustment.Active
                        });
                }

                return glAdjustments.ToArray();
            });
        }

        #endregion

        #region IFRSReport operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IFRSReport UpdateIFRSReport(IFRSReport ifrsReport)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSReportRepository ifrsReportRepository = _DataRepositoryFactory.GetDataRepository<IIFRSReportRepository>();

                IFRSReport updatedEntity = null;

                if (ifrsReport.IFRSReportId == 0)
                    updatedEntity = ifrsReportRepository.Add(ifrsReport);
                else
                    updatedEntity = ifrsReportRepository.Update(ifrsReport);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIFRSReport(int ifrsReportId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSReportRepository ifrsReportRepository = _DataRepositoryFactory.GetDataRepository<IIFRSReportRepository>();

                ifrsReportRepository.Remove(ifrsReportId);
            });
        }

        public IFRSReport GetIFRSReport(int ifrsReportId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSReportRepository ifrsReportRepository = _DataRepositoryFactory.GetDataRepository<IIFRSReportRepository>();

                IFRSReport ifrsReportEntity = ifrsReportRepository.Get(ifrsReportId);
                if (ifrsReportEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IFRSReport with ID of {0} is not in database", ifrsReportId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsReportEntity;
            });
        }

        public IFRSReport[] GetAllIFRSReports()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSReportRepository ifrsReportRepository = _DataRepositoryFactory.GetDataRepository<IIFRSReportRepository>();

                IEnumerable<IFRSReport> ifrsReports = ifrsReportRepository.Get().ToArray();

                return ifrsReports.ToArray();
            });
        }

        #endregion

        #region TransactionDetail operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TransactionDetail UpdateTransactionDetail(TransactionDetail transactionDetail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransactionDetailRepository transactionDetailRepository = _DataRepositoryFactory.GetDataRepository<ITransactionDetailRepository>();

                TransactionDetail updatedEntity = null;

                if (transactionDetail.TransactionDetailId == 0)
                    updatedEntity = transactionDetailRepository.Add(transactionDetail);
                else
                    updatedEntity = transactionDetailRepository.Update(transactionDetail);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTransactionDetail(int transactionDetailId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransactionDetailRepository transactionDetailRepository = _DataRepositoryFactory.GetDataRepository<ITransactionDetailRepository>();

                transactionDetailRepository.Remove(transactionDetailId);
            });
        }

        public TransactionDetail GetTransactionDetail(int transactionDetailId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransactionDetailRepository transactionDetailRepository = _DataRepositoryFactory.GetDataRepository<ITransactionDetailRepository>();

                TransactionDetail transactionDetailEntity = transactionDetailRepository.Get(transactionDetailId);
                if (transactionDetailEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TransactionDetail with ID of {0} is not in database", transactionDetailId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return transactionDetailEntity;
            });
        }

        public TransactionDetail[] GetAllTransactionDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransactionDetailRepository transactionDetailRepository = _DataRepositoryFactory.GetDataRepository<ITransactionDetailRepository>();

                IEnumerable<TransactionDetail> transactionDetails = transactionDetailRepository.Get().ToArray();

                return transactionDetails.ToArray();
            });
        }

        #endregion

        #region IFRSBudget operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IFRSBudget UpdateIFRSBudget(IFRSBudget ifrsBudget)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSBudgetRepository ifrsBudgetRepository = _DataRepositoryFactory.GetDataRepository<IIFRSBudgetRepository>();

                IFRSBudget updatedEntity = null;

                if (ifrsBudget.IFRSBudgetId == 0)
                    updatedEntity = ifrsBudgetRepository.Add(ifrsBudget);
                else
                    updatedEntity = ifrsBudgetRepository.Update(ifrsBudget);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIFRSBudget(int ifrsbudgetId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSBudgetRepository ifrsBudgetRepository = _DataRepositoryFactory.GetDataRepository<IIFRSBudgetRepository>();

                ifrsBudgetRepository.Remove(ifrsbudgetId);
            });
        }

        public IFRSBudget GetIFRSBudget(int ifrsbudgetId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSBudgetRepository ifrsBudgetRepository = _DataRepositoryFactory.GetDataRepository<IIFRSBudgetRepository>();

                IFRSBudget ifrsBudgetEntity = ifrsBudgetRepository.Get(ifrsbudgetId);
                if (ifrsBudgetEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IFRSBudget with ID of {0} is not in database", ifrsbudgetId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsBudgetEntity;
            });
        }

        public IFRSBudget[] GetAllIFRSBudgets()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIFRSBudgetRepository ifrsBudgetRepository = _DataRepositoryFactory.GetDataRepository<IIFRSBudgetRepository>();

                IEnumerable<IFRSBudget> ifrsBudgets = ifrsBudgetRepository.Get().ToArray();

                return ifrsBudgets.ToArray();
            });
        }

        #endregion

        #region RevenueGLMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RevenueGLMapping UpdateRevenueGLMapping(RevenueGLMapping revenueGLMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueGLMappingRepository RevenueGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IRevenueGLMappingRepository>();

                RevenueGLMapping updatedEntity = null;

                if (revenueGLMapping.GLMappingId == 0)
                    updatedEntity = RevenueGLMappingRepository.Add(revenueGLMapping);
                else
                    updatedEntity = RevenueGLMappingRepository.Update(revenueGLMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRevenueGLMapping(int revenueGLMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueGLMappingRepository RevenueGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IRevenueGLMappingRepository>();

                RevenueGLMappingRepository.Remove(revenueGLMappingId);
            });
        }

        public RevenueGLMapping GetRevenueGLMapping(int revenueGLMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueGLMappingRepository RevenueGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IRevenueGLMappingRepository>();

                RevenueGLMapping RevenueGLMappingEntity = RevenueGLMappingRepository.Get(revenueGLMappingId);
                if (RevenueGLMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("RevenueGLMapping with ID of {0} is not in database", revenueGLMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return RevenueGLMappingEntity;
            });
        }

        public RevenueGLMapping[] GetAllRevenueGLMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRevenueGLMappingRepository RevenueGLMappingRepository = _DataRepositoryFactory.GetDataRepository<IRevenueGLMappingRepository>();

                IEnumerable<RevenueGLMapping> RevenueGLMapping = RevenueGLMappingRepository.Get().ToArray();

                return RevenueGLMapping.ToArray();
            });
        }

        public KeyValueData[] GetUnMappedRevenueGLs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var results = new List<KeyValueData>();

                var connectionString = GetDataConnection();

                using (var con = new SqlConnection(connectionString))
                {
                    var cmd = new SqlCommand("spp_finstat_revenue_getunmappedgl", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;

                    con.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var data = new KeyValueData();

                        if (reader["GLCode"] != DBNull.Value)
                            data.Key = reader["GLCode"].ToString();

                        if (reader["Description"] != DBNull.Value)
                            data.Value = reader["Description"].ToString();

                        results.Add(data);
                    }

                    con.Close();
                }

                return results.ToArray();
            });
        }

        #endregion

        #region Helper

        protected override bool AllowAccessToOperation(string solutionName, List<string> groupNames)
        {
            if (groupNames.Count == 0)
                return true;

            systemCoreData.IUserRoleRepository accountRoleRepository = _DataRepositoryFactory.GetDataRepository<systemCoreData.IUserRoleRepository>();
            var accountRoles = accountRoleRepository.GetUserRoleInfo(solutionName, _LoginName, groupNames);

            if (accountRoles == null || accountRoles.Count() <= 0)
            {
                AuthorizationValidationException ex = new AuthorizationValidationException(string.Format("Access denied for {0}.", _LoginName));
                throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
            }

            return true;
        }

        private decimal GetNegativeAbsolute(decimal value)
        {
            return (value > 0 ? value : (-1 * value));
        }

        protected override string GetContextConnection()
        {
            if (!string.IsNullOrEmpty(_CompanyCode))
            {
                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();
            }

            return string.Empty;
        }

        protected Solution GetSolution(string name)
        {
            ISolutionRepository solutionRepository = _DataRepositoryFactory.GetDataRepository<ISolutionRepository>();

            var solution = solutionRepository.Get().Where(c => c.Name == name).FirstOrDefault();

            return solution;
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


        public void InsertOtherCurrencies(string glCode)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_insert_other_currency_glmapping", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "glCode",
                    Value = glCode,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        #endregion

    }

    public class TempAdjustment
    {
        public Indicator Indicator { get; set; }
        public decimal Amount { get; set; }
    }

}
