using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
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
using Fintrak.Presentation.WebClient.Models;
using System.Web.Script.Serialization;



namespace Fintrak.Business.IFRS.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete = false)]
    public class IFRS9Manager : ManagerBase, IIFRS9Service
    {
        public IFRS9Manager()
        {
        }

        public IFRS9Manager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        /// <summary>
        /// </summary>
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        const string SOLUTION_NAME = "FIN_IFRS";
        const string SOLUTION_ALIAS = "IFRS";
        const string MODULE_NAME = "FIN_IFRS9";
        const string MODULE_ALIAS = "IFRS9";

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
                //IGLTypeRepository glTypeRepository = _DataRepositoryFactory.GetDataRepository<IGLTypeRepository>();
             
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
                        var root = menuRepository.Get().Where(c => c.Alias == "IFRS9").FirstOrDefault();
                       
                       var actionMenu = new systemCoreEntities.Menu()
                        {
                            Name = "IRB_EXTERNAL_MAPPING",
                            Alias = "IRB To External Rating Mapping",
                            Action = "IRB_EXTERNAL_MAPPING",
                            ActionUrl = "ifrs9-ratingmapping-list",
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
                            Name = "INTERNAL_RATING_BASED",
                            Alias = "Internal Rating Based",
                            Action = "INTERNAL_RATING_BASED",
                            ActionUrl = "ifrs9-internalratingbased-list",
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
                            Name = "EXTERNAL_AGENCY_RATING",
                            Alias = "External Agency Rating",
                            Action = "EXTERNAL_AGENCY_RATING",
                            ActionUrl = "ifrs9-externalrating-list",
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
                            Name = "TRANSITION_MATRIX",
                            Alias = "Transition Matrix",
                            Action = "TRANSITION_MATRIX",
                            ActionUrl = "ifrs9-transition-list",
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
                            Name = "MACROECONOMIC_VARIABLE",
                            Alias = "12Months Macroeconomic Variables",
                            Action = "MACROECONOMIC_VARIABLE",
                            ActionUrl = "ifrs9-macroeconomic-list",
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
                            Name = "HISTORICAL_SECTORIAL_RATING",
                            Alias = "Historical Sectorial Rating",
                            Action = "HISTORICAL_SECTORIAL_RATING",
                            ActionUrl = "ifrs9-historicalsectorrating-list",
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
                            Name = "NOTCH_DIFFERENCE",
                            Alias = "Notch Difference",
                            Action = "NOTCH_DIFFERENCE",
                            ActionUrl = "ifrs9-notchdifference-list",
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
                            Name = "MACRO_ECONOMIC_HISTORICAL",
                            Alias = "Macro Economic Historical",
                            Action = "MACRO_ECONOMIC_HISTORICAL",
                            ActionUrl = "ifrs9-macroeconomichistorical-list",
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
                            Name = "HISTORICAL_CLASSIFICATION",
                            Alias = "Historical Classification",
                            Action = "HISTORICAL_CLASSIFICATION",
                            ActionUrl = "ifrs9-historicalclassification-list",
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
                            Name = "SET_UP",
                            Alias = "SetUp",
                            Action = "SET_UP",
                            ActionUrl = "ifrs9-setup-list",
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
                            Name = "SECTOR",
                            Alias = "Sector",
                            Action = "SECTOR",
                            ActionUrl = "ifrs9-sector-list",
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
                            Name = "ComputedForcastedPDLGD",
                            Alias = "Forcasted Computed PD/LGD",
                            Action = "ComputedForcastedPDLGD",
                            ActionUrl = "ifrs9-computedforcastedpdlgd-list",
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
                            Name = "SectorialRegressedLGD",
                            Alias = "Sectorial Regressed LGD",
                            Action = "SectorialRegressedLGD",
                            ActionUrl = "ifrs9-sectorialregressedlgd-list",
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
                            Name = "SectorialRegressedPD",
                            Alias = "Sectorial Regressed PD",
                            Action = "SectorialRegressedPD",
                            ActionUrl = "ifrs9-sectorialregressedpd-list",
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
                            Name = "HISTORICAL_SECTORIAL_PD",
                            Alias = "Historical Sectorial PD",
                            Action = "HISTORICAL_SECTORIAL_PD",
                            ActionUrl = "ifrs9-historicalsectorialpd-list",
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

                    }

                    ts.Complete();
                }

            });

        }


        #region ExternalRating operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ExternalRating UpdateExternalRating(ExternalRating externalRating)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExternalRatingRepository externalRatingRepository = _DataRepositoryFactory.GetDataRepository<IExternalRatingRepository>();

                ExternalRating updatedEntity = null;

                if (externalRating.ExternalRatingId == 0)
                    updatedEntity = externalRatingRepository.Add(externalRating);
                else
                    updatedEntity = externalRatingRepository.Update(externalRating);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteExternalRating(int externalRatingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExternalRatingRepository externalRatingRepository = _DataRepositoryFactory.GetDataRepository<IExternalRatingRepository>();

                externalRatingRepository.Remove(externalRatingId);
            });
        }

        public ExternalRating GetExternalRating(int externalRatingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExternalRatingRepository externalRatingRepository = _DataRepositoryFactory.GetDataRepository<IExternalRatingRepository>();

                ExternalRating externalRatingEntity = externalRatingRepository.Get(externalRatingId);
                if (externalRatingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ExternalRating with ID of {0} is not in database", externalRatingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return externalRatingEntity;
            });
        }

        public ExternalRating[] GetAllExternalRatings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IExternalRatingRepository externalRatingRepository = _DataRepositoryFactory.GetDataRepository<IExternalRatingRepository>();

                IEnumerable<ExternalRating> externalRatings = externalRatingRepository.Get().ToArray();

                return externalRatings.ToArray();
            });
        }

             #endregion

        #region HistoricalSectorRating operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public HistoricalSectorRating UpdateHistoricalSectorRating(HistoricalSectorRating historicalSectorRating)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorRatingRepository historicalSectorRatingRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorRatingRepository>();

                HistoricalSectorRating updatedEntity = null;

                if (historicalSectorRating.HistoricalSectorRatingId == 0)
                    updatedEntity = historicalSectorRatingRepository.Add(historicalSectorRating);
                else
                    updatedEntity = historicalSectorRatingRepository.Update(historicalSectorRating);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteHistoricalSectorRating(int historicalSectorRatingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorRatingRepository historicalSectorRatingRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorRatingRepository>();

                historicalSectorRatingRepository.Remove(historicalSectorRatingId);
            });
        }

        public HistoricalSectorRating GetHistoricalSectorRating(int historicalSectorRatingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorRatingRepository historicalSectorRatingRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorRatingRepository>();

                HistoricalSectorRating historicalSectorRatingEntity = historicalSectorRatingRepository.Get(historicalSectorRatingId);
                if (historicalSectorRatingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("HistoricalSectorRating with ID of {0} is not in database", historicalSectorRatingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return historicalSectorRatingEntity;
            });
        }

        public HistoricalSectorRating[] GetAllHistoricalSectorRatings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorRatingRepository historicalSectorRatingRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorRatingRepository>();

                IEnumerable<HistoricalSectorRating> historicalSectorRatings = historicalSectorRatingRepository.Get().ToArray();

                return historicalSectorRatings.ToArray();
            });
        }

        #endregion

        #region InternalRatingBased operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public InternalRatingBased UpdateInternalRatingBased(InternalRatingBased internalRatingBased)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInternalRatingBasedRepository internalRatingBasedRepository = _DataRepositoryFactory.GetDataRepository<IInternalRatingBasedRepository>();

                InternalRatingBased updatedEntity = null;

                if (internalRatingBased.InternalRatingBasedId == 0)
                    updatedEntity = internalRatingBasedRepository.Add(internalRatingBased);
                else
                    updatedEntity = internalRatingBasedRepository.Update(internalRatingBased);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteInternalRatingBased(int internalRatingBasedId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInternalRatingBasedRepository internalRatingBasedRepository = _DataRepositoryFactory.GetDataRepository<IInternalRatingBasedRepository>();

                internalRatingBasedRepository.Remove(internalRatingBasedId);
            });
        }

        public InternalRatingBased GetInternalRatingBased(int internalRatingBasedId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInternalRatingBasedRepository internalRatingBasedRepository = _DataRepositoryFactory.GetDataRepository<IInternalRatingBasedRepository>();

                InternalRatingBased internalRatingBasedEntity = internalRatingBasedRepository.Get(internalRatingBasedId);
                if (internalRatingBasedEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("InternalRatingBased with ID of {0} is not in database", internalRatingBasedId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return internalRatingBasedEntity;
            });
        }

        public InternalRatingBased[] GetAllInternalRatingBaseds()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInternalRatingBasedRepository internalRatingBasedRepository = _DataRepositoryFactory.GetDataRepository<IInternalRatingBasedRepository>();

                IEnumerable<InternalRatingBased> internalRatingBaseds = internalRatingBasedRepository.Get().ToArray().OrderBy(c => c.Rank).ToList();

                return internalRatingBaseds.ToArray();
            });
        }

        #endregion

        #region MacroEconomic operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacroEconomic UpdateMacroEconomic(MacroEconomic macroEconomic)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicRepository macroEconomicRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicRepository>();

                MacroEconomic updatedEntity = null;

                if (macroEconomic.MacroEconomicId == 0)
                    updatedEntity = macroEconomicRepository.Add(macroEconomic);
                else
                    updatedEntity = macroEconomicRepository.Update(macroEconomic);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacroEconomic(int macroEconomicId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicRepository macroEconomicRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicRepository>();

                macroEconomicRepository.Remove(macroEconomicId);
            });
        }

        public MacroEconomic GetMacroEconomic(int macroEconomicId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicRepository macroEconomicRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicRepository>();

                MacroEconomic macroEconomicEntity = macroEconomicRepository.Get(macroEconomicId);
                if (macroEconomicEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroEconomic with ID of {0} is not in database", macroEconomicId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macroEconomicEntity;
            });
        }

        public MacroEconomic[] GetAllMacroEconomics()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicRepository macroEconomicRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicRepository>();

                IEnumerable<MacroEconomic> macroEconomics = macroEconomicRepository.Get().ToArray();

                return macroEconomics.ToArray();
            });
        }

        #endregion

        #region RatingMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RatingMapping UpdateRatingMapping(RatingMapping ratingMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatingMappingRepository ratingMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatingMappingRepository>();

                RatingMapping updatedEntity = null;

                if (ratingMapping.RatingMappingId == 0)
                    updatedEntity = ratingMappingRepository.Add(ratingMapping);
                else
                    updatedEntity = ratingMappingRepository.Update(ratingMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRatingMapping(int ratingMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatingMappingRepository ratingMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatingMappingRepository>();

                ratingMappingRepository.Remove(ratingMappingId);
            });
        }

        public RatingMapping GetRatingMapping(int ratingMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatingMappingRepository ratingMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatingMappingRepository>();

                RatingMapping ratingMappingEntity = ratingMappingRepository.Get(ratingMappingId);
                if (ratingMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("RatingMapping with ID of {0} is not in database", ratingMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ratingMappingEntity;
            });
        }

        public RatingMapping[] GetAllRatingMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatingMappingRepository ratingMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatingMappingRepository>();

                IEnumerable<RatingMapping> ratingMappings = ratingMappingRepository.Get().ToArray();

                return ratingMappings.ToArray();
            });
        }


        public RatingMappingData[] GetRatingMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatingMappingRepository ratingMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatingMappingRepository>();


                List<RatingMappingData> ratingMappings = new List<RatingMappingData>();
                IEnumerable<RatingMappingInfo> ratingMappingInfos = ratingMappingRepository.GetRatingMappings().ToArray();

                foreach (var ratingMappingInfo in ratingMappingInfos)
                {
                    ratingMappings.Add(
                        new RatingMappingData
                        {
                            RatingMappingId = ratingMappingInfo.RatingMapping.EntityId,
                            Credit_Risk_Id = ratingMappingInfo.InternalRatingBased.InternalRatingBasedId,
                            CreditRiskName = ratingMappingInfo.InternalRatingBased.Code,
                            External_Rating_Id = ratingMappingInfo.ExternalRating.ExternalRatingId,
                            ExternalRatingName = ratingMappingInfo.ExternalRating.Rating,
                            Active = ratingMappingInfo.RatingMapping.Active
                        });
                }

                return ratingMappings.ToArray();
            });
        }

        #endregion

        #region Transition operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Transition UpdateTransition(Transition transition)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransitionRepository transitionRepository = _DataRepositoryFactory.GetDataRepository<ITransitionRepository>();

                Transition updatedEntity = null;

                if (transition.TransitionId == 0)
                    updatedEntity = transitionRepository.Add(transition);
                else
                    updatedEntity = transitionRepository.Update(transition);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTransition(int transitionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransitionRepository transitionRepository = _DataRepositoryFactory.GetDataRepository<ITransitionRepository>();

                transitionRepository.Remove(transitionId);
            });
        }

        public Transition GetTransition(int transitionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransitionRepository transitionRepository = _DataRepositoryFactory.GetDataRepository<ITransitionRepository>();

                Transition transitionEntity = transitionRepository.Get(transitionId);
                if (transitionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Transition with ID of {0} is not in database", transitionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return transitionEntity;
            });
        }

        public Transition[] GetAllTransitions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransitionRepository transitionRepository = _DataRepositoryFactory.GetDataRepository<ITransitionRepository>();

                IEnumerable<Transition> transitions = transitionRepository.Get().ToArray();

                return transitions.ToArray();
            });
        }

        #endregion

        #region Sector operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Sector UpdateSector(Sector sector)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorRepository sectorRepository = _DataRepositoryFactory.GetDataRepository<ISectorRepository>();

                Sector updatedEntity = null;

                if (sector.SectorId == 0)
                    updatedEntity = sectorRepository.Add(sector);
                else
                    updatedEntity = sectorRepository.Update(sector);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSector(int sectorId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorRepository sectorRepository = _DataRepositoryFactory.GetDataRepository<ISectorRepository>();

                sectorRepository.Remove(sectorId);
            });
        }

        public Sector GetSector(int sectorId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorRepository sectorRepository = _DataRepositoryFactory.GetDataRepository<ISectorRepository>();

                Sector sectorEntity = sectorRepository.Get(sectorId);
                if (sectorEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Sector with ID of {0} is not in database", sectorId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorEntity;
            });
        }

        public Sector[] GetAllSectors()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorRepository sectorRepository = _DataRepositoryFactory.GetDataRepository<ISectorRepository>();

                IEnumerable<Sector> sectors = sectorRepository.Get().ToArray();

                return sectors.ToArray();
            });
        }

        public Sector[] GetSectorBySource(string Source)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorRepository sectorRepository = _DataRepositoryFactory.GetDataRepository<ISectorRepository>();

                return sectorRepository.GetSectorBySource(Source).ToArray();
            });
        }

        #endregion

        #region HistoricalClassification operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public HistoricalClassification UpdateHistoricalClassification(HistoricalClassification historicalClassification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalClassificationRepository historicalClassificationRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalClassificationRepository>();

                HistoricalClassification updatedEntity = null;

                if (historicalClassification.HistoricalClassificationId == 0)
                    updatedEntity = historicalClassificationRepository.Add(historicalClassification);
                else
                    updatedEntity = historicalClassificationRepository.Update(historicalClassification);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteHistoricalClassification(int historicalClassificationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalClassificationRepository historicalClassificationRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalClassificationRepository>();

                historicalClassificationRepository.Remove(historicalClassificationId);
            });
        }

        public HistoricalClassification GetHistoricalClassification(int historicalClassificationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalClassificationRepository historicalClassificationRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalClassificationRepository>();

                HistoricalClassification historicalClassificationEntity = historicalClassificationRepository.Get(historicalClassificationId);
                if (historicalClassificationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("HistoricalClassification with ID of {0} is not in database", historicalClassificationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return historicalClassificationEntity;
            });
        }

        public HistoricalClassification[] GetAllHistoricalClassifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalClassificationRepository historicalClassificationRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalClassificationRepository>();

                IEnumerable<HistoricalClassification> historicalClassifications = historicalClassificationRepository.Get().ToArray();

                return historicalClassifications.ToArray();
            });
        }

        #endregion

        #region MacroEconomicHistorical operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacroEconomicHistorical UpdateMacroEconomicHistorical(MacroEconomicHistorical macroEconomicHistorical)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicHistoricalRepository macroEconomicHistoricalRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicHistoricalRepository>();

                MacroEconomicHistorical updatedEntity = null;

                if (macroEconomicHistorical.MacroEconomicHistoricalId == 0)
                    updatedEntity = macroEconomicHistoricalRepository.Add(macroEconomicHistorical);
                else
                    updatedEntity = macroEconomicHistoricalRepository.Update(macroEconomicHistorical);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacroEconomicHistorical(int macroEconomicHistoricalId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicHistoricalRepository macroEconomicHistoricalRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicHistoricalRepository>();

                macroEconomicHistoricalRepository.Remove(macroEconomicHistoricalId);
            });
        }

        public MacroEconomicHistorical GetMacroEconomicHistorical(int macroEconomicHistoricalId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicHistoricalRepository macroEconomicHistoricalRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicHistoricalRepository>();

                MacroEconomicHistorical macroEconomicHistoricalEntity = macroEconomicHistoricalRepository.Get(macroEconomicHistoricalId);
                if (macroEconomicHistoricalEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroEconomicHistorical with ID of {0} is not in database", macroEconomicHistoricalId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macroEconomicHistoricalEntity;
            });
        }

        //public MacroEconomicHistorical[] GetAllMacroEconomicHistoricals()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IMacroEconomicHistoricalRepository macroEconomicHistoricalRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicHistoricalRepository>();

        //        IEnumerable<MacroEconomicHistorical> macroEconomicHistoricals = macroEconomicHistoricalRepository.Get().ToArray();

        //        return macroEconomicHistoricals.ToArray();
        //    });
        //}


        public MacroEconomicHistoricalData[] GetAllMacroEconomicHistoricals()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicHistoricalRepository macroEconomicHistoricalRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicHistoricalRepository>();

                List<MacroEconomicHistoricalData> macroEconomicHistoricals = new List<MacroEconomicHistoricalData>();
                IEnumerable<MacroEconomicHistoricalInfo> macroEconomicHistoricalInfos = macroEconomicHistoricalRepository.GetMacroEconomicHistoricals().ToArray();


                foreach (var macroEconomicHistoricalInfo in macroEconomicHistoricalInfos)
                {
                    string vtype = "";
                    if (macroEconomicHistoricalInfo.MacroEconomicHistorical.Type == 1)
                    {
                        vtype = "PD";
                    }
                    else

                        vtype = "LGD";
                    macroEconomicHistoricals.Add(
                        new MacroEconomicHistoricalData
                        {
                            MacroEconomicHistoricalId = macroEconomicHistoricalInfo.MacroEconomicHistorical.EntityId,
                            Year = macroEconomicHistoricalInfo.MacroEconomicHistorical.Year,
                            Sector = macroEconomicHistoricalInfo.MacroEconomicHistorical.Sector_Code,
                            SectorName = macroEconomicHistoricalInfo.Sector.Description,
                            Type = macroEconomicHistoricalInfo.MacroEconomicHistorical.Type,
                            TypeName = vtype,
                            Variable = macroEconomicHistoricalInfo.MacroEconomicHistorical.Variable,
                            VariableName = macroEconomicHistoricalInfo.MacroEconomicVariable.Description,
                            Value = macroEconomicHistoricalInfo.MacroEconomicHistorical.Value,
                            Active = macroEconomicHistoricalInfo.MacroEconomicHistorical.Active
                        });
                }

                return macroEconomicHistoricals.ToArray();
            });
        }

        #endregion

        #region NotchDifference operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public NotchDifference UpdateNotchDifference(NotchDifference notchDifference)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INotchDifferenceRepository notchDifferenceRepository = _DataRepositoryFactory.GetDataRepository<INotchDifferenceRepository>();

                NotchDifference updatedEntity = null;

                if (notchDifference.NotchDifferenceId == 0)
                    updatedEntity = notchDifferenceRepository.Add(notchDifference);
                else
                    updatedEntity = notchDifferenceRepository.Update(notchDifference);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteNotchDifference(int notchDifferenceId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INotchDifferenceRepository notchDifferenceRepository = _DataRepositoryFactory.GetDataRepository<INotchDifferenceRepository>();

                notchDifferenceRepository.Remove(notchDifferenceId);
            });
        }

        public NotchDifference GetNotchDifference(int notchDifferenceId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INotchDifferenceRepository notchDifferenceRepository = _DataRepositoryFactory.GetDataRepository<INotchDifferenceRepository>();

                NotchDifference notchDifferenceEntity = notchDifferenceRepository.Get(notchDifferenceId);
                if (notchDifferenceEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("NotchDifference with ID of {0} is not in database", notchDifferenceId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return notchDifferenceEntity;
            });
        }

        public NotchDifference[] GetAllNotchDifferences()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                INotchDifferenceRepository notchDifferenceRepository = _DataRepositoryFactory.GetDataRepository<INotchDifferenceRepository>();

                IEnumerable<NotchDifference> notchDifferences = notchDifferenceRepository.Get().ToArray();

                return notchDifferences.ToArray();
            });
        }

        #endregion

        #region SetUp operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SetUp UpdateSetUp(SetUp setUp)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

                SetUp updatedEntity = null;

                if (setUp.SetUpId == 0)
                    updatedEntity = setUpRepository.Add(setUp);
                else
                    updatedEntity = setUpRepository.Update(setUp);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSetUp(int setUpId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

                setUpRepository.Remove(setUpId);
            });
        }

        public SetUp GetSetUp(int setUpId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

                SetUp setUpEntity = setUpRepository.Get(setUpId);
                if (setUpEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SetUp with ID of {0} is not in database", setUpId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return setUpEntity;
            });
        }

        public SetUp[] GetAllSetUps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

                IEnumerable<SetUp> setUps = setUpRepository.Get().ToArray();

                return setUps.ToArray();
            });
        }

        #endregion

        #region HistoricalSectorialPD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public HistoricalSectorialPD UpdateHistoricalSectorialPD(HistoricalSectorialPD historicalSectorialPD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialPDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialPDRepository>();

                HistoricalSectorialPD updatedEntity = null;

                if (historicalSectorialPD.HistoricalSectorialPDId == 0)
                    updatedEntity = historicalSectorialPDRepository.Add(historicalSectorialPD);
                else
                    updatedEntity = historicalSectorialPDRepository.Update(historicalSectorialPD);

                return updatedEntity;
                            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteHistoricalSectorialPD(int historicalSectorialPDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialPDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialPDRepository>();

                historicalSectorialPDRepository.Remove(historicalSectorialPDId);
            });
        }

        public HistoricalSectorialPD GetHistoricalSectorialPD(int historicalSectorialPDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialPDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialPDRepository>();

                HistoricalSectorialPD historicalSectorialPDEntity = historicalSectorialPDRepository.Get(historicalSectorialPDId);
                if (historicalSectorialPDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("HistoricalSectorialPD with ID of {0} is not in database", historicalSectorialPDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return historicalSectorialPDEntity;
            });
        }

        public HistoricalSectorialPD[] GetAllHistoricalSectorialPDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialPDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialPDRepository>();

                IEnumerable<HistoricalSectorialPD> historicalSectorialPDs = historicalSectorialPDRepository.Get().ToArray();

                return historicalSectorialPDs.ToArray();
            });
        }

        public string[] GetDistinctYear()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var yearList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_get_distinct_year", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var years = new ReferenceNoModel();
                        if (reader["Year"] != DBNull.Value)
                            years.RefNo = reader["Year"].ToString();
                        yearList.Add(years.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return yearList.ToArray();
        }

        public string[] GetDistinctPeriod()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var yearList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_get_distinct_period", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var periods = new ReferenceNoModel();
                        if (reader["Period"] != DBNull.Value)
                            periods.RefNo = reader["Period"].ToString();
                        yearList.Add(periods.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return yearList.ToArray();
        }


        public void ComputeHistoricalSectorialPD(int computationtype, int curYear, int curPeriod, int prevYear = 0, int prevPeriod = 0)
        {

            var connectionString = GetDataConnection();

            int status = 0;
            string storProc = "";
            if (computationtype == 1)
            {
                storProc = "ifrs_historical_pd_computation";
            }

            else
            {
                storProc = "ifrs_historical_lgd_computation";
            }

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(storProc, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CurYear",
                    Value = curYear,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CurPeriod",
                    Value = curPeriod,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PrevYear",
                    Value = prevYear,                
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PrevPeriod",
                    Value = prevPeriod,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }
       
        #endregion

        #region HistoricalSectorialLGD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public HistoricalSectorialLGD UpdateHistoricalSectorialLGD(HistoricalSectorialLGD historicalSectorialPD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialLGDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialLGDRepository>();

                HistoricalSectorialLGD updatedEntity = null;

                if (historicalSectorialPD.HistoricalSectorialLGDId == 0)
                    updatedEntity = historicalSectorialPDRepository.Add(historicalSectorialPD);
                else
                    updatedEntity = historicalSectorialPDRepository.Update(historicalSectorialPD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteHistoricalSectorialLGD(int historicalSectorialPDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialLGDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialLGDRepository>();

                historicalSectorialPDRepository.Remove(historicalSectorialPDId);
            });
        }

        public HistoricalSectorialLGD GetHistoricalSectorialLGD(int historicalSectorialPDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialLGDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialLGDRepository>();

                HistoricalSectorialLGD historicalSectorialPDEntity = historicalSectorialPDRepository.Get(historicalSectorialPDId);
                if (historicalSectorialPDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("HistoricalSectorialLGD with ID of {0} is not in database", historicalSectorialPDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return historicalSectorialPDEntity;
            });
        }

        public HistoricalSectorialLGD[] GetAllHistoricalSectorialLGDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IHistoricalSectorialLGDRepository historicalSectorialPDRepository = _DataRepositoryFactory.GetDataRepository<IHistoricalSectorialLGDRepository>();

                IEnumerable<HistoricalSectorialLGD> historicalSectorialPDs = historicalSectorialPDRepository.Get().ToArray();

                return historicalSectorialPDs.ToArray();
            });
        }

        public string[] GetDistinctLYear()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var yearList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_get_distinct_year", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var years = new ReferenceNoModel();
                        if (reader["Year"] != DBNull.Value)
                            years.RefNo = reader["Year"].ToString();
                        yearList.Add(years.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return yearList.ToArray();
        }

        public string[] GetDistinctLPeriod()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var yearList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_get_distinct_period", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var periods = new ReferenceNoModel();
                        if (reader["Period"] != DBNull.Value)
                            periods.RefNo = reader["Period"].ToString();
                        yearList.Add(periods.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return yearList.ToArray();
        }


        public void ComputeHistoricalSectorialLGD(int computationtype, int curYear, int curPeriod, int prevYear = 0, int prevPeriod = 0)
        {

            var connectionString = GetDataConnection();

            int status = 0;
            string storProc = "";
            if (computationtype == 1)
            {
                storProc = "ifrs_historical_pd_computation";
            }

            else
            {
                storProc = "ifrs_historical_lgd_computation";
            }

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand(storProc, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CurYear",
                    Value = curYear,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "CurPeriod",
                    Value = curPeriod,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PrevYear",
                    Value = prevYear,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "PrevPeriod",
                    Value = prevPeriod,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        #endregion

        //#region ComputedForcastedPDLGD operations

        //[OperationBehavior(TransactionScopeRequired = true)]
        //public ComputedForcastedPDLGD UpdateComputedForcastedPDLGD(ComputedForcastedPDLGD computedForcastedPDLGD)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

        //        ComputedForcastedPDLGD updatedEntity = null;

        //        if (computedForcastedPDLGD.ComputedPDId == 0)
        //            updatedEntity = computedForcastedPDLGDRepository.Add(computedForcastedPDLGD);
        //        else
        //            updatedEntity = computedForcastedPDLGDRepository.Update(computedForcastedPDLGD);

        //        return updatedEntity;
        //    });
        //}

        //[OperationBehavior(TransactionScopeRequired = true)]
        //public void DeleteComputedForcastedPDLGD(int computedForcastedPDLGDId)
        //{
        //    ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

        //        computedForcastedPDLGDRepository.Remove(computedForcastedPDLGDId);
        //    });
        //}

        //public ComputedForcastedPDLGD GetComputedForcastedPDLGD(int computedForcastedPDLGDId)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

        //        ComputedForcastedPDLGD computedForcastedPDLGDEntity = computedForcastedPDLGDRepository.Get(computedForcastedPDLGDId);
        //        if (computedForcastedPDLGDEntity == null)
        //        {
        //            NotFoundException ex = new NotFoundException(string.Format("ComputedForcastedPDLGD with ID of {0} is not in database", computedForcastedPDLGDId));
        //            throw new FaultException<NotFoundException>(ex, ex.Message);
        //        }

        //        return computedForcastedPDLGDEntity;
        //    });
        //}

        //public ComputedForcastedPDLGD[] GetAllComputedForcastedPDLGDs()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

        //        List<ComputedForcastedPDLGD> computedForcastedPDLGDs = computedForcastedPDLGDRepository.GetComputedForcastedPDLGD();

        //        return computedForcastedPDLGDs.ToArray();
        //    });
        //}

        //#endregion

        #region SectorialRegressedPD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SectorialRegressedPD UpdateSectorialRegressedPD(SectorialRegressedPD sectorialRegressedPD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedPDRepository sectorialRegressedPDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedPDRepository>();

                SectorialRegressedPD updatedEntity = null;

                if (sectorialRegressedPD.SectorialRegressedPDId == 0)
                    updatedEntity = sectorialRegressedPDRepository.Add(sectorialRegressedPD);
                else
                    updatedEntity = sectorialRegressedPDRepository.Update(sectorialRegressedPD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSectorialRegressedPD(int sectorialRegressedPDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedPDRepository sectorialRegressedPDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedPDRepository>();

                sectorialRegressedPDRepository.Remove(sectorialRegressedPDId);
            });
        }

        public SectorialRegressedPD GetSectorialRegressedPD(int sectorialRegressedPDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedPDRepository sectorialRegressedPDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedPDRepository>();

                SectorialRegressedPD sectorialRegressedPDEntity = sectorialRegressedPDRepository.Get(sectorialRegressedPDId);
                if (sectorialRegressedPDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SectorialRegressedPD with ID of {0} is not in database", sectorialRegressedPDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorialRegressedPDEntity;
            });
        }

        public SectorialRegressedPD[] GetAllSectorialRegressedPDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedPDRepository sectorialRegressedPDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedPDRepository>();

                IEnumerable<SectorialRegressedPD> sectorialRegressedPDs = sectorialRegressedPDRepository.Get().ToArray();

                return sectorialRegressedPDs.ToArray();
            });
        }

        #endregion

        #region SectorialRegressedLGD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SectorialRegressedLGD UpdateSectorialRegressedLGD(SectorialRegressedLGD sectorialRegressedLGD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedLGDRepository sectorialRegressedLGDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedLGDRepository>();

                SectorialRegressedLGD updatedEntity = null;

                if (sectorialRegressedLGD.SectorialRegressedLGDId == 0)
                    updatedEntity = sectorialRegressedLGDRepository.Add(sectorialRegressedLGD);
                else
                    updatedEntity = sectorialRegressedLGDRepository.Update(sectorialRegressedLGD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSectorialRegressedLGD(int sectorialRegressedLGDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedLGDRepository sectorialRegressedLGDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedLGDRepository>();

                sectorialRegressedLGDRepository.Remove(sectorialRegressedLGDId);
            });
        }

        public SectorialRegressedLGD GetSectorialRegressedLGD(int sectorialRegressedLGDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedLGDRepository sectorialRegressedLGDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedLGDRepository>();

                SectorialRegressedLGD sectorialRegressedLGDEntity = sectorialRegressedLGDRepository.Get(sectorialRegressedLGDId);
                if (sectorialRegressedLGDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SectorialRegressedLGD with ID of {0} is not in database", sectorialRegressedLGDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorialRegressedLGDEntity;
            });
        }

        public SectorialRegressedLGD[] GetAllSectorialRegressedLGDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialRegressedLGDRepository sectorialRegressedLGDRepository = _DataRepositoryFactory.GetDataRepository<ISectorialRegressedLGDRepository>();

                IEnumerable<SectorialRegressedLGD> sectorialRegressedLGDs = sectorialRegressedLGDRepository.Get().ToArray();

                return sectorialRegressedLGDs.ToArray();
            });
        }

        #endregion

        #region MacroEconomicVariable operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacroEconomicVariable UpdateMacroEconomicVariable(MacroEconomicVariable macroEconomicVariable)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicVariableRepository macroEconomicVariableRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicVariableRepository>();

                MacroEconomicVariable updatedEntity = null;

                if (macroEconomicVariable.MacroEconomicVariableId == 0)
                    updatedEntity = macroEconomicVariableRepository.Add(macroEconomicVariable);
                else
                    updatedEntity = macroEconomicVariableRepository.Update(macroEconomicVariable);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacroEconomicVariable(int macroEconomicVariableId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicVariableRepository macroEconomicVariableRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicVariableRepository>();

                macroEconomicVariableRepository.Remove(macroEconomicVariableId);
            });
        }

        public MacroEconomicVariable GetMacroEconomicVariable(int macroEconomicVariableId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicVariableRepository macroEconomicVariableRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicVariableRepository>();

                MacroEconomicVariable macroEconomicVariableEntity = macroEconomicVariableRepository.Get(macroEconomicVariableId);
                if (macroEconomicVariableEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroEconomicVariable with ID of {0} is not in database", macroEconomicVariableId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macroEconomicVariableEntity;
            });
        }

        public MacroEconomicVariable[] GetAllMacroEconomicVariables()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicVariableRepository macroEconomicVariableRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicVariableRepository>();

                IEnumerable<MacroEconomicVariable> macroEconomicVariables = macroEconomicVariableRepository.Get().ToArray();

                return macroEconomicVariables.ToArray();
            });
        }

        #endregion

        #region SectorVariableMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SectorVariableMapping UpdateSectorVariableMapping(SectorVariableMapping sectorVariableMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorVariableMappingRepository sectorVariableMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorVariableMappingRepository>();

                SectorVariableMapping updatedEntity = null;

                if (sectorVariableMapping.SectorVariableMappingId == 0)
                    updatedEntity = sectorVariableMappingRepository.Add(sectorVariableMapping);
                else
                    updatedEntity = sectorVariableMappingRepository.Update(sectorVariableMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSectorVariableMapping(int sectorVariableMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorVariableMappingRepository sectorVariableMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorVariableMappingRepository>();

                sectorVariableMappingRepository.Remove(sectorVariableMappingId);
            });
        }

        public SectorVariableMapping GetSectorVariableMapping(int sectorVariableMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorVariableMappingRepository sectorVariableMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorVariableMappingRepository>();

                SectorVariableMapping sectorVariableMappingEntity = sectorVariableMappingRepository.Get(sectorVariableMappingId);
                if (sectorVariableMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SectorVariableMapping with ID of {0} is not in database", sectorVariableMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorVariableMappingEntity;
            });
        }

        public SectorVariableMappingData[] GetAllSectorVariableMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorVariableMappingRepository sectorVariableMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorVariableMappingRepository>();

                List<SectorVariableMappingData> sectorVariableMappings = new List<SectorVariableMappingData>();
                IEnumerable<SectorVariableMappingInfo> sectorVariableMappingInfos = sectorVariableMappingRepository.GetSectorVariableMappings().ToArray();
               

                foreach (var sectorVariableMappingInfo in sectorVariableMappingInfos)
                {
                     string vtype = "";
                    if (sectorVariableMappingInfo.SectorVariableMapping.Type == 1)
                    {
                        vtype = "PD";
                    }
                    else

                        vtype = "LGD";
                    sectorVariableMappings.Add(
                        new SectorVariableMappingData
                        {
                            SectorVariableMappingId = sectorVariableMappingInfo.SectorVariableMapping.EntityId,
                            Year = sectorVariableMappingInfo.SectorVariableMapping.Year,
                            Sector = sectorVariableMappingInfo.SectorVariableMapping.Sector,
                            SectorName = sectorVariableMappingInfo.Sector.Description,
                            Type = sectorVariableMappingInfo.SectorVariableMapping.Type,                                          
                            TypeName = vtype,
                            Variable = sectorVariableMappingInfo.SectorVariableMapping.Variable,
                            VariableName = sectorVariableMappingInfo.MacroEconomicVariable.Description,
                            Value = sectorVariableMappingInfo.SectorVariableMapping.Value,
                            Active = sectorVariableMappingInfo.SectorVariableMapping.Active
                        });
                }

                return sectorVariableMappings.ToArray();
            });
        }

        #endregion

        #region PitFormular operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PitFormular UpdatePitFormular(PitFormular pitFormular)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPitFormularRepository pitFormularRepository = _DataRepositoryFactory.GetDataRepository<IPitFormularRepository>();

                PitFormular updatedEntity = null;

                if (pitFormular.PitFormularId == 0)
                    updatedEntity = pitFormularRepository.Add(pitFormular);
                else
                    updatedEntity = pitFormularRepository.Update(pitFormular);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePitFormular(int pitFormularId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPitFormularRepository pitFormularRepository = _DataRepositoryFactory.GetDataRepository<IPitFormularRepository>();

                pitFormularRepository.Remove(pitFormularId);
            });
        }

        public PitFormular GetPitFormular(int pitFormularId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPitFormularRepository pitFormularRepository = _DataRepositoryFactory.GetDataRepository<IPitFormularRepository>();

                PitFormular pitFormularEntity = pitFormularRepository.Get(pitFormularId);
                if (pitFormularEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PitFormular with ID of {0} is not in database", pitFormularId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return pitFormularEntity;
            });
        }

        public PitFormular[] GetAllPitFormulars()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPitFormularRepository pitFormularRepository = _DataRepositoryFactory.GetDataRepository<IPitFormularRepository>();

                List<PitFormular> pitFormulars = pitFormularRepository.GetPitFormular();

                return pitFormulars.ToArray();
            });
        }

        #endregion

        #region PortfolioExposure operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PortfolioExposure UpdatePortfolioExposure(PortfolioExposure portfolioExposure)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPortfolioExposureRepository portfolioExposureRepository = _DataRepositoryFactory.GetDataRepository<IPortfolioExposureRepository>();

                PortfolioExposure updatedEntity = null;

                if (portfolioExposure.PortfolioId == 0)
                    updatedEntity = portfolioExposureRepository.Add(portfolioExposure);
                else
                    updatedEntity = portfolioExposureRepository.Update(portfolioExposure);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePortfolioExposure(int portfolioExposureId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPortfolioExposureRepository portfolioExposureRepository = _DataRepositoryFactory.GetDataRepository<IPortfolioExposureRepository>();

                portfolioExposureRepository.Remove(portfolioExposureId);
            });
        }

        public PortfolioExposure GetPortfolioExposure(int portfolioExposureId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPortfolioExposureRepository portfolioExposureRepository = _DataRepositoryFactory.GetDataRepository<IPortfolioExposureRepository>();

                PortfolioExposure portfolioExposureEntity = portfolioExposureRepository.Get(portfolioExposureId);
                if (portfolioExposureEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PortfolioExposure with ID of {0} is not in database", portfolioExposureId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return portfolioExposureEntity;
            });
        }

        public PortfolioExposure[] GetAllPortfolioExposures()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPortfolioExposureRepository portfolioExposureRepository = _DataRepositoryFactory.GetDataRepository<IPortfolioExposureRepository>();

                IEnumerable<PortfolioExposure> portfolioExposures = portfolioExposureRepository.Get().ToArray();

              //  ShowFusionChart(); 
                return portfolioExposures.ToArray();
            });
        }

        public string GetAllPortfolioExposuresChart()
        {
            DataTable dt = new DataTable();
            dt = LoadGrid();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>(); Dictionary<string, object> row;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "Name")
                        row.Add("label", dr[col]);
                    if (col.ColumnName == "Exposure")
                        row.Add("value", dr[col]);
                }
                rows.Add(row);

            }
            return serializer.Serialize(rows);
        }

        public DataTable LoadGrid()
        {

            string cnString = GetDataConnection();
            string sql = "spp_ifrs_get_portfolio_exposure";
            using (SqlConnection cn = new SqlConnection(cnString))
            {
                cn.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    da.SelectCommand.CommandTimeout = 120;
                    da.SelectCommand.CommandText = sql;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }
  
        #endregion

        #region SectorialExposure operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SectorialExposure UpdateSectorialExposure(SectorialExposure sectorialExposure)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<ISectorialExposureRepository>();

                SectorialExposure updatedEntity = null;

                if (sectorialExposure.SectorialExposureId == 0)
                    updatedEntity = sectorialExposureRepository.Add(sectorialExposure);
                else
                    updatedEntity = sectorialExposureRepository.Update(sectorialExposure);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSectorialExposure(int sectorialExposureId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<ISectorialExposureRepository>();

                sectorialExposureRepository.Remove(sectorialExposureId);
            });
        }

        public SectorialExposure GetSectorialExposure(int sectorialExposureId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<ISectorialExposureRepository>();

                SectorialExposure sectorialExposureEntity = sectorialExposureRepository.Get(sectorialExposureId);
                if (sectorialExposureEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SectorialExposure with ID of {0} is not in database", sectorialExposureId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorialExposureEntity;
            });
        }

        public SectorialExposure[] GetAllSectorialExposures()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorialExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<ISectorialExposureRepository>();

                IEnumerable<SectorialExposure> sectorialExposures = sectorialExposureRepository.Get().ToArray();

                return sectorialExposures.ToArray();
            });
        }

        public string GetAllSectorialExposuresChart()
        {
            DataTable dt = new DataTable();
            dt = LoadSectorialGrid();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>(); Dictionary<string, object> row;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "Name")
                        row.Add("label", dr[col]);
                    if (col.ColumnName == "Exposure")
                        row.Add("value", dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }


        public DataTable LoadSectorialGrid()
        {

            string cnString = GetDataConnection();
            string sql = "spp_ifrs_get_sectorial_exposure";
            using (SqlConnection cn = new SqlConnection(cnString))
            {
                cn.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    da.SelectCommand.CommandTimeout = 120;
                    da.SelectCommand.CommandText = sql;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }
        #endregion

        #region PiTPD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PiTPD UpdatePiTPD(PiTPD piTPD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDRepository piTPDRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDRepository>();

                PiTPD updatedEntity = null;

                if (piTPD.PiTPDId == 0)
                    updatedEntity = piTPDRepository.Add(piTPD);
                else
                    updatedEntity = piTPDRepository.Update(piTPD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePiTPD(int piTPDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDRepository piTPDRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDRepository>();

                piTPDRepository.Remove(piTPDId);
            });
        }

        public PiTPD GetPiTPD(int piTPDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDRepository piTPDRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDRepository>();

                PiTPD piTPDEntity = piTPDRepository.Get(piTPDId);
                if (piTPDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PiTPD with ID of {0} is not in database", piTPDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return piTPDEntity;
            });
        }

        public PiTPD[] GetAllPiTPDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDRepository piTPDRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDRepository>();

                IEnumerable<PiTPD> piTPDs = piTPDRepository.Get().ToArray();

                return piTPDs.ToArray();
            });
        }


        public void RegressPD()
        {

            var connectionString = GetDataConnection();

           // storProc = "spp_ifrs_regression_Analysis";
            int status = 0;
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_regression_Analysis", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }
        #endregion

        #region EclCalculationModel operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public EclCalculationModel UpdateEclCalculationModel(EclCalculationModel eclCalculationModel)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclCalculationModelRepository externalRatingRepository = _DataRepositoryFactory.GetDataRepository<IEclCalculationModelRepository>();

                EclCalculationModel updatedEntity = null;

                if (eclCalculationModel.EclModelId == 0)
                    updatedEntity = externalRatingRepository.Add(eclCalculationModel);
                else
                    updatedEntity = externalRatingRepository.Update(eclCalculationModel);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteEclCalculationModel(int eclCalculationModelId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclCalculationModelRepository eclCalculationModelRepository = _DataRepositoryFactory.GetDataRepository<IEclCalculationModelRepository>();

                eclCalculationModelRepository.Remove(eclCalculationModelId);
            });
        }

        public EclCalculationModel GetEclCalculationModel(int eclCalculationModelId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclCalculationModelRepository eclCalculationModelRepository = _DataRepositoryFactory.GetDataRepository<IEclCalculationModelRepository>();

                EclCalculationModel eclCalculationModelEntity = eclCalculationModelRepository.Get(eclCalculationModelId);
                if (eclCalculationModelEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("EclCalculationModel with ID of {0} is not in database", eclCalculationModelId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return eclCalculationModelEntity;
            });
        }

        public EclCalculationModel[] GetAllEclCalculationModels()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclCalculationModelRepository eclCalculationModelRepository = _DataRepositoryFactory.GetDataRepository<IEclCalculationModelRepository>();

                IEnumerable<EclCalculationModel> eclCalculationModels = eclCalculationModelRepository.Get().ToArray();

                return eclCalculationModels.ToArray();
            });
        }

        #endregion

        #region LoanBucketDistribution operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanBucketDistribution UpdateLoanBucketDistribution(LoanBucketDistribution loanSpreadScenario)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanBucketDistributionRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanBucketDistributionRepository>();

                LoanBucketDistribution updatedEntity = null;

                if (loanSpreadScenario.LoanBucketDistributionId == 0)
                    updatedEntity = loanSpreadScenarioRepository.Add(loanSpreadScenario);
                else
                    updatedEntity = loanSpreadScenarioRepository.Update(loanSpreadScenario);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanBucketDistribution(int loanSpreadScenarioId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanBucketDistributionRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanBucketDistributionRepository>();

                loanSpreadScenarioRepository.Remove(loanSpreadScenarioId);
            });
        }

        public LoanBucketDistribution GetLoanBucketDistribution(int loanSpreadScenarioId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanBucketDistributionRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanBucketDistributionRepository>();

                LoanBucketDistribution loanSpreadScenarioEntity = loanSpreadScenarioRepository.Get(loanSpreadScenarioId);
                if (loanSpreadScenarioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanBucketDistribution with ID of {0} is not in database", loanSpreadScenarioId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanSpreadScenarioEntity;
            });
        }

        public LoanBucketDistribution[] GetAllLoanBucketDistributions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanBucketDistributionRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanBucketDistributionRepository>();

                IEnumerable<LoanBucketDistribution> loanSpreadScenarios = loanSpreadScenarioRepository.Get().ToArray();

                return loanSpreadScenarios.ToArray();
            });
        }

        public void PDDistribution()
        {

            var connectionString = GetDataConnection();

            // storProc = "spp_ifrs_regression_Analysis";
            int status = 0;
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_loan_classification_spread_pd", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        public void PastDueDayDistribution()
        {

            var connectionString = GetDataConnection();

            // storProc = "spp_ifrs_regression_Analysis";
            int status = 0;
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_loan_classification_spread_pastduedays", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        public LoanBucketDistribution[] GetLoanAssessments(string bucket)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanBucketDistributionRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanBucketDistributionRepository>();

                IEnumerable<LoanBucketDistribution> loanSpreadScenarios = loanSpreadScenarioRepository.GetLoanAssessments(bucket).ToArray();

                return loanSpreadScenarios.ToArray();
            });
        }


        #endregion

        #region MacroeconomicVDisplay operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacroeconomicVDisplay UpdateMacroeconomicVDisplay(MacroeconomicVDisplay macroeconomicVDisplay)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicVDisplayRepository macroeconomicVDisplayRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicVDisplayRepository>();

                MacroeconomicVDisplay updatedEntity = null;

                if (macroeconomicVDisplay.MacroVariableDisplayId == 0)
                    updatedEntity = macroeconomicVDisplayRepository.Add(macroeconomicVDisplay);
                else
                    updatedEntity = macroeconomicVDisplayRepository.Update(macroeconomicVDisplay);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacroeconomicVDisplay(int macroeconomicVDisplayId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicVDisplayRepository macroeconomicVDisplayRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicVDisplayRepository>();

                macroeconomicVDisplayRepository.Remove(macroeconomicVDisplayId);
            });
        }

        public MacroeconomicVDisplay GetMacroeconomicVDisplay(int macroeconomicVDisplayId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicVDisplayRepository macroeconomicVDisplayRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicVDisplayRepository>();

                MacroeconomicVDisplay macroeconomicVDisplayEntity = macroeconomicVDisplayRepository.Get(macroeconomicVDisplayId);
                if (macroeconomicVDisplayEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroeconomicVDisplay with ID of {0} is not in database", macroeconomicVDisplayId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macroeconomicVDisplayEntity;
            });
        }

        public MacroeconomicVDisplay[] GetAllMacroeconomicVDisplays()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicVDisplayRepository macroeconomicVDisplayRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicVDisplayRepository>();

                IEnumerable<MacroeconomicVDisplay> macroeconomicVDisplays = macroeconomicVDisplayRepository.Get().ToArray();

                return macroeconomicVDisplays.ToArray();
            });
        }

        public string[] GetDistinctFHYear(string vType)
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var yearList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_get_distinct_FHyear", con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "VType",
                    Value = vType,
                });
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var years = new ReferenceNoModel();
                        if (reader["Year"] != DBNull.Value)
                            years.RefNo = reader["Year"].ToString();
                        yearList.Add(years.RefNo);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return yearList.ToArray();
        }

        public MacroeconomicVDisplay[] GetMacroeconomicVDisplayByYear(int yr)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroeconomicVDisplayRepository macroeconomicVDisplayRepository = _DataRepositoryFactory.GetDataRepository<IMacroeconomicVDisplayRepository>();

                IEnumerable<MacroeconomicVDisplay> macroeconomicVDisplays = macroeconomicVDisplayRepository.GetMacroeconomicVDisplayByYear(yr).ToList();

                return macroeconomicVDisplays.ToArray();
            });
        }
        #endregion

        #region LifeTimePDClassification operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LifeTimePDClassification UpdateLifeTimePDClassification(LifeTimePDClassification lifeTimePDClassification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILifeTimePDClassificationRepository lifeTimePDClassificationRepository = _DataRepositoryFactory.GetDataRepository<ILifeTimePDClassificationRepository>();

                LifeTimePDClassification updatedEntity = null;

                if (lifeTimePDClassification.LifeTimePDClassificationId == 0)
                    updatedEntity = lifeTimePDClassificationRepository.Add(lifeTimePDClassification);
                else
                    updatedEntity = lifeTimePDClassificationRepository.Update(lifeTimePDClassification);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLifeTimePDClassification(int lifeTimePDClassificationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILifeTimePDClassificationRepository lifeTimePDClassificationRepository = _DataRepositoryFactory.GetDataRepository<ILifeTimePDClassificationRepository>();

                lifeTimePDClassificationRepository.Remove(lifeTimePDClassificationId);
            });
        }

        public LifeTimePDClassification GetLifeTimePDClassification(int lifeTimePDClassificationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILifeTimePDClassificationRepository lifeTimePDClassificationRepository = _DataRepositoryFactory.GetDataRepository<ILifeTimePDClassificationRepository>();

                LifeTimePDClassification lifeTimePDClassificationEntity = lifeTimePDClassificationRepository.Get(lifeTimePDClassificationId);
                if (lifeTimePDClassificationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LifeTimePDClassification with ID of {0} is not in database", lifeTimePDClassificationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return lifeTimePDClassificationEntity;
            });
        }

        public LifeTimePDClassification[] GetAllLifeTimePDClassifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                PopulateAssesmentTable();

                ILifeTimePDClassificationRepository lifeTimePDClassificationRepository = _DataRepositoryFactory.GetDataRepository<ILifeTimePDClassificationRepository>();

                IEnumerable<LifeTimePDClassification> lifeTimePDClassifications = lifeTimePDClassificationRepository.Get().ToArray();

                return lifeTimePDClassifications.ToArray();
            });
        }


        public void PopulateAssesmentTable()
        {

            var connectionString = GetDataConnection();

            // storProc = "spp_ifrs_regression_Analysis";
            int status = 0;
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_loan_assessment", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }
        #endregion

        #region SummaryReport operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SummaryReport UpdateSummaryReport(SummaryReport summaryReport)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISummaryReportRepository summaryReportRepository = _DataRepositoryFactory.GetDataRepository<ISummaryReportRepository>();

                SummaryReport updatedEntity = null;

                if (summaryReport.SummaryReportId == 0)
                    updatedEntity = summaryReportRepository.Add(summaryReport);
                else
                    updatedEntity = summaryReportRepository.Update(summaryReport);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSummaryReport(int summaryReportId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISummaryReportRepository summaryReportRepository = _DataRepositoryFactory.GetDataRepository<ISummaryReportRepository>();

                summaryReportRepository.Remove(summaryReportId);
            });
        }

        public SummaryReport GetSummaryReport(int summaryReportId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISummaryReportRepository summaryReportRepository = _DataRepositoryFactory.GetDataRepository<ISummaryReportRepository>();

                SummaryReport summaryReportEntity = summaryReportRepository.Get(summaryReportId);
                if (summaryReportEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SummaryReport with ID of {0} is not in database", summaryReportId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return summaryReportEntity;
            });
        }

        public SummaryReport[] GetAllSummaryReports()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISummaryReportRepository summaryReportRepository = _DataRepositoryFactory.GetDataRepository<ISummaryReportRepository>();

                IEnumerable<SummaryReport> summaryReports = summaryReportRepository.Get().ToArray();

                //  ShowFusionChart(); 
                return summaryReports.ToArray();
            });
        }

        public string GetAllSummaryReportsChart()
        {
            DataTable dt = new DataTable();
            dt = LoadSummaryGrid();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>(); Dictionary<string, object> row;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "Bucket")
                        row.Add("label", dr[col]);
                    if (col.ColumnName == "Individual")
                        row.Add("value", dr[col]);
                    if (col.ColumnName == "Collective")
                     row.Add("value2", dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        public DataTable LoadSummaryGrid()
        {

            string cnString = GetDataConnection();
            string sql = "spp_ifrs_get_summary_report";
            using (SqlConnection cn = new SqlConnection(cnString))
            {
                cn.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    da.SelectCommand.CommandTimeout = 120;
                    da.SelectCommand.CommandText = sql;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }

        #endregion

        #region FairValuationModel operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FairValuationModel UpdateFairValuationModel(FairValuationModel fairValuationModel)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValuationModelRepository externalRatingRepository = _DataRepositoryFactory.GetDataRepository<IFairValuationModelRepository>();

                FairValuationModel updatedEntity = null;

                if (fairValuationModel.FairValueModelId == 0)
                    updatedEntity = externalRatingRepository.Add(fairValuationModel);
                else
                    updatedEntity = externalRatingRepository.Update(fairValuationModel);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFairValuationModel(int fairValuationModelId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValuationModelRepository fairValuationModelRepository = _DataRepositoryFactory.GetDataRepository<IFairValuationModelRepository>();

                fairValuationModelRepository.Remove(fairValuationModelId);
            });
        }

        public FairValuationModel GetFairValuationModel(int fairValuationModelId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValuationModelRepository fairValuationModelRepository = _DataRepositoryFactory.GetDataRepository<IFairValuationModelRepository>();

                FairValuationModel fairValuationModelEntity = fairValuationModelRepository.Get(fairValuationModelId);
                if (fairValuationModelEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FairValuationModel with ID of {0} is not in database", fairValuationModelId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return fairValuationModelEntity;
            });
        }

        public FairValuationModel[] GetAllFairValuationModels()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFairValuationModelRepository fairValuationModelRepository = _DataRepositoryFactory.GetDataRepository<IFairValuationModelRepository>();

                IEnumerable<FairValuationModel> fairValuationModels = fairValuationModelRepository.Get().ToArray();

                return fairValuationModels.ToArray();
            });
        }

        #endregion

        #region IfrsEquityUnqouted operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsEquityUnqouted UpdateIfrsEquityUnqouted(IfrsEquityUnqouted ifrsEquityUnqouted)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsEquityUnqoutedRepository ifrsEquityUnqoutedRepository = _DataRepositoryFactory.GetDataRepository<IIfrsEquityUnqoutedRepository>();

                IfrsEquityUnqouted updatedEntity = null;

                if (ifrsEquityUnqouted.IfrsEquityUnqoutedId == 0)
                    updatedEntity = ifrsEquityUnqoutedRepository.Add(ifrsEquityUnqouted);
                else
                    updatedEntity = ifrsEquityUnqoutedRepository.Update(ifrsEquityUnqouted);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsEquityUnqouted(int ifrsEquityUnqoutedId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsEquityUnqoutedRepository ifrsEquityUnqoutedRepository = _DataRepositoryFactory.GetDataRepository<IIfrsEquityUnqoutedRepository>();

                ifrsEquityUnqoutedRepository.Remove(ifrsEquityUnqoutedId);
            });
        }

        public IfrsEquityUnqouted GetIfrsEquityUnqouted(int ifrsEquityUnqoutedId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsEquityUnqoutedRepository ifrsEquityUnqoutedRepository = _DataRepositoryFactory.GetDataRepository<IIfrsEquityUnqoutedRepository>();

                IfrsEquityUnqouted ifrsEquityUnqoutedEntity = ifrsEquityUnqoutedRepository.Get(ifrsEquityUnqoutedId);
                if (ifrsEquityUnqoutedEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsEquityUnqouted with ID of {0} is not in database", ifrsEquityUnqoutedId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsEquityUnqoutedEntity;
            });
        }

        public IfrsEquityUnqouted[] GetAllIfrsEquityUnqouteds()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsEquityUnqoutedRepository ifrsEquityUnqoutedRepository = _DataRepositoryFactory.GetDataRepository<IIfrsEquityUnqoutedRepository>();

                IEnumerable<IfrsEquityUnqouted> ifrsEquityUnqouteds = ifrsEquityUnqoutedRepository.Get().ToArray();

                return ifrsEquityUnqouteds.ToArray();
            });
        }

        #endregion

        #region IfrsStocksPrimaryData operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsStocksPrimaryData UpdateIfrsStocksPrimaryData(IfrsStocksPrimaryData ifrsStocksPrimaryData)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksPrimaryDataRepository ifrsStocksPrimaryDataRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksPrimaryDataRepository>();

                IfrsStocksPrimaryData updatedEntity = null;

                if (ifrsStocksPrimaryData.IfrsStocksPrimaryDataId == 0)
                    updatedEntity = ifrsStocksPrimaryDataRepository.Add(ifrsStocksPrimaryData);
                else
                    updatedEntity = ifrsStocksPrimaryDataRepository.Update(ifrsStocksPrimaryData);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsStocksPrimaryData(int ifrsStocksPrimaryDataId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksPrimaryDataRepository ifrsStocksPrimaryDataRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksPrimaryDataRepository>();

                ifrsStocksPrimaryDataRepository.Remove(ifrsStocksPrimaryDataId);
            });
        }

        public IfrsStocksPrimaryData GetIfrsStocksPrimaryData(int ifrsStocksPrimaryDataId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksPrimaryDataRepository ifrsStocksPrimaryDataRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksPrimaryDataRepository>();

                IfrsStocksPrimaryData ifrsStocksPrimaryDataEntity = ifrsStocksPrimaryDataRepository.Get(ifrsStocksPrimaryDataId);
                if (ifrsStocksPrimaryDataEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsStocksPrimaryData with ID of {0} is not in database", ifrsStocksPrimaryDataId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsStocksPrimaryDataEntity;
            });
        }

        public IfrsStocksPrimaryData[] GetAllIfrsStocksPrimaryDatas()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksPrimaryDataRepository ifrsStocksPrimaryDataRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksPrimaryDataRepository>();

                IEnumerable<IfrsStocksPrimaryData> ifrsStocksPrimaryDatas = ifrsStocksPrimaryDataRepository.Get().ToArray();

                return ifrsStocksPrimaryDatas.ToArray();
            });
        }

        #endregion

        #region IfrsStocksMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IfrsStocksMapping UpdateIfrsStocksMapping(IfrsStocksMapping ifrsStocksMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksMappingRepository ifrsStocksMappingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksMappingRepository>();

                IfrsStocksMapping updatedEntity = null;

                if (ifrsStocksMapping.IfrsStocksMappingId == 0)
                    updatedEntity = ifrsStocksMappingRepository.Add(ifrsStocksMapping);
                else
                    updatedEntity = ifrsStocksMappingRepository.Update(ifrsStocksMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIfrsStocksMapping(int ifrsStocksMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksMappingRepository ifrsStocksMappingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksMappingRepository>();

                ifrsStocksMappingRepository.Remove(ifrsStocksMappingId);
            });
        }

        public IfrsStocksMapping GetIfrsStocksMapping(int ifrsStocksMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksMappingRepository ifrsStocksMappingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksMappingRepository>();

                IfrsStocksMapping ifrsStocksMappingEntity = ifrsStocksMappingRepository.Get(ifrsStocksMappingId);
                if (ifrsStocksMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IfrsStocksMapping with ID of {0} is not in database", ifrsStocksMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifrsStocksMappingEntity;
            });
        }

        public IfrsStocksMappingData[] GetAllIfrsStocksMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIfrsStocksMappingRepository ifrsStocksMappingRepository = _DataRepositoryFactory.GetDataRepository<IIfrsStocksMappingRepository>();

                List<IfrsStocksMappingData> ifrsstocksMappings = new List<IfrsStocksMappingData>();
                IEnumerable<IfrsStocksMappingInfo> ifrsstocksMappingInfos = ifrsStocksMappingRepository.GetIfrsStocksMappings().ToArray();

                foreach (var ifrsstocksMappingInfo in ifrsstocksMappingInfos)
                {
                    ifrsstocksMappings.Add(
                        new IfrsStocksMappingData
                        {
                            IfrsStocksMappingId = ifrsstocksMappingInfo.IfrsEquityUnqouted.EntityId,
                            Qouted_stock_code = ifrsstocksMappingInfo.IfrsStocksPrimaryData.Stock_code,
                            Qouted_stock_Name = ifrsstocksMappingInfo.IfrsStocksPrimaryData.Company_name,
                            Unqouted_stock_code = ifrsstocksMappingInfo.IfrsEquityUnqouted.Stock_code,
                            Unqouted_stock_Name = ifrsstocksMappingInfo.IfrsEquityUnqouted.Stock_description,
                            Active = ifrsstocksMappingInfo.IfrsStocksMapping.Active
                        });
                }


                return ifrsstocksMappings.ToArray();
            });
        }


        #endregion

        #region Reconciliation operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Reconciliation UpdateReconciliation(Reconciliation reconciliation)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IReconciliationRepository reconciliationRepository = _DataRepositoryFactory.GetDataRepository<IReconciliationRepository>();

                Reconciliation updatedEntity = null;

                if (reconciliation.ReconciliationId == 0)
                    updatedEntity = reconciliationRepository.Add(reconciliation);
                else
                    updatedEntity = reconciliationRepository.Update(reconciliation);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteReconciliation(int reconciliationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IReconciliationRepository reconciliationRepository = _DataRepositoryFactory.GetDataRepository<IReconciliationRepository>();

                reconciliationRepository.Remove(reconciliationId);
            });
        }

        public Reconciliation GetReconciliation(int reconciliationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IReconciliationRepository reconciliationRepository = _DataRepositoryFactory.GetDataRepository<IReconciliationRepository>();

                Reconciliation reconciliationEntity = reconciliationRepository.Get(reconciliationId);
                if (reconciliationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Reconciliation with ID of {0} is not in database", reconciliationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return reconciliationEntity;
            });
        }

        public Reconciliation[] GetAllReconciliations()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IReconciliationRepository reconciliationRepository = _DataRepositoryFactory.GetDataRepository<IReconciliationRepository>();

                IEnumerable<Reconciliation> reconciliations = reconciliationRepository.Get().OrderBy(c => c.ReconciliationId).ToArray();

                return reconciliations.ToArray();
            });
        }

        #endregion

        #region ForecastedMacroeconimcsSensitivity operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ForecastedMacroeconimcsSensitivity UpdateForecastedMacroeconimcsSensitivity(ForecastedMacroeconimcsSensitivity forecastedMacroeconimcsSensitivity)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsSensitivityRepository forecastedMacroeconimcsSensitivityRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsSensitivityRepository>();

                ForecastedMacroeconimcsSensitivity updatedEntity = null;

                if (forecastedMacroeconimcsSensitivity.ForecastedMacroeconimcsSensitivityId == 0)
                    updatedEntity = forecastedMacroeconimcsSensitivityRepository.Add(forecastedMacroeconimcsSensitivity);
                else
                    updatedEntity = forecastedMacroeconimcsSensitivityRepository.Update(forecastedMacroeconimcsSensitivity);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteForecastedMacroeconimcsSensitivity(int forecastedMacroeconimcsSensitivityId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsSensitivityRepository forecastedMacroeconimcsSensitivityRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsSensitivityRepository>();

                forecastedMacroeconimcsSensitivityRepository.Remove(forecastedMacroeconimcsSensitivityId);
            });
        }

        public ForecastedMacroeconimcsSensitivity GetForecastedMacroeconimcsSensitivity(int forecastedMacroeconimcsSensitivityId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsSensitivityRepository forecastedMacroeconimcsSensitivityRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsSensitivityRepository>();

                ForecastedMacroeconimcsSensitivity forecastedMacroeconimcsSensitivityEntity = forecastedMacroeconimcsSensitivityRepository.Get(forecastedMacroeconimcsSensitivityId);
                if (forecastedMacroeconimcsSensitivityEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ForecastedMacroeconimcsSensitivity with ID of {0} is not in database", forecastedMacroeconimcsSensitivityId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return forecastedMacroeconimcsSensitivityEntity;
            });
        }

        public ForecastedMacroeconimcsSensitivityData[] GetAllForecastedMacroeconimcsSensitivitys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsSensitivityRepository forecastedMacroeconimcsSensitivityRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsSensitivityRepository>();

                List<ForecastedMacroeconimcsSensitivityData> forecastedMacroeconimcsSensitivitys = new List<ForecastedMacroeconimcsSensitivityData>();
                IEnumerable<ForecastedMacroeconimcsSensitivityInfo> forecastedMacroeconimcsSensitivityInfos = forecastedMacroeconimcsSensitivityRepository.GetForecastedMacroeconimcsSensitivitys().ToArray();


                foreach (var forecastedMacroeconimcsSensitivityInfo in forecastedMacroeconimcsSensitivityInfos)
                {
                    string vtype = "";
                    if (forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Type == 1)
                    {
                        vtype = "PD";
                    }
                    else

                        vtype = "LGD";
                    forecastedMacroeconimcsSensitivitys.Add(
                        new ForecastedMacroeconimcsSensitivityData
                        {
                            ForecastedMacroeconimcsSensitivityId = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.EntityId,
                            Year = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Year,
                            Sector = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Sector_Code,
                            SectorName = forecastedMacroeconimcsSensitivityInfo.Sector.Description,
                            Type = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Type,
                            TypeName = vtype,
                            Variable = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Variable,
                            VariableName = forecastedMacroeconimcsSensitivityInfo.MacroEconomicVariable.Description,
                            Value = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Value,
                            Active = forecastedMacroeconimcsSensitivityInfo.ForecastedMacroeconimcsSensitivity.Active
                        });
                }

                return forecastedMacroeconimcsSensitivitys.ToArray();
            });
        }


        public void InsertSensitivityData(string microeconomic, int year, int types, float values)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_Forecasted_Macroeconimcs_Sensitivity_Add", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Microeconomic",
                    Value = microeconomic,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = year,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Type",
                    Value = types,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Value",
                    Value = values,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }
        public void ComputeSensitivity()
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_regression_Analysis_Sensitivity", con);//spp_ifrs_regression_Analysis_Sensitivity
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        #endregion

        #region BucketExposure operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BucketExposure UpdateBucketExposure(BucketExposure sectorialExposure)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBucketExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<IBucketExposureRepository>();

                BucketExposure updatedEntity = null;

                if (sectorialExposure.BucketExposureId == 0)
                    updatedEntity = sectorialExposureRepository.Add(sectorialExposure);
                else
                    updatedEntity = sectorialExposureRepository.Update(sectorialExposure);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBucketExposure(int sectorialExposureId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBucketExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<IBucketExposureRepository>();

                sectorialExposureRepository.Remove(sectorialExposureId);
            });
        }

        public BucketExposure GetBucketExposure(int sectorialExposureId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBucketExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<IBucketExposureRepository>();

                BucketExposure sectorialExposureEntity = sectorialExposureRepository.Get(sectorialExposureId);
                if (sectorialExposureEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BucketExposure with ID of {0} is not in database", sectorialExposureId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorialExposureEntity;
            });
        }

        public BucketExposure[] GetAllBucketExposures()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBucketExposureRepository sectorialExposureRepository = _DataRepositoryFactory.GetDataRepository<IBucketExposureRepository>();

                IEnumerable<BucketExposure> sectorialExposures = sectorialExposureRepository.Get().ToArray();

                return sectorialExposures.ToArray();
            });
        }

        public string GetAllBucketExposuresChart()
        {
            DataTable dt = new DataTable();
            dt = LoadBucketGrid();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>(); Dictionary<string, object> row;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "Name")
                        row.Add("label", dr[col]);
                    if (col.ColumnName == "Exposure")
                        row.Add("value", dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }


        public DataTable LoadBucketGrid()
        {

            string cnString = GetDataConnection();
            string sql = "spp_ifrs_get_bucket_exposure";
            using (SqlConnection cn = new SqlConnection(cnString))
            {
                cn.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    da.SelectCommand.CommandTimeout = 120;
                    da.SelectCommand.CommandText = sql;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }
        #endregion

        #region ForecastedMacroeconimcsScenario operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ForecastedMacroeconimcsScenario UpdateForecastedMacroeconimcsScenario(ForecastedMacroeconimcsScenario forecastedMacroeconimcsScenario)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsScenarioRepository forecastedMacroeconimcsScenarioRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsScenarioRepository>();

                ForecastedMacroeconimcsScenario updatedEntity = null;

                if (forecastedMacroeconimcsScenario.ForecastedMacroeconimcsScenarioId == 0)
                    updatedEntity = forecastedMacroeconimcsScenarioRepository.Add(forecastedMacroeconimcsScenario);
                else
                    updatedEntity = forecastedMacroeconimcsScenarioRepository.Update(forecastedMacroeconimcsScenario);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteForecastedMacroeconimcsScenario(int forecastedMacroeconimcsScenarioId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsScenarioRepository forecastedMacroeconimcsScenarioRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsScenarioRepository>();

                forecastedMacroeconimcsScenarioRepository.Remove(forecastedMacroeconimcsScenarioId);
            });
        }

        public ForecastedMacroeconimcsScenario GetForecastedMacroeconimcsScenario(int forecastedMacroeconimcsScenarioId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsScenarioRepository forecastedMacroeconimcsScenarioRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsScenarioRepository>();

                ForecastedMacroeconimcsScenario forecastedMacroeconimcsScenarioEntity = forecastedMacroeconimcsScenarioRepository.Get(forecastedMacroeconimcsScenarioId);
                if (forecastedMacroeconimcsScenarioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ForecastedMacroeconimcsScenario with ID of {0} is not in database", forecastedMacroeconimcsScenarioId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return forecastedMacroeconimcsScenarioEntity;
            });
        }

        public ForecastedMacroeconimcsScenarioData[] GetAllForecastedMacroeconimcsScenarios()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForecastedMacroeconimcsScenarioRepository forecastedMacroeconimcsScenarioRepository = _DataRepositoryFactory.GetDataRepository<IForecastedMacroeconimcsScenarioRepository>();

                List<ForecastedMacroeconimcsScenarioData> forecastedMacroeconimcsScenarios = new List<ForecastedMacroeconimcsScenarioData>();
                IEnumerable<ForecastedMacroeconimcsScenarioInfo> forecastedMacroeconimcsScenarioInfos = forecastedMacroeconimcsScenarioRepository.GetForecastedMacroeconimcsScenarios().ToArray();


                foreach (var forecastedMacroeconimcsScenarioInfo in forecastedMacroeconimcsScenarioInfos)
                {
                    string vtype = "";
                    if (forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Type == 1)
                    {
                        vtype = "PD";
                    }
                    else

                        vtype = "LGD";
                    forecastedMacroeconimcsScenarios.Add(
                        new ForecastedMacroeconimcsScenarioData
                        {
                            ForecastedMacroeconimcsScenarioId = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.EntityId,
                            Year = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Year,
                            Sector = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Sector_Code,
                            SectorName = forecastedMacroeconimcsScenarioInfo.Sector.Description,
                            Type = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Type,
                            TypeName = vtype,
                            Variable = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Variable,
                            VariableName = forecastedMacroeconimcsScenarioInfo.MacroEconomicVariable.Description,
                            Value = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Value,
                            Active = forecastedMacroeconimcsScenarioInfo.ForecastedMacroeconimcsScenario.Active
                        });
                }

                return forecastedMacroeconimcsScenarios.ToArray();
            });
        }


        public void InsertScenarioData(string sector, string microeconomic, int year, int types, float values)
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_Forecasted_Macroeconimcs_Scenario_Add", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Sector",
                    Value = sector,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Microeconomic",
                    Value = microeconomic,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Year",
                    Value = year,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Type",
                    Value = types,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Value",
                    Value = values,
                });

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        public void ComputeScenario()
        {

            var connectionString = GetDataConnection();

            int status = 0;

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_ifrs_regression_Analysis_Scenario", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                

                con.Open();

                status = cmd.ExecuteNonQuery();

                con.Close();
            }


        }

        #endregion

        #region LoanSpreadSensitivity operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanSpreadSensitivity UpdateLoanSpreadSensitivity(LoanSpreadSensitivity loanSpreadSensitivity)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadSensitivityRepository loanSpreadSensitivityRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadSensitivityRepository>();

                LoanSpreadSensitivity updatedEntity = null;

                if (loanSpreadSensitivity.LoanSpreadSensitivityId == 0)
                    updatedEntity = loanSpreadSensitivityRepository.Add(loanSpreadSensitivity);
                else
                    updatedEntity = loanSpreadSensitivityRepository.Update(loanSpreadSensitivity);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanSpreadSensitivity(int loanSpreadSensitivityId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadSensitivityRepository loanSpreadSensitivityRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadSensitivityRepository>();

                loanSpreadSensitivityRepository.Remove(loanSpreadSensitivityId);
            });
        }

        public LoanSpreadSensitivity GetLoanSpreadSensitivity(int loanSpreadSensitivityId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadSensitivityRepository loanSpreadSensitivityRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadSensitivityRepository>();

                LoanSpreadSensitivity loanSpreadSensitivityEntity = loanSpreadSensitivityRepository.Get(loanSpreadSensitivityId);
                if (loanSpreadSensitivityEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanSpreadSensitivity with ID of {0} is not in database", loanSpreadSensitivityId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanSpreadSensitivityEntity;
            });
        }

        public LoanSpreadSensitivity[] GetAllLoanSpreadSensitivitys()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadSensitivityRepository loanSpreadSensitivityRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadSensitivityRepository>();

                IEnumerable<LoanSpreadSensitivity> loanSpreadSensitivitys = loanSpreadSensitivityRepository.Get().ToArray();

                return loanSpreadSensitivitys.ToArray();
            });
        }



        #endregion

        #region LoanSpreadScenario operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LoanSpreadScenario UpdateLoanSpreadScenario(LoanSpreadScenario loanSpreadScenario)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadScenarioRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadScenarioRepository>();

                LoanSpreadScenario updatedEntity = null;

                if (loanSpreadScenario.LoanSpreadScenarioId == 0)
                    updatedEntity = loanSpreadScenarioRepository.Add(loanSpreadScenario);
                else
                    updatedEntity = loanSpreadScenarioRepository.Update(loanSpreadScenario);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLoanSpreadScenario(int loanSpreadScenarioId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadScenarioRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadScenarioRepository>();

                loanSpreadScenarioRepository.Remove(loanSpreadScenarioId);
            });
        }

        public LoanSpreadScenario GetLoanSpreadScenario(int loanSpreadScenarioId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadScenarioRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadScenarioRepository>();

                LoanSpreadScenario loanSpreadScenarioEntity = loanSpreadScenarioRepository.Get(loanSpreadScenarioId);
                if (loanSpreadScenarioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LoanSpreadScenario with ID of {0} is not in database", loanSpreadScenarioId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return loanSpreadScenarioEntity;
            });
        }

        public LoanSpreadScenario[] GetAllLoanSpreadScenarios()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILoanSpreadScenarioRepository loanSpreadScenarioRepository = _DataRepositoryFactory.GetDataRepository<ILoanSpreadScenarioRepository>();

                IEnumerable<LoanSpreadScenario> loanSpreadScenarios = loanSpreadScenarioRepository.Get().ToArray();

                return loanSpreadScenarios.ToArray();
            });
        }


        #endregion

        #region UnquotedEquityFairvalueResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public UnquotedEquityFairvalueResult UpdateUnquotedEquityFairvalueResult(UnquotedEquityFairvalueResult unquotedEquityFairvalueResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUnquotedEquityFairvalueResultRepository unquotedEquityFairvalueResultRepository = _DataRepositoryFactory.GetDataRepository<IUnquotedEquityFairvalueResultRepository>();

                UnquotedEquityFairvalueResult updatedEntity = null;

                if (unquotedEquityFairvalueResult.UnquotedEquityFairvalueResultId == 0)
                    updatedEntity = unquotedEquityFairvalueResultRepository.Add(unquotedEquityFairvalueResult);
                else
                    updatedEntity = unquotedEquityFairvalueResultRepository.Update(unquotedEquityFairvalueResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUnquotedEquityFairvalueResult(int unquotedEquityFairvalueResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUnquotedEquityFairvalueResultRepository unquotedEquityFairvalueResultRepository = _DataRepositoryFactory.GetDataRepository<IUnquotedEquityFairvalueResultRepository>();

                unquotedEquityFairvalueResultRepository.Remove(unquotedEquityFairvalueResultId);
            });
        }

        public UnquotedEquityFairvalueResult GetUnquotedEquityFairvalueResult(int unquotedEquityFairvalueResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUnquotedEquityFairvalueResultRepository unquotedEquityFairvalueResultRepository = _DataRepositoryFactory.GetDataRepository<IUnquotedEquityFairvalueResultRepository>();

                UnquotedEquityFairvalueResult unquotedEquityFairvalueResultEntity = unquotedEquityFairvalueResultRepository.Get(unquotedEquityFairvalueResultId);
                if (unquotedEquityFairvalueResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UnquotedEquityFairvalueResult with ID of {0} is not in database", unquotedEquityFairvalueResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return unquotedEquityFairvalueResultEntity;
            });
        }

        public UnquotedEquityFairvalueResult[] GetAllUnquotedEquityFairvalueResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUnquotedEquityFairvalueResultRepository unquotedEquityFairvalueResultRepository = _DataRepositoryFactory.GetDataRepository<IUnquotedEquityFairvalueResultRepository>();

                IEnumerable<UnquotedEquityFairvalueResult> unquotedEquityFairvalueResults = unquotedEquityFairvalueResultRepository.Get().ToArray();

                return unquotedEquityFairvalueResults.ToArray();
            });
        }

        #endregion

        #region PiTPDComparism operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PiTPDComparism UpdatePiTPDComparism(PiTPDComparism piTPDComparism)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDComparismRepository piTPDComparismRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDComparismRepository>();

                PiTPDComparism updatedEntity = null;

                if (piTPDComparism.ComparismPDId == 0)
                    updatedEntity = piTPDComparismRepository.Add(piTPDComparism);
                else
                    updatedEntity = piTPDComparismRepository.Update(piTPDComparism);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePiTPDComparism(int piTPDComparismId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDComparismRepository piTPDComparismRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDComparismRepository>();

                piTPDComparismRepository.Remove(piTPDComparismId);
            });
        }

        public PiTPDComparism GetPiTPDComparism(int piTPDComparismId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDComparismRepository piTPDComparismRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDComparismRepository>();

                PiTPDComparism piTPDComparismEntity = piTPDComparismRepository.Get(piTPDComparismId);
                if (piTPDComparismEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PiTPDComparism with ID of {0} is not in database", piTPDComparismId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return piTPDComparismEntity;
            });
        }

        public PiTPDComparism[] GetAllPiTPDComparisms()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPiTPDComparismRepository piTPDComparismRepository = _DataRepositoryFactory.GetDataRepository<IPiTPDComparismRepository>();

                IEnumerable<PiTPDComparism> piTPDComparisms = piTPDComparismRepository.Get().Where(c => c.Type == "SENSITIVITY").ToArray();

                return piTPDComparisms.ToArray();
            });
        }


        #endregion

        #region MarkovMatrix operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MarkovMatrix UpdateMarkovMatrix(MarkovMatrix markovMatrix)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarkovMatrixRepository markovMatrixRepository = _DataRepositoryFactory.GetDataRepository<IMarkovMatrixRepository>();

                MarkovMatrix updatedEntity = null;

                if (markovMatrix.MarkovMatrixId == 0)
                    updatedEntity = markovMatrixRepository.Add(markovMatrix);
                else
                    updatedEntity = markovMatrixRepository.Update(markovMatrix);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMarkovMatrix(int markovMatrixId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarkovMatrixRepository markovMatrixRepository = _DataRepositoryFactory.GetDataRepository<IMarkovMatrixRepository>();

                markovMatrixRepository.Remove(markovMatrixId);
            });
        }

        public MarkovMatrix GetMarkovMatrix(int markovMatrixId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarkovMatrixRepository markovMatrixRepository = _DataRepositoryFactory.GetDataRepository<IMarkovMatrixRepository>();

                MarkovMatrix markovMatrixEntity = markovMatrixRepository.Get(markovMatrixId);
                if (markovMatrixEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MarkovMatrix with ID of {0} is not in database", markovMatrixId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return markovMatrixEntity;
            });
        }

        public MarkovMatrix[] GetAllMarkovMatrixs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarkovMatrixRepository markovMatrixRepository = _DataRepositoryFactory.GetDataRepository<IMarkovMatrixRepository>();

                IEnumerable<MarkovMatrix> markovMatrixs = markovMatrixRepository.Get().ToArray();

                return markovMatrixs.ToArray();
            });
        }


        public MarkovMatrixData[] GetMarkovMatrixs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarkovMatrixRepository markovMatrixRepository = _DataRepositoryFactory.GetDataRepository<IMarkovMatrixRepository>();


                List<MarkovMatrixData> markovMatrixs = new List<MarkovMatrixData>();
                IEnumerable<MarkovMatrixInfo> markovMatrixInfos = markovMatrixRepository.GetMarkovMatrixs().ToArray();

                foreach (var markovMatrixInfo in markovMatrixInfos)
                {
                    markovMatrixs.Add(
                        new MarkovMatrixData
                        {
                            MarkovMatrixId = markovMatrixInfo.MarkovMatrix.EntityId,
                            Sector = markovMatrixInfo.Sector.Code,
                            SectorName = markovMatrixInfo.Sector.Description,
                            Year = markovMatrixInfo.MarkovMatrix.Year,
                            InitialPD = markovMatrixInfo.MarkovMatrix.InitialPD,
                            InitialNonPD = markovMatrixInfo.MarkovMatrix.InitialNonPD,
                            PDmatrix = markovMatrixInfo.MarkovMatrix.PDmatrix,
                            NPDmatrix = markovMatrixInfo.MarkovMatrix.NPDmatrix,
                            Active = markovMatrixInfo.MarkovMatrix.Active
                        });
                }

                return markovMatrixs.ToArray();
            });
        }

        #endregion

        #region CCFModelling operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CCFModelling UpdateCCFModelling(CCFModelling ccfModelling)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICCFModellingRepository ccfModellingRepository = _DataRepositoryFactory.GetDataRepository<ICCFModellingRepository>();

                CCFModelling updatedEntity = null;

                if (ccfModelling.CCFModellingId == 0)
                    updatedEntity = ccfModellingRepository.Add(ccfModelling);
                else
                    updatedEntity = ccfModellingRepository.Update(ccfModelling);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCCFModelling(int ccfModellingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICCFModellingRepository ccfModellingRepository = _DataRepositoryFactory.GetDataRepository<ICCFModellingRepository>();

                ccfModellingRepository.Remove(ccfModellingId);
            });
        }

        public CCFModelling GetCCFModelling(int ccfModellingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICCFModellingRepository ccfModellingRepository = _DataRepositoryFactory.GetDataRepository<ICCFModellingRepository>();

                CCFModelling ccfModellingEntity = ccfModellingRepository.Get(ccfModellingId);
                if (ccfModellingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CCFModelling with ID of {0} is not in database", ccfModellingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ccfModellingEntity;
            });
        }

        public CCFModelling[] GetAllCCFModellings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICCFModellingRepository ccfModellingRepository = _DataRepositoryFactory.GetDataRepository<ICCFModellingRepository>();

                IEnumerable<CCFModelling> ccfModellings = ccfModellingRepository.Get().ToArray();

                return ccfModellings.ToArray();
            });
        }


        #endregion

        #region ECLComparism operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ECLComparism UpdateECLComparism(ECLComparism eclComparism)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLComparismRepository eclComparismRepository = _DataRepositoryFactory.GetDataRepository<IECLComparismRepository>();

                ECLComparism updatedEntity = null;

                if (eclComparism.ECLComparismId == 0)
                    updatedEntity = eclComparismRepository.Add(eclComparism);
                else
                    updatedEntity = eclComparismRepository.Update(eclComparism);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteECLComparism(int eclComparismId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLComparismRepository eclComparismRepository = _DataRepositoryFactory.GetDataRepository<IECLComparismRepository>();

                eclComparismRepository.Remove(eclComparismId);
            });
        }

        public ECLComparism GetECLComparism(int eclComparismId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLComparismRepository eclComparismRepository = _DataRepositoryFactory.GetDataRepository<IECLComparismRepository>();

                ECLComparism eclComparismEntity = eclComparismRepository.Get(eclComparismId);
                if (eclComparismEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ECLComparism with ID of {0} is not in database", eclComparismId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return eclComparismEntity;
            });
        }

        public ECLComparism[] GetAllECLComparisms()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IECLComparismRepository eclComparismRepository = _DataRepositoryFactory.GetDataRepository<IECLComparismRepository>();

                IEnumerable<ECLComparism> eclComparisms = eclComparismRepository.Get().ToArray();

                return eclComparisms.ToArray();
            });
        }

        #endregion

        #region ForeignEADExchangeRate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ForeignEADExchangeRate UpdateForeignEADExchangeRate(ForeignEADExchangeRate ForeignEADExchangeRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForeignEADExchangeRateRepository foreignEADExchangeRateRepository = _DataRepositoryFactory.GetDataRepository<IForeignEADExchangeRateRepository>();

                ForeignEADExchangeRate updatedEntity = null;

                if (ForeignEADExchangeRate.ForeignEADExchangeRateId == 0)
                    updatedEntity = foreignEADExchangeRateRepository.Add(ForeignEADExchangeRate);
                else
                    updatedEntity = foreignEADExchangeRateRepository.Update(ForeignEADExchangeRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteForeignEADExchangeRate(int foreignEadExchangeRateId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForeignEADExchangeRateRepository foreignEadExchangeRateRepository = _DataRepositoryFactory.GetDataRepository<IForeignEADExchangeRateRepository>();

                foreignEadExchangeRateRepository.Remove(foreignEadExchangeRateId);
            });
        }

        public ForeignEADExchangeRate GetForeignEADExchangeRate(int foreignEADExchangeRateId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForeignEADExchangeRateRepository foreignEadExchangeRateRepository = _DataRepositoryFactory.GetDataRepository<IForeignEADExchangeRateRepository>();

                ForeignEADExchangeRate foreignEadExchangeRateEntity = foreignEadExchangeRateRepository.Get(foreignEADExchangeRateId);
                if (foreignEadExchangeRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ForeignEADExchangeRate with ID of {0} is not in database", foreignEADExchangeRateId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return foreignEadExchangeRateEntity;
            });
        }

        public ForeignEADExchangeRate[] GetAllForeignEADExchangeRates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IForeignEADExchangeRateRepository foreignEadExchangeRateRepository = _DataRepositoryFactory.GetDataRepository<IForeignEADExchangeRateRepository>();

                IEnumerable<ForeignEADExchangeRate> foreignEadExchangeRates = foreignEadExchangeRateRepository.Get().ToArray();

                return foreignEadExchangeRates.ToArray();
            });
        }



        #endregion

        //Begin Victor Segment

        #region EuroBondSpread operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public EuroBondSpread UpdateEuroBondSpread(EuroBondSpread euroBondSpread)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEuroBondSpreadRepository euroBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<IEuroBondSpreadRepository>();

                EuroBondSpread updatedEntity = null;

                if (euroBondSpread.ID == 0)
                    updatedEntity = euroBondSpreadRepository.Add(euroBondSpread);
                else
                    updatedEntity = euroBondSpreadRepository.Update(euroBondSpread);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteEuroBondSpread(int euroBondSpreadId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEuroBondSpreadRepository euroBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<IEuroBondSpreadRepository>();

                euroBondSpreadRepository.Remove(euroBondSpreadId);
            });
        }

        public EuroBondSpread GetEuroBondSpread(int euroBondSpreadId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEuroBondSpreadRepository euroBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<IEuroBondSpreadRepository>();

                EuroBondSpread euroBondSpreadEntity = euroBondSpreadRepository.Get(euroBondSpreadId);
                if (euroBondSpreadEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("EuroBondSpread with ID of {0} is not in database", euroBondSpreadId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return euroBondSpreadEntity;
            });
        }

        public EuroBondSpread[] GetAllEuroBondSpreads()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEuroBondSpreadRepository euroBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<IEuroBondSpreadRepository>();

                IEnumerable<EuroBondSpread> euroBondSpreads = euroBondSpreadRepository.Get().ToArray();

                return euroBondSpreads.ToArray();
            });
        }

        #endregion

        #region PlacementComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PlacementComputationResult UpdatePlacementComputationResult(PlacementComputationResult placementComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementComputationResultRepository placementComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementComputationResultRepository>();

                PlacementComputationResult updatedEntity = null;

                if (placementComputationResult.Id == 0)
                    updatedEntity = placementComputationResultRepository.Add(placementComputationResult);
                else
                    updatedEntity = placementComputationResultRepository.Update(placementComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePlacementComputationResult(int placementComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementComputationResultRepository placementComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementComputationResultRepository>();

                placementComputationResultRepository.Remove(placementComputationResultId);
            });
        }

        public PlacementComputationResult GetPlacementComputationResult(int placementComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementComputationResultRepository placementComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementComputationResultRepository>();

                PlacementComputationResult placementComputationResultEntity = placementComputationResultRepository.Get(placementComputationResultId);
                if (placementComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PlacementComputationResult with ID of {0} is not in database", placementComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return placementComputationResultEntity;
            });
        }

        public PlacementComputationResult[] GetAllPlacementComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementComputationResultRepository placementComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementComputationResultRepository>();

                IEnumerable<PlacementComputationResult> placementComputationResults = placementComputationResultRepository.Get().ToArray();

                return placementComputationResults.ToArray();
            });
        }

        #endregion

        #region LgdComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LgdComputationResult UpdateLgdComputationResult(LgdComputationResult lgdComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdComputationResultRepository lgdComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILgdComputationResultRepository>();

                LgdComputationResult updatedEntity = null;

                if (lgdComputationResult.Id == 0)
                    updatedEntity = lgdComputationResultRepository.Add(lgdComputationResult);
                else
                    updatedEntity = lgdComputationResultRepository.Update(lgdComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLgdComputationResult(int lgdComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdComputationResultRepository lgdComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILgdComputationResultRepository>();

                lgdComputationResultRepository.Remove(lgdComputationResultId);
            });
        }

        public LgdComputationResult GetLgdComputationResult(int lgdComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdComputationResultRepository lgdComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILgdComputationResultRepository>();

                LgdComputationResult lgdComputationResultEntity = lgdComputationResultRepository.Get(lgdComputationResultId);
                if (lgdComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LgdComputationResult with ID of {0} is not in database", lgdComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return lgdComputationResultEntity;
            });
        }

        public LgdComputationResult[] GetAllLgdComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILgdComputationResultRepository lgdComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILgdComputationResultRepository>();

                IEnumerable<LgdComputationResult> lgdComputationResults = lgdComputationResultRepository.Get().ToArray();

                return lgdComputationResults.ToArray();
            });
        }

        #endregion

        #region LocalBondSpread operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LocalBondSpread UpdateLocalBondSpread(LocalBondSpread localBondSpread)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILocalBondSpreadRepository localBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<ILocalBondSpreadRepository>();

                LocalBondSpread updatedEntity = null;

                if (localBondSpread.ID == 0)
                    updatedEntity = localBondSpreadRepository.Add(localBondSpread);
                else
                    updatedEntity = localBondSpreadRepository.Update(localBondSpread);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLocalBondSpread(int localBondSpreadId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILocalBondSpreadRepository localBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<ILocalBondSpreadRepository>();

                localBondSpreadRepository.Remove(localBondSpreadId);
            });
        }

        public LocalBondSpread GetLocalBondSpread(int localBondSpreadId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILocalBondSpreadRepository localBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<ILocalBondSpreadRepository>();

                LocalBondSpread localBondSpreadEntity = localBondSpreadRepository.Get(localBondSpreadId);
                if (localBondSpreadEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LocalBondSpread with ID of {0} is not in database", localBondSpreadId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return localBondSpreadEntity;
            });
        }

        public LocalBondSpread[] GetAllLocalBondSpreads()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILocalBondSpreadRepository localBondSpreadRepository = _DataRepositoryFactory.GetDataRepository<ILocalBondSpreadRepository>();

                IEnumerable<LocalBondSpread> localBondSpreads = localBondSpreadRepository.Get().ToArray();

                return localBondSpreads.ToArray();
            });
        }

        #endregion

        #region MarginalPDDistribution operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MarginalPDDistribution UpdateMarginalPDDistribution(MarginalPDDistribution marginalPDDistribution)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionRepository marginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionRepository>();

                MarginalPDDistribution updatedEntity = null;

                if (marginalPDDistribution.ID == 0)
                    updatedEntity = marginalPDDistributionRepository.Add(marginalPDDistribution);
                else
                    updatedEntity = marginalPDDistributionRepository.Update(marginalPDDistribution);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMarginalPDDistribution(int marginalPDDistributionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionRepository marginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionRepository>();

                marginalPDDistributionRepository.Remove(marginalPDDistributionId);
            });
        }

        public MarginalPDDistribution GetMarginalPDDistribution(int marginalPDDistributionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionRepository marginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionRepository>();

                MarginalPDDistribution marginalPDDistributionEntity = marginalPDDistributionRepository.Get(marginalPDDistributionId);
                if (marginalPDDistributionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MarginalPDDistribution with ID of {0} is not in database", marginalPDDistributionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return marginalPDDistributionEntity;
            });
        }

        public MarginalPDDistribution[] GetAllMarginalPDDistributions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionRepository marginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionRepository>();

                IEnumerable<MarginalPDDistribution> marginalPDDistributions = marginalPDDistributionRepository.Get().ToArray();

                return marginalPDDistributions.ToArray();
            });
        }

        #endregion

        #region BondMarginalPDDistribution operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BondMarginalPDDistribution UpdateBondMarginalPDDistribution(BondMarginalPDDistribution bondMarginalPDDistribution)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondMarginalPDDistributionRepository bondMarginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IBondMarginalPDDistributionRepository>();

                BondMarginalPDDistribution updatedEntity = null;

                if (bondMarginalPDDistribution.ID == 0)
                    updatedEntity = bondMarginalPDDistributionRepository.Add(bondMarginalPDDistribution);
                else
                    updatedEntity = bondMarginalPDDistributionRepository.Update(bondMarginalPDDistribution);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBondMarginalPDDistribution(int bondMarginalPDDistributionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondMarginalPDDistributionRepository bondMarginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IBondMarginalPDDistributionRepository>();

                bondMarginalPDDistributionRepository.Remove(bondMarginalPDDistributionId);
            });
        }

        public BondMarginalPDDistribution GetBondMarginalPDDistribution(int bondMarginalPDDistributionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondMarginalPDDistributionRepository bondMarginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IBondMarginalPDDistributionRepository>();

                BondMarginalPDDistribution bondMarginalPDDistributionEntity = bondMarginalPDDistributionRepository.Get(bondMarginalPDDistributionId);
                if (bondMarginalPDDistributionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BondMarginalPDDistribution with ID of {0} is not in database", bondMarginalPDDistributionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bondMarginalPDDistributionEntity;
            });
        }

        public BondMarginalPDDistribution[] GetAllBondMarginalPDDistributions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondMarginalPDDistributionRepository bondMarginalPDDistributionRepository = _DataRepositoryFactory.GetDataRepository<IBondMarginalPDDistributionRepository>();

                IEnumerable<BondMarginalPDDistribution> bondMarginalPDDistributions = bondMarginalPDDistributionRepository.Get().ToArray();

                return bondMarginalPDDistributions.ToArray();
            });
        }

        #endregion

        #region MarginalPDDistributionPlacement operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MarginalPDDistributionPlacement UpdateMarginalPDDistributionPlacement(MarginalPDDistributionPlacement marginalPDDistributionPlacement)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionPlacementRepository marginalPDDistributionPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionPlacementRepository>();

                MarginalPDDistributionPlacement updatedEntity = null;

                if (marginalPDDistributionPlacement.ID == 0)
                    updatedEntity = marginalPDDistributionPlacementRepository.Add(marginalPDDistributionPlacement);
                else
                    updatedEntity = marginalPDDistributionPlacementRepository.Update(marginalPDDistributionPlacement);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMarginalPDDistributionPlacement(int marginalPDDistributionPlacementId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionPlacementRepository marginalPDDistributionPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionPlacementRepository>();

                marginalPDDistributionPlacementRepository.Remove(marginalPDDistributionPlacementId);
            });
        }

        public MarginalPDDistributionPlacement GetMarginalPDDistributionPlacement(int marginalPDDistributionPlacementId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionPlacementRepository marginalPDDistributionPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionPlacementRepository>();

                MarginalPDDistributionPlacement marginalPDDistributionPlacementEntity = marginalPDDistributionPlacementRepository.Get(marginalPDDistributionPlacementId);
                if (marginalPDDistributionPlacementEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MarginalPDDistributionPlacement with ID of {0} is not in database", marginalPDDistributionPlacementId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return marginalPDDistributionPlacementEntity;
            });
        }

        public MarginalPDDistributionPlacement[] GetAllMarginalPDDistributionPlacements()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMarginalPDDistributionPlacementRepository marginalPDDistributionPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMarginalPDDistributionPlacementRepository>();

                IEnumerable<MarginalPDDistributionPlacement> marginalPDDistributionPlacements = marginalPDDistributionPlacementRepository.Get().ToArray();

                return marginalPDDistributionPlacements.ToArray();
            });
        }

        #endregion

        #region EclComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public EclComputationResult UpdateEclComputationResult(EclComputationResult eclComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclComputationResultRepository eclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IEclComputationResultRepository>();

                EclComputationResult updatedEntity = null;

                if (eclComputationResult.ID == 0)
                    updatedEntity = eclComputationResultRepository.Add(eclComputationResult);
                else
                    updatedEntity = eclComputationResultRepository.Update(eclComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteEclComputationResult(int eclComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclComputationResultRepository eclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IEclComputationResultRepository>();

                eclComputationResultRepository.Remove(eclComputationResultId);
            });
        }

        public EclComputationResult GetEclComputationResult(int eclComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclComputationResultRepository eclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IEclComputationResultRepository>();

                EclComputationResult eclComputationResultEntity = eclComputationResultRepository.Get(eclComputationResultId);
                if (eclComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("EclComputationResult with ID of {0} is not in database", eclComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return eclComputationResultEntity;
            });
        }

        public EclComputationResult[] GetAllEclComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IEclComputationResultRepository eclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IEclComputationResultRepository>();

                IEnumerable<EclComputationResult> eclComputationResults = eclComputationResultRepository.Get().ToArray();

                return eclComputationResults.ToArray();
            });
        }

        #endregion

        #region BondEclComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BondEclComputationResult UpdateBondEclComputationResult(BondEclComputationResult bondEclComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondEclComputationResultRepository bondEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IBondEclComputationResultRepository>();

                BondEclComputationResult updatedEntity = null;

                if (bondEclComputationResult.ID == 0)
                    updatedEntity = bondEclComputationResultRepository.Add(bondEclComputationResult);
                else
                    updatedEntity = bondEclComputationResultRepository.Update(bondEclComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBondEclComputationResult(int bondEclComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondEclComputationResultRepository bondEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IBondEclComputationResultRepository>();

                bondEclComputationResultRepository.Remove(bondEclComputationResultId);
            });
        }

        public BondEclComputationResult GetBondEclComputationResult(int bondEclComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondEclComputationResultRepository bondEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IBondEclComputationResultRepository>();

                BondEclComputationResult bondEclComputationResultEntity = bondEclComputationResultRepository.Get(bondEclComputationResultId);
                if (bondEclComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BondEclComputationResult with ID of {0} is not in database", bondEclComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bondEclComputationResultEntity;
            });
        }

        public BondEclComputationResult[] GetAllBondEclComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBondEclComputationResultRepository bondEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IBondEclComputationResultRepository>();

                IEnumerable<BondEclComputationResult> bondEclComputationResults = bondEclComputationResultRepository.Get().ToArray();

                return bondEclComputationResults.ToArray();
            });
        }

        #endregion

        #region PlacementEclComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PlacementEclComputationResult UpdatePlacementEclComputationResult(PlacementEclComputationResult placementEclComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementEclComputationResultRepository placementEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementEclComputationResultRepository>();

                PlacementEclComputationResult updatedEntity = null;

                if (placementEclComputationResult.ID == 0)
                    updatedEntity = placementEclComputationResultRepository.Add(placementEclComputationResult);
                else
                    updatedEntity = placementEclComputationResultRepository.Update(placementEclComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePlacementEclComputationResult(int placementEclComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementEclComputationResultRepository placementEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementEclComputationResultRepository>();

                placementEclComputationResultRepository.Remove(placementEclComputationResultId);
            });
        }

        public PlacementEclComputationResult GetPlacementEclComputationResult(int placementEclComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementEclComputationResultRepository placementEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementEclComputationResultRepository>();

                PlacementEclComputationResult placementEclComputationResultEntity = placementEclComputationResultRepository.Get(placementEclComputationResultId);
                if (placementEclComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PlacementEclComputationResult with ID of {0} is not in database", placementEclComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return placementEclComputationResultEntity;
            });
        }

        public PlacementEclComputationResult[] GetAllPlacementEclComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPlacementEclComputationResultRepository placementEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<IPlacementEclComputationResultRepository>();

                IEnumerable<PlacementEclComputationResult> placementEclComputationResults = placementEclComputationResultRepository.Get().ToArray();

                return placementEclComputationResults.ToArray();
            });
        }

        #endregion

        #region LcBgEclComputationResult operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public LcBgEclComputationResult UpdateLcBgEclComputationResult(LcBgEclComputationResult lcBgEclComputationResult)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILcBgEclComputationResultRepository lcBgEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILcBgEclComputationResultRepository>();

                LcBgEclComputationResult updatedEntity = null;

                if (lcBgEclComputationResult.ID == 0)
                    updatedEntity = lcBgEclComputationResultRepository.Add(lcBgEclComputationResult);
                else
                    updatedEntity = lcBgEclComputationResultRepository.Update(lcBgEclComputationResult);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteLcBgEclComputationResult(int lcBgEclComputationResultId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILcBgEclComputationResultRepository lcBgEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILcBgEclComputationResultRepository>();

                lcBgEclComputationResultRepository.Remove(lcBgEclComputationResultId);
            });
        }

        public LcBgEclComputationResult GetLcBgEclComputationResult(int lcBgEclComputationResultId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILcBgEclComputationResultRepository lcBgEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILcBgEclComputationResultRepository>();

                LcBgEclComputationResult lcBgEclComputationResultEntity = lcBgEclComputationResultRepository.Get(lcBgEclComputationResultId);
                if (lcBgEclComputationResultEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("LcBgEclComputationResult with ID of {0} is not in database", lcBgEclComputationResultId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return lcBgEclComputationResultEntity;
            });
        }

        public LcBgEclComputationResult[] GetAllLcBgEclComputationResults()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ILcBgEclComputationResultRepository lcBgEclComputationResultRepository = _DataRepositoryFactory.GetDataRepository<ILcBgEclComputationResultRepository>();

                IEnumerable<LcBgEclComputationResult> lcBgEclComputationResults = lcBgEclComputationResultRepository.Get().ToArray();

                return lcBgEclComputationResults.ToArray();
            });
        }

        #endregion

        //End Victor Segment



        #region ComputedForcastedPDLGD operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ComputedForcastedPDLGD UpdateComputedForcastedPDLGD(ComputedForcastedPDLGD computedForcastedPDLGD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

                ComputedForcastedPDLGD updatedEntity = null;

                if (computedForcastedPDLGD.ComputedPDId == 0)
                    updatedEntity = computedForcastedPDLGDRepository.Add(computedForcastedPDLGD);
                else
                    updatedEntity = computedForcastedPDLGDRepository.Update(computedForcastedPDLGD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteComputedForcastedPDLGD(int computedForcastedPDLGDId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

                computedForcastedPDLGDRepository.Remove(computedForcastedPDLGDId);
            });
        }

        public ComputedForcastedPDLGD GetComputedForcastedPDLGD(int computedForcastedPDLGDId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

                ComputedForcastedPDLGD computedForcastedPDLGDEntity = computedForcastedPDLGDRepository.Get(computedForcastedPDLGDId);
                if (computedForcastedPDLGDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ComputedForcastedPDLGD with ID of {0} is not in database", computedForcastedPDLGDId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return computedForcastedPDLGDEntity;
            });
        }

        public ComputedForcastedPDLGD[] GetAllComputedForcastedPDLGDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

                List<ComputedForcastedPDLGD> computedForcastedPDLGDs = computedForcastedPDLGDRepository.GetComputedForcastedPDLGD();

                return computedForcastedPDLGDs.ToArray();
            });
        }

        public ComputedForcastedPDLGD[] GetListComputedForcastedPDLGDs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDRepository computedForcastedPDLGDRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDRepository>();

                IEnumerable<ComputedForcastedPDLGD> computedForcastedPDLGDs = computedForcastedPDLGDRepository.Get().ToArray();

                return computedForcastedPDLGDs.ToArray();
            });
        }

        #endregion

        #region ComputedForcastedPDLGDForeign operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ComputedForcastedPDLGDForeign UpdateComputedForcastedPDLGDForeign(ComputedForcastedPDLGDForeign computedForcastedPDLGDForeign)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDForeignRepository computedForcastedPDLGDForeignRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDForeignRepository>();

                ComputedForcastedPDLGDForeign updatedEntity = null;

                if (computedForcastedPDLGDForeign.ComputedPDId == 0)
                    updatedEntity = computedForcastedPDLGDForeignRepository.Add(computedForcastedPDLGDForeign);
                else
                    updatedEntity = computedForcastedPDLGDForeignRepository.Update(computedForcastedPDLGDForeign);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteComputedForcastedPDLGDForeign(int computedForcastedPDLGDForeignId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDForeignRepository computedForcastedPDLGDForeignRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDForeignRepository>();

                computedForcastedPDLGDForeignRepository.Remove(computedForcastedPDLGDForeignId);
            });
        }

        public ComputedForcastedPDLGDForeign GetComputedForcastedPDLGDForeign(int computedForcastedPDLGDForeignId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDForeignRepository computedForcastedPDLGDForeignRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDForeignRepository>();

                ComputedForcastedPDLGDForeign computedForcastedPDLGDForeignEntity = computedForcastedPDLGDForeignRepository.Get(computedForcastedPDLGDForeignId);
                if (computedForcastedPDLGDForeignEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ComputedForcastedPDLGDForeign with ID of {0} is not in database", computedForcastedPDLGDForeignId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return computedForcastedPDLGDForeignEntity;
            });
        }

        public ComputedForcastedPDLGDForeign[] GetAllComputedForcastedPDLGDForeigns()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDForeignRepository computedForcastedPDLGDForeignRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDForeignRepository>();

                List<ComputedForcastedPDLGDForeign> computedForcastedPDLGDForeigns = computedForcastedPDLGDForeignRepository.GetComputedForcastedPDLGDForeign();

                return computedForcastedPDLGDForeigns.ToArray();
            });
        }

        public ComputedForcastedPDLGDForeign[] GetListComputedForcastedPDLGDForeigns()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IComputedForcastedPDLGDForeignRepository computedForcastedPDLGDForeignRepository = _DataRepositoryFactory.GetDataRepository<IComputedForcastedPDLGDForeignRepository>();

                IEnumerable<ComputedForcastedPDLGDForeign> computedForcastedPDLGDForeigns = computedForcastedPDLGDForeignRepository.Get().ToArray();

                return computedForcastedPDLGDForeigns.ToArray();
            });
        }

        #endregion

        #region MacroEconomicsNPL operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacroEconomicsNPL UpdateMacroEconomicsNPL(MacroEconomicsNPL macroEconomicsNPL)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicsNPLRepository macroEconomicsNPLRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicsNPLRepository>();

                MacroEconomicsNPL updatedEntity = null;

                if (macroEconomicsNPL.macroeconomicnplId == 0)
                    updatedEntity = macroEconomicsNPLRepository.Add(macroEconomicsNPL);
                else
                    updatedEntity = macroEconomicsNPLRepository.Update(macroEconomicsNPL);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacroEconomicsNPL(int macroeconomicnplId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicsNPLRepository macroEconomicsNPLRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicsNPLRepository>();

                macroEconomicsNPLRepository.Remove(macroeconomicnplId);
            });
        }


        public MacroEconomicsNPL GetMacroEconomicsNPL(int macroeconomicnplId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicsNPLRepository macroEconomicsNPLRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicsNPLRepository>();

                MacroEconomicsNPL macroEconomicsNPLEntity = macroEconomicsNPLRepository.Get(macroeconomicnplId);
                if (macroEconomicsNPLEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacroEconomicsNPL with ID of {0} is not in database", macroeconomicnplId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macroEconomicsNPLEntity;
            });
        }

        public MacroEconomicsNPL[] GetAllMacroEconomicsNPLs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacroEconomicsNPLRepository macroEconomicsNPLRepository = _DataRepositoryFactory.GetDataRepository<IMacroEconomicsNPLRepository>();

                IEnumerable<MacroEconomicsNPL> macroEconomicsNPLs = macroEconomicsNPLRepository.Get().ToArray();

                return macroEconomicsNPLs.ToArray();
            });
        }


        #endregion

        #region MonthlyDiscountFactor operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MonthlyDiscountFactor UpdateMonthlyDiscountFactor(MonthlyDiscountFactor monthlyDiscountFactor)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorRepository monthlyDiscountFactorRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorRepository>();

                MonthlyDiscountFactor updatedEntity = null;

                if (monthlyDiscountFactor.MonthlyDiscountFactor_Id == 0)
                    updatedEntity = monthlyDiscountFactorRepository.Add(monthlyDiscountFactor);
                else
                    updatedEntity = monthlyDiscountFactorRepository.Update(monthlyDiscountFactor);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMonthlyDiscountFactor(int MonthlyDiscountFactor_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorRepository monthlyDiscountFactorRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorRepository>();

                monthlyDiscountFactorRepository.Remove(MonthlyDiscountFactor_Id);
            });
        }

        public MonthlyDiscountFactor GetMonthlyDiscountFactor(int MonthlyDiscountFactor_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorRepository monthlyDiscountFactorRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorRepository>();

                MonthlyDiscountFactor monthlyDiscountFactorEntity = monthlyDiscountFactorRepository.Get(MonthlyDiscountFactor_Id);
                if (monthlyDiscountFactorEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MonthlyDiscountFactor with ID of {0} is not in database", MonthlyDiscountFactor_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return monthlyDiscountFactorEntity;
            });
        }

        public MonthlyDiscountFactor[] GetAllMonthlyDiscountFactors()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorRepository monthlyDiscountFactorRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorRepository>();

                IEnumerable<MonthlyDiscountFactor> monthlyDiscountFactors = monthlyDiscountFactorRepository.Get().ToArray();

                return monthlyDiscountFactors.Take(100).ToArray();
            });
        }

        public MonthlyDiscountFactor[] GetMonthlyDiscountFactorByRefNo(string RefNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorRepository monthlyDiscountFactorRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorRepository>();

                return monthlyDiscountFactorRepository.GetMonthlyDiscountFactorByRefNo(RefNo).ToArray();
            });
        }



        #endregion

        #region MonthlyDiscountFactorBond operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MonthlyDiscountFactorBond UpdateMonthlyDiscountFactorBond(MonthlyDiscountFactorBond monthlyDiscountFactorBond)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorBondRepository monthlyDiscountFactorBondRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorBondRepository>();

                MonthlyDiscountFactorBond updatedEntity = null;

                if (monthlyDiscountFactorBond.MonthlyDiscountFactorBond_Id == 0)
                    updatedEntity = monthlyDiscountFactorBondRepository.Add(monthlyDiscountFactorBond);
                else
                    updatedEntity = monthlyDiscountFactorBondRepository.Update(monthlyDiscountFactorBond);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMonthlyDiscountFactorBond(int MonthlyDiscountFactorBond_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorBondRepository monthlyDiscountFactorBondRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorBondRepository>();

                monthlyDiscountFactorBondRepository.Remove(MonthlyDiscountFactorBond_Id);
            });
        }

        public MonthlyDiscountFactorBond GetMonthlyDiscountFactorBond(int MonthlyDiscountFactorBond_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorBondRepository monthlyDiscountFactorBondRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorBondRepository>();

                MonthlyDiscountFactorBond monthlyDiscountFactorBondEntity = monthlyDiscountFactorBondRepository.Get(MonthlyDiscountFactorBond_Id);
                if (monthlyDiscountFactorBondEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MonthlyDiscountFactorBond with ID of {0} is not in database", MonthlyDiscountFactorBond_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return monthlyDiscountFactorBondEntity;
            });
        }

        public MonthlyDiscountFactorBond[] GetAllMonthlyDiscountFactorBonds()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorBondRepository monthlyDiscountFactorBondRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorBondRepository>();

                IEnumerable<MonthlyDiscountFactorBond> monthlyDiscountFactorBonds = monthlyDiscountFactorBondRepository.Get().ToArray();

                return monthlyDiscountFactorBonds.Take(100).ToArray();
            });
        }

        public MonthlyDiscountFactorBond[] GetMonthlyDiscountFactorBondByRefNo(string RefNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorBondRepository monthlyDiscountFactorBondRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorBondRepository>(); ;

                return monthlyDiscountFactorBondRepository.GetMonthlyDiscountFactorBondByRefNo(RefNo).ToArray();
            });
        }


        #endregion

        #region MonthlyDiscountFactorPlacement operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MonthlyDiscountFactorPlacement UpdateMonthlyDiscountFactorPlacement(MonthlyDiscountFactorPlacement monthlyDiscountFactorPlacement)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorPlacementRepository monthlyDiscountFactorPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorPlacementRepository>();

                MonthlyDiscountFactorPlacement updatedEntity = null;

                if (monthlyDiscountFactorPlacement.MonthlyDiscountFactorPlacement_Id == 0)
                    updatedEntity = monthlyDiscountFactorPlacementRepository.Add(monthlyDiscountFactorPlacement);
                else
                    updatedEntity = monthlyDiscountFactorPlacementRepository.Update(monthlyDiscountFactorPlacement);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMonthlyDiscountFactorPlacement(int MonthlyDiscountFactorPlacement_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorPlacementRepository monthlyDiscountFactorPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorPlacementRepository>();

                monthlyDiscountFactorPlacementRepository.Remove(MonthlyDiscountFactorPlacement_Id);
            });
        }

        public MonthlyDiscountFactorPlacement GetMonthlyDiscountFactorPlacement(int MonthlyDiscountFactorPlacement_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorPlacementRepository monthlyDiscountFactorPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorPlacementRepository>();

                MonthlyDiscountFactorPlacement monthlyDiscountFactorPlacementEntity = monthlyDiscountFactorPlacementRepository.Get(MonthlyDiscountFactorPlacement_Id);
                if (monthlyDiscountFactorPlacementEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MonthlyDiscountFactorPlacement with ID of {0} is not in database", MonthlyDiscountFactorPlacement_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return monthlyDiscountFactorPlacementEntity;
            });
        }

        public MonthlyDiscountFactorPlacement[] GetAllMonthlyDiscountFactorPlacements()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorPlacementRepository monthlyDiscountFactorPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorPlacementRepository>();

                IEnumerable<MonthlyDiscountFactorPlacement> monthlyDiscountFactorPlacements = monthlyDiscountFactorPlacementRepository.Get().ToArray();

                return monthlyDiscountFactorPlacements.Take(100).ToArray();
            });
        }

        public MonthlyDiscountFactorPlacement[] GetMonthlyDiscountFactorPlacementByRefNo(string RefNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMonthlyDiscountFactorPlacementRepository monthlyDiscountFactorPlacementRepository = _DataRepositoryFactory.GetDataRepository<IMonthlyDiscountFactorPlacementRepository>();

                return monthlyDiscountFactorPlacementRepository.GetMonthlyDiscountFactorPlacementByRefNo(RefNo).ToArray();
            });
        }


        #endregion

        #region ProbabilityWeighted operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProbabilityWeighted UpdateProbabilityWeighted(ProbabilityWeighted probabilityWeighted)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProbabilityWeightedRepository probabilityWeightedRepository = _DataRepositoryFactory.GetDataRepository<IProbabilityWeightedRepository>();

                ProbabilityWeighted updatedEntity = null;

                if (probabilityWeighted.ProbabilityWeighted_Id == 0)
                    updatedEntity = probabilityWeightedRepository.Add(probabilityWeighted);
                else
                    updatedEntity = probabilityWeightedRepository.Update(probabilityWeighted);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteProbabilityWeighted(int ProbabilityWeighted_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProbabilityWeightedRepository probabilityWeightedRepository = _DataRepositoryFactory.GetDataRepository<IProbabilityWeightedRepository>();

                probabilityWeightedRepository.Remove(ProbabilityWeighted_Id);
            });
        }

        public ProbabilityWeighted GetProbabilityWeighted(int ProbabilityWeighted_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProbabilityWeightedRepository probabilityWeightedRepository = _DataRepositoryFactory.GetDataRepository<IProbabilityWeightedRepository>();

                ProbabilityWeighted probabilityWeightedEntity = probabilityWeightedRepository.Get(ProbabilityWeighted_Id);
                if (probabilityWeightedEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProbabilityWeighted with ID of {0} is not in database", ProbabilityWeighted_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return probabilityWeightedEntity;
            });
        }

        public ProbabilityWeighted[] GetAllProbabilityWeighteds()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProbabilityWeightedRepository probabilityWeightedRepository = _DataRepositoryFactory.GetDataRepository<IProbabilityWeightedRepository>();

                IEnumerable<ProbabilityWeighted> probabilityWeighteds = probabilityWeightedRepository.Get().ToArray();

                return probabilityWeighteds.ToArray();
            });
        }

        public ProbabilityWeighted[] GetProbabilityWeightedByInstrumentType(string InstrumentType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProbabilityWeightedRepository probabilityWeightedRepository = _DataRepositoryFactory.GetDataRepository<IProbabilityWeightedRepository>();

                return probabilityWeightedRepository.GetProbabilityWeightedByInstrumentType(InstrumentType).ToArray();
            });
        }



        #endregion

        #region MacrovariableEstimate operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MacrovariableEstimate UpdateMacrovariableEstimate(MacrovariableEstimate macrovariableEstimate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacrovariableEstimateRepository macrovariableEstimateRepository = _DataRepositoryFactory.GetDataRepository<IMacrovariableEstimateRepository>();

                MacrovariableEstimate updatedEntity = null;

                if (macrovariableEstimate.MacrovariableEstimate_Id == 0)
                    updatedEntity = macrovariableEstimateRepository.Add(macrovariableEstimate);
                else
                    updatedEntity = macrovariableEstimateRepository.Update(macrovariableEstimate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMacrovariableEstimate(int MacrovariableEstimate_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacrovariableEstimateRepository macrovariableEstimateRepository = _DataRepositoryFactory.GetDataRepository<IMacrovariableEstimateRepository>();

                macrovariableEstimateRepository.Remove(MacrovariableEstimate_Id);
            });
        }

        public MacrovariableEstimate GetMacrovariableEstimate(int MacrovariableEstimate_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacrovariableEstimateRepository macrovariableEstimateRepository = _DataRepositoryFactory.GetDataRepository<IMacrovariableEstimateRepository>();

                MacrovariableEstimate macrovariableEstimateEntity = macrovariableEstimateRepository.Get(MacrovariableEstimate_Id);
                if (macrovariableEstimateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MacrovariableEstimate with ID of {0} is not in database", MacrovariableEstimate_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return macrovariableEstimateEntity;
            });
        }

        public MacrovariableEstimate[] GetAllMacrovariableEstimates()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacrovariableEstimateRepository macrovariableEstimateRepository = _DataRepositoryFactory.GetDataRepository<IMacrovariableEstimateRepository>();

                IEnumerable<MacrovariableEstimate> macrovariableEstimates = macrovariableEstimateRepository.Get().ToArray();

                return macrovariableEstimates.ToArray();
            });
        }

        public MacrovariableEstimate[] GetMacrovariableEstimateByCategory(string Category)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMacrovariableEstimateRepository macrovariableEstimateRepository = _DataRepositoryFactory.GetDataRepository<IMacrovariableEstimateRepository>();

                return macrovariableEstimateRepository.GetMacrovariableEstimateByCategory(Category).ToArray();
            });
        }



        #endregion

        #region SectorMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SectorMapping UpdateSectorMapping(SectorMapping sectorMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorMappingRepository sectorMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorMappingRepository>();

                SectorMapping updatedEntity = null;

                if (sectorMapping.SectorMapping_Id == 0)
                    updatedEntity = sectorMappingRepository.Add(sectorMapping);
                else
                    updatedEntity = sectorMappingRepository.Update(sectorMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSectorMapping(int SectorMapping_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorMappingRepository sectorMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorMappingRepository>();

                sectorMappingRepository.Remove(SectorMapping_Id);
            });
        }

        public SectorMapping GetSectorMapping(int SectorMapping_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorMappingRepository sectorMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorMappingRepository>();

                SectorMapping sectorMappingEntity = sectorMappingRepository.Get(SectorMapping_Id);
                if (sectorMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SectorMapping with ID of {0} is not in database", SectorMapping_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sectorMappingEntity;
            });
        }

        public SectorMapping[] GetAllSectorMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISectorMappingRepository sectorMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorMappingRepository>();

                IEnumerable<SectorMapping> sectorMappings = sectorMappingRepository.Get().ToArray();

                return sectorMappings.ToArray();
            });
        }

        //public SectorMapping[] GetSectorMappingBySource(string Source)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        ISectorMappingRepository sectorMappingRepository = _DataRepositoryFactory.GetDataRepository<ISectorMappingRepository>();

        //        return sectorMappingRepository.GetSectorMappingBySource(Source).ToArray();
        //    });
        //}

        #endregion

        #region InvestmentOthersECL operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public InvestmentOthersECL UpdateInvestmentOthersECL(InvestmentOthersECL investmentOthersECL)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentOthersECLRepository investmentOthersECLRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentOthersECLRepository>();

                InvestmentOthersECL updatedEntity = null;

                if (investmentOthersECL.ecl_Id == 0)
                    updatedEntity = investmentOthersECLRepository.Add(investmentOthersECL);
                else
                    updatedEntity = investmentOthersECLRepository.Update(investmentOthersECL);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteInvestmentOthersECL(int ecl_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentOthersECLRepository investmentOthersECLRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentOthersECLRepository>();

                investmentOthersECLRepository.Remove(ecl_Id);
            });
        }

        public InvestmentOthersECL GetInvestmentOthersECL(int ecl_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentOthersECLRepository investmentOthersECLRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentOthersECLRepository>();

                InvestmentOthersECL investmentOthersECLEntity = investmentOthersECLRepository.Get(ecl_Id);
                if (investmentOthersECLEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("InvestmentOthersECL with ID of {0} is not in database", ecl_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return investmentOthersECLEntity;
            });
        }

        public InvestmentOthersECL[] GetAllInvestmentOthersECLs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentOthersECLRepository investmentOthersECLRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentOthersECLRepository>();

                IEnumerable<InvestmentOthersECL> investmentOthersECLs = investmentOthersECLRepository.Get().ToArray();

                return investmentOthersECLs.ToArray();
            });
        }

        public InvestmentOthersECL[] GetInvestmentOthersECLByRefNo(string RefNo)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IInvestmentOthersECLRepository investmentOthersECLRepository = _DataRepositoryFactory.GetDataRepository<IInvestmentOthersECLRepository>();

                return investmentOthersECLRepository.GetInvestmentOthersECLByRefNo(RefNo).ToArray();
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
        

        #endregion



       
    }
}
