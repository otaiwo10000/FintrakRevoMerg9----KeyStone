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
    public class MPRCoreManager : ManagerBase, IMPRCoreService
    {
        public MPRCoreManager()
        {
        }

        public MPRCoreManager(IDataRepositoryFactory dataRepositoryFactory)
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

        // #region UserMIS operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public UserMIS UpdateUserMIS(UserMIS userMIS)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

        //         UserMIS updatedEntity = null;

        //         if (userMIS.UserMISId == 0)
        //         {
        //             updatedEntity = userMISRepository.Add(userMIS);
        //         }
        //         else
        //             updatedEntity = userMISRepository.Update(userMIS);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteUserMIS(int userMISId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

        //         userMISRepository.Remove(userMISId);
        //     });
        // }

        // public UserMIS GetUserMIS(int userMISId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

        //         UserMIS userMISEntity = userMISRepository.Get(userMISId);
        //         if (userMISEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("UserMIS with ID of {0} is not in database", userMISId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return userMISEntity;
        //     });
        // }

        // public UserMIS GetUserMISByLoginID(string loginID)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

        //         var setUp = GetSetup();

        //         UserMIS userMISEntity = userMISRepository.Get().Where(c => c.LoginID == loginID).FirstOrDefault();

        //         return userMISEntity;
        //     });
        // }

        // public UserMIS[] GetAllUserMISs()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<UserMIS> userMIS = userMISRepository.Get().ToArray();

        //         return userMIS.ToArray();
        //     });
        // }


        // #endregion

        // #region UserClassificationMap operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public UserClassificationMap UpdateUserClassificationMap(UserClassificationMap userClassificationMap)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

        //         UserClassificationMap updatedEntity = null;

        //         if (userClassificationMap.UserClassificationMapId == 0)
        //         {
        //             updatedEntity = userClassificationMapRepository.Add(userClassificationMap);
        //         }
        //         else
        //             updatedEntity = userClassificationMapRepository.Update(userClassificationMap);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteUserClassificationMap(int userClassificationMapId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

        //         userClassificationMapRepository.Remove(userClassificationMapId);
        //     });
        // }

        // public UserClassificationMap GetUserClassificationMap(int userClassificationMapId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

        //         UserClassificationMap userClassificationMapEntity = userClassificationMapRepository.Get(userClassificationMapId);
        //         if (userClassificationMapEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("UserClassificationMap with ID of {0} is not in database", userClassificationMapId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return userClassificationMapEntity;
        //     });
        // }

        // public UserClassificationMap[] GetAllUserClassificationMaps(string loginID)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<UserClassificationMap> userClassificationMap = userClassificationMapRepository.GetUserClassificationMaps(loginID).ToArray();

        //         return userClassificationMap.ToArray();
        //     });
        // }


        // #endregion

        // #region TeamDefinition operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public TeamDefinition UpdateTeamDefinition(TeamDefinition teamDefinition)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

        //         TeamDefinition updatedEntity = null;

        //         if (teamDefinition.TeamDefinitionId == 0)
        //         {
        //             teamDefinition.Year = GetSetup().Year;
        //             updatedEntity = teamDefinitionRepository.Add(teamDefinition);
        //         }      
        //         else
        //             updatedEntity = teamDefinitionRepository.Update(teamDefinition);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteTeamDefinition(int teamDefinitionId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

        //         teamDefinitionRepository.Remove(teamDefinitionId);
        //     });
        // }

        // public TeamDefinition GetTeamDefinition(int teamDefinitionId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

        //         TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get(teamDefinitionId);
        //         if (teamDefinitionEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return teamDefinitionEntity;
        //     });
        // }

        // public TeamDefinition GetTeamDefinitionByCode(string code)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

        //         var setUp = GetSetup();

        //         TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c=>c.Code == code && c.Year == setUp.Year).FirstOrDefault();

        //         return teamDefinitionEntity;
        //     });
        // }

        // public TeamDefinition[] GetAllTeamDefinitions()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<TeamDefinition> teamDefinition = teamDefinitionRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         return teamDefinition.ToArray();
        //     });
        // }


        // #endregion

        // #region TeamClassificationType operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public TeamClassificationType UpdateTeamClassificationType(TeamClassificationType teamClassificationType)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

        //         TeamClassificationType updatedEntity = null;

        //         if (teamClassificationType.TeamClassificationTypeId == 0)
        //         {
        //             teamClassificationType.Year = GetSetup().Year;
        //             updatedEntity = teamClassificationTypeRepository.Add(teamClassificationType);
        //         }
        //         else
        //             updatedEntity = teamClassificationTypeRepository.Update(teamClassificationType);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteTeamClassificationType(int teamClassificationTypeId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

        //         teamClassificationTypeRepository.Remove(teamClassificationTypeId);
        //     });
        // }

        // public TeamClassificationType GetTeamClassificationType(int teamClassificationTypeId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

        //         TeamClassificationType teamClassificationTypeEntity = teamClassificationTypeRepository.Get(teamClassificationTypeId);
        //         if (teamClassificationTypeEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("TeamClassificationType with ID of {0} is not in database", teamClassificationTypeId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return teamClassificationTypeEntity;
        //     });
        // }

        // public TeamClassificationType[] GetAllTeamClassificationTypes()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<TeamClassificationType> teamClassificationType = teamClassificationTypeRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         return teamClassificationType.ToArray();
        //     });
        // }

        // #endregion

        // #region TeamClassification operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public TeamClassification UpdateTeamClassification(TeamClassification teamClassification)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

        //         TeamClassification updatedEntity = null;

        //         if (teamClassification.TeamClassificationId == 0)
        //         {
        //             teamClassification.Year = GetSetup().Year;
        //             updatedEntity = teamClassificationRepository.Add(teamClassification);
        //         }
        //         else
        //             updatedEntity = teamClassificationRepository.Update(teamClassification);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteTeamClassification(int teamClassificationId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

        //         teamClassificationRepository.Remove(teamClassificationId);
        //     });
        // }

        // public TeamClassification GetTeamClassification(int teamClassificationId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

        //         TeamClassification teamClassificationEntity = teamClassificationRepository.Get(teamClassificationId);
        //         if (teamClassificationEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("TeamClassification with ID of {0} is not in database", teamClassificationId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return teamClassificationEntity;
        //     });
        // }

        // public TeamClassification[] GetAllTeamClassifications( )
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<TeamClassification> teamClassification = teamClassificationRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         return teamClassification.ToArray();
        //     });
        // }

        // public TeamClassification[] GetTeamClassifications(string typeCode)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();
        //         ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

        //         var setup = GetSetup();

        //         var classificationType = teamClassificationTypeRepository.Get().Where(c => c.Code == typeCode).FirstOrDefault();

        //         IEnumerable<TeamClassification> teamClassification = null;

        //         if (classificationType != null)
        //             teamClassification = teamClassificationRepository.Get().Where(c => c.Year == setup.Year && c.ClassificationTypeCode == typeCode && c.Level == classificationType.MaximumLevel ).ToArray();

        //         return teamClassification.ToArray();
        //     });
        // }

        // #endregion        

        // #region Team operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public Team UpdateTeam(Team team)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         Team updatedEntity = null;

        //         if (team.TeamId == 0)
        //         {
        //             team.Year = GetSetup().Year;
        //             updatedEntity = teamRepository.Add(team);
        //         }
        //         else
        //             updatedEntity = teamRepository.Update(team);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteTeam(int teamId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         teamRepository.Remove(teamId);
        //     });
        // }

        // public Team GetTeam(int teamId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         Team teamEntity = teamRepository.Get(teamId);
        //         if (teamEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("Team with ID of {0} is not in database", teamId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return teamEntity;
        //     });
        // }

        // public Team[] GetParentTeams(string definitionCode)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         var setUp = setupRepository.Get().FirstOrDefault();

        //         var teamDefinition = teamDefinitionRepository.Get().Where(c => c.Code == definitionCode && c.Year == setUp.Year).FirstOrDefault();
        //         var parentDefinition = teamDefinitionRepository.Get().Where(c => c.Position == (teamDefinition.Position + 1)).FirstOrDefault();

        //         Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == parentDefinition.Code && c.Year == setUp.Year).ToArray();

        //         return teams;
        //     });
        // }

        // public Team[] GetTeamByLevel(int level)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         var setUp = setupRepository.Get().FirstOrDefault();

        //         var teamDefinition = teamDefinitionRepository.Get().Where(c=>c.Position == level).FirstOrDefault();

        //          Team[] teams = null;

        //         if (teamDefinition != null)
        //             teams = teamRepository.Get().Where(c => c.DefinitionCode == teamDefinition.Code && c.Year == setUp.Year).ToArray();

        //         return teams;
        //     });
        // }

        // public Team[] GetTeamByDefinition(string definitionCode)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //       //  ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         var setUp = setupRepository.Get().FirstOrDefault();

        //         Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == definitionCode && c.Year == setUp.Year).ToArray();

        //         return teams;
        //     });
        // }

        // public TeamData[] GetTeams()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

        //         ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         var setUp = setupRepository.Get().FirstOrDefault();

        //         List<TeamData> team = new List<TeamData>();
        //         IEnumerable<TeamInfo> teamInfos = teamRepository.GetTeams().OrderByDescending(c => c.Team.DefinitionCode).Where(c => c.Team.Year == setUp.Year).ToArray();

        //         foreach (var teamInfo in teamInfos)
        //         {
        //             team.Add(
        //                 new TeamData
        //                 {
        //                     TeamId = teamInfo.Team.EntityId,
        //                     Code = teamInfo.Team.Code,
        //                     Name = teamInfo.Team.Name,
        //                     //ParentId = teamInfo.Team.ParentId,
        //                     ParentCode = teamInfo.Parent != null ? teamInfo.Parent.Code : string.Empty,
        //                     ParentName = teamInfo.Parent != null ? teamInfo.Parent.Name : "",
        //                     DefinitionCode = teamInfo.Team.DefinitionCode,
        //                     CanClassified = true,
        //                     CanUseStaffsId = true,
        //                     StaffsId = teamInfo.Team.StaffsId,
        //                     Position = 1
        //                 });
        //         }

        //         return team.ToArray();
        //     });
        // }

        // #endregion

        // #region TeamClassificationMap operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public TeamClassificationMap UpdateTeamClassificationMap(TeamClassificationMap teamClassificationMap)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

        //         TeamClassificationMap updatedEntity = null;

        //         if (teamClassificationMap.TeamClassificationMapId == 0)
        //         {
        //             teamClassificationMap.Year = GetSetup().Year;
        //             updatedEntity = teamClassificationMapRepository.Add(teamClassificationMap);
        //         }
        //         else
        //             updatedEntity = teamClassificationMapRepository.Update(teamClassificationMap);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteTeamClassificationMap(int teamClassificationMapId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

        //         teamClassificationMapRepository.Remove(teamClassificationMapId);
        //     });
        // }

        // public TeamClassificationMap GetTeamClassificationMap(int teamClassificationMapId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

        //         TeamClassificationMap teamClassificationMapEntity = teamClassificationMapRepository.Get(teamClassificationMapId);
        //         if (teamClassificationMapEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("TeamClassificationMap with ID of {0} is not in database", teamClassificationMapId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return teamClassificationMapEntity;
        //     });
        // }

        // public TeamClassificationMap[] GetAllTeamClassificationMaps(string misCode,string definitionCode)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<TeamClassificationMap> teamClassificationMap = teamClassificationMapRepository.Get().Where(c => c.Year == setup.Year && c.DefinitionCode == definitionCode && c.MisCode == misCode).ToArray();

        //         return teamClassificationMap.ToArray();
        //     });
        // }

        //#endregion

        // #region AccountOfficerDetail operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public AccountOfficerDetail UpdateAccountOfficerDetail(AccountOfficerDetail accountOfficerDetail)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

        //         AccountOfficerDetail updatedEntity = null;

        //         if (accountOfficerDetail.AccountOfficerDetailId == 0)
        //         {
        //             accountOfficerDetail.Year = GetSetup().Year;
        //             updatedEntity = accountOfficerDetailRepository.Add(accountOfficerDetail);
        //         }
        //         else
        //             updatedEntity = accountOfficerDetailRepository.Update(accountOfficerDetail);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteAccountOfficerDetail(int accountOfficerDetailId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

        //         accountOfficerDetailRepository.Remove(accountOfficerDetailId);
        //     });
        // }

        // public AccountOfficerDetail GetAccountOfficerDetail(int accountOfficerDetailId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

        //         AccountOfficerDetail accountOfficerDetailEntity = accountOfficerDetailRepository.Get(accountOfficerDetailId);
        //         if (accountOfficerDetailEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("AccountOfficerDetail with ID of {0} is not in database", accountOfficerDetailId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return accountOfficerDetailEntity;
        //     });
        // }

        // public AccountOfficerDetail[] GetAllAccountOfficerDetails()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<AccountOfficerDetail> accountOfficerDetail = accountOfficerDetailRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         return accountOfficerDetail.ToArray();
        //     });
        // }

        // #endregion

        // #region BranchDefaultMIS operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public BranchDefaultMIS UpdateBranchDefaultMIS(BranchDefaultMIS branchDefaultMIS)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

        //         BranchDefaultMIS updatedEntity = null;

        //         if (branchDefaultMIS.BranchDefaultMISId == 0)
        //         {
        //             branchDefaultMIS.Year = GetSetup().Year;
        //             updatedEntity = branchDefaultMISRepository.Add(branchDefaultMIS);
        //         }
        //         else
        //             updatedEntity = branchDefaultMISRepository.Update(branchDefaultMIS);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteBranchDefaultMIS(int branchDefaultMISId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

        //         branchDefaultMISRepository.Remove(branchDefaultMISId);
        //     });
        // }

        // public BranchDefaultMIS GetBranchDefaultMIS(int branchDefaultMISId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

        //         BranchDefaultMIS branchDefaultMISEntity = branchDefaultMISRepository.Get(branchDefaultMISId);
        //         if (branchDefaultMISEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("BranchDefaultMIS with ID of {0} is not in database", branchDefaultMISId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return branchDefaultMISEntity;
        //     });
        // }

        // public BranchDefaultMIS[] GetAllBranchDefaultMISs()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<BranchDefaultMIS> branchDefaultMIS = branchDefaultMISRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         return branchDefaultMIS.ToArray();
        //     });
        // }

        //   #endregion

        // #region ManagementTree operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public ManagementTree UpdateManagementTree(ManagementTree managementTree)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

        //         ManagementTree updatedEntity = null;

        //         if (managementTree.ManagementTreeId == 0)
        //         {
        //             managementTree.Year = GetSetup().Year;
        //             updatedEntity = managementTreeRepository.Add(managementTree);
        //         }
        //         else
        //             updatedEntity = managementTreeRepository.Update(managementTree);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteManagementTree(int managementTreeId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

        //         managementTreeRepository.Remove(managementTreeId);
        //     });
        // }

        // public ManagementTree GetManagementTree(int managementTreeId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

        //         ManagementTree managementTreeEntity = managementTreeRepository.Get(managementTreeId);
        //         if (managementTreeEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("ManagementTree with ID of {0} is not in database", managementTreeId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return managementTreeEntity;
        //     });
        // }

        // public ManagementTreeData[] GetAllManagementTrees()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

        //         //var setup = GetSetup();

        //         //IEnumerable<ManagementTreeData> managementTree = managementTreeRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         List<ManagementTreeData> managementTree = new List<ManagementTreeData>();
        //         IEnumerable<ManagementTreeInfo> managementTreeInfos = managementTreeRepository.GetManagementTrees().ToArray();

        //         foreach (var managementTreeInfo in managementTreeInfos)
        //         {
        //             managementTree.Add(
        //                 new ManagementTreeData
        //                 {
        //                     ManagementTreeId = managementTreeInfo.ManagementTree.EntityId,
        //                     AccountNo=managementTreeInfo.ManagementTree.AccountNo,
        //                     TeamDefinitionCode = managementTreeInfo.ManagementTree.TeamDefinitionCode,
        //                     TeamDefinitionName = managementTreeInfo.TeamDefinition.Name,
        //                     TeamCode = managementTreeInfo.ManagementTree.TeamCode,
        //                     TeamName = managementTreeInfo.Team.Name,
        //                     AccountOfficerDefinitionCode = managementTreeInfo.ManagementTree != null ?  managementTreeInfo.ManagementTree.AccountOfficerDefinitionCode: string.Empty,
        //                     AccountOfficerDefinitionName = managementTreeInfo.AccountOfficerDefinition != null ? managementTreeInfo.AccountOfficerDefinition.Name : string.Empty,
        //                     AccountOfficerCode = managementTreeInfo.ManagementTree != null ? managementTreeInfo.ManagementTree.AccountOfficerCode : string.Empty,
        //                     AccountOfficerName = managementTreeInfo.AccountOfficer != null ? managementTreeInfo.AccountOfficer.Name : string.Empty,
        //                     Rate= managementTreeInfo.ManagementTree.Rate,
        //                     Year = managementTreeInfo.ManagementTree.Year,
        //                     Active = managementTreeInfo.ManagementTree.Active
        //                 });
        //         }


        //         return managementTree.ToArray();
        //     });
        // }

        // #endregion

        // #region AccountMIS operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public AccountMIS UpdateAccountMIS(AccountMIS accountMIS)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

        //         AccountMIS updatedEntity = null;

        //         if (accountMIS.AccountMISId == 0)
        //         {
        //             accountMIS.Year = GetSetup().Year;
        //             updatedEntity = accountMISRepository.Add(accountMIS);
        //         }
        //         else
        //             updatedEntity = accountMISRepository.Update(accountMIS);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteAccountMIS(int accountMISId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

        //         accountMISRepository.Remove(accountMISId);
        //     });
        // }

        // public AccountMIS GetAccountMIS(int accountMISId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

        //         AccountMIS accountMISEntity = accountMISRepository.Get(accountMISId);
        //         if (accountMISEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("AccountMIS with ID of {0} is not in database", accountMISId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return accountMISEntity;
        //     });
        // }

        // public AccountMISData[] GetAllAccountMISs()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

        //         //var setup = GetSetup();

        //         //IEnumerable<AccountMIS> accountMIS = accountMISRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         //return accountMIS.ToArray();

        //         ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         var setUp = setupRepository.Get().FirstOrDefault();

        //         List<AccountMISData> accountMIS = new List<AccountMISData>();
        //         IEnumerable<AccountMISInfo> accountMISInfos = accountMISRepository.GetAccountMISs().Where(c => c.AccountMIS.Year == setUp.Year).ToArray();

        //         foreach (var accountMISInfo in accountMISInfos)
        //         {
        //             accountMIS.Add(
        //                 new AccountMISData
        //                 {
        //                     AccountMISId = accountMISInfo.AccountMIS.EntityId,
        //                     AccountNo = accountMISInfo.AccountMIS.AccountNo,
        //                     TeamDefinitionCode = accountMISInfo.AccountMIS.TeamDefinitionCode,
        //                     TeamDefinitionName = accountMISInfo.TeamDefinition.Name,
        //                     TeamCode = accountMISInfo.AccountMIS.TeamCode,
        //                     TeamName = accountMISInfo.Team.Name,
        //                     AccountOfficerDefinitionCode = accountMISInfo.AccountMIS != null ? accountMISInfo.AccountMIS.AccountOfficerDefinitionCode : string.Empty,
        //                     AccountOfficerDefinitionName = accountMISInfo.AccountOfficerDefinition != null ? accountMISInfo.AccountOfficerDefinition.Name : string.Empty,
        //                     AccountOfficerCode = accountMISInfo.AccountMIS != null ? accountMISInfo.AccountMIS.AccountOfficerCode : string.Empty,
        //                     AccountOfficerName = accountMISInfo.AccountOfficer != null ? accountMISInfo.AccountOfficer.Name : string.Empty,
        //                     AccountName = accountMISInfo.CustAccount != null ? accountMISInfo.CustAccount.AccountName : string.Empty, 
        //                     //Rate = accountMISInfo.AccountMIS.Rate,
        //                     Year = accountMISInfo.AccountMIS.Year,
        //                     Active = accountMISInfo.AccountMIS.Active
        //                 });
        //         }


        //         return accountMIS.ToArray();
        //     });
        // }

        // public void DeleteSelectedIds(string selectedIds)
        // {
        //     var connectionString = GetDataConnection();

        //     using (var con = new SqlConnection(connectionString))
        //     {
        //         var cmd = new SqlCommand("MultipleDeletion", con);
        //         cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //         cmd.Parameters.Add(new SqlParameter
        //         {
        //             ParameterName = "@IdLists",
        //             Value = selectedIds,
        //         });
        //         cmd.Parameters.Add(new SqlParameter
        //         {
        //             ParameterName = "@pageOwner",
        //             Value = "AcctMIS"
        //         });
        //         cmd.CommandTimeout = 0;

        //         con.Open();

        //         cmd.ExecuteNonQuery();

        //         con.Close();
        //     }

        // }

        // #endregion

        // #region MISReplacement operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public MISReplacement UpdateMISReplacement(MISReplacement misReplacement)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

        //         MISReplacement updatedEntity = null;

        //         if (misReplacement.MISReplacementId == 0)
        //         {
        //             misReplacement.Year = GetSetup().Year;
        //             updatedEntity = misReplacementRepository.Add(misReplacement);
        //         }     
        //         else
        //             updatedEntity = misReplacementRepository.Update(misReplacement);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteMISReplacement(int misReplacementId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

        //         misReplacementRepository.Remove(misReplacementId);
        //     });
        // }

        // public MISReplacement GetMISReplacement(int misReplacementId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

        //         MISReplacement misReplacementEntity = misReplacementRepository.Get(misReplacementId);
        //         if (misReplacementEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("MISReplacement with ID of {0} is not in database", misReplacementId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return misReplacementEntity;
        //     });
        // }

        // public MISReplacement[] GetAllMISReplacements()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

        //         var setup = GetSetup();

        //         IEnumerable<MISReplacement> misReplacement = misReplacementRepository.Get().Where(c => c.Year == setup.Year).ToArray();

        //         return misReplacement.ToArray();
        //     });
        // }

        //  #endregion

        // #region SetUp operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public SetUp UpdateMPRSetup(SetUp setUp)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

        //         SetUp updatedEntity = null;

        //         if (setUp.SetupId == 0)
        //             updatedEntity = setUpRepository.Add(setUp);
        //         else
        //             updatedEntity = setUpRepository.Update(setUp);

        //         return updatedEntity;
        //     });
        // }

        // public SetUp GetFirstMPRSetup()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

        //         SetUp setUpEntity = setUpRepository.Get().FirstOrDefault();
        //         //if (setUpEntity == null)
        //         //{
        //         //    NotFoundException ex = new NotFoundException(string.Format("SetUp with ID of {0} is not in database", setUpId));
        //         //    throw new FaultException<NotFoundException>(ex, ex.Message);
        //         //}

        //         return setUpEntity;
        //     });
        // }

        // #endregion

        // #region TransferPrice operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public TransferPrice UpdateTransferPrice(TransferPrice transferPrice)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

        //         TransferPrice updatedEntity = null;

        //         if (transferPrice.TransferPriceId == 0)
        //         {
        //             transferPrice.Year = GetSetup().Year;
        //             updatedEntity = transferPriceRepository.Add(transferPrice);
        //         }
        //         else
        //             updatedEntity = transferPriceRepository.Update(transferPrice);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteTransferPrice(int transferPriceId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

        //         transferPriceRepository.Remove(transferPriceId);
        //     });
        // }

        // public TransferPrice GetTransferPrice(int transferPriceId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

        //         TransferPrice transferPriceEntity = transferPriceRepository.Get(transferPriceId);
        //         if (transferPriceEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("TransferPrice with ID of {0} is not in database", transferPriceId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return transferPriceEntity;
        //     });
        // }

        // public TransferPriceData[] GetAllTransferPrices()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

        //         //var setUp = GetSetup();

        //         //IEnumerable<TransferPrice> transferPrice = transferPriceRepository.Get().Where (c=>c.Year == setUp.Year).ToArray();

        //         List<TransferPriceData> transferPrice = new List<TransferPriceData>();
        //         IEnumerable<TransferPriceInfo> transferPriceInfos = transferPriceRepository.GetTransferPrices().ToArray();

        //         foreach (var transferPriceInfo in transferPriceInfos)
        //         {
        //             transferPrice.Add(
        //                 new TransferPriceData
        //                 {
        //                     TransferPriceId = transferPriceInfo.TransferPrice.EntityId,
        //                     ProductCode = transferPriceInfo.TransferPrice.ProductCode,
        //                     ProductName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.Product.Name : string.Empty,
        //                     CaptionCode = transferPriceInfo.TransferPrice.CaptionCode,
        //                     CaptionName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.BSCaption.CaptionName: string.Empty,
        //                     Rate = transferPriceInfo.TransferPrice.Rate,
        //                     DefinitionCode = transferPriceInfo.TransferPrice.DefinitionCode,
        //                     DefinitionName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.TeamDefinition.Name : string.Empty,
        //                     MisCode = transferPriceInfo.TransferPrice.MisCode,
        //                     MisName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.Team.Name : string.Empty,
        //                     Year = transferPriceInfo.TransferPrice.Year,
        //                     Period = transferPriceInfo.TransferPrice.Period,
        //                     SolutionId = transferPriceInfo.TransferPrice.SolutionId,
        //                     SolutionName = string.Empty,
        //                     CompanyCode = transferPriceInfo.TransferPrice.CompanyCode,
        //                     Active = transferPriceInfo.TransferPrice.Active
        //                 });
        //         }


        //         return transferPrice.ToArray();
        //     });
        // }
        // #endregion

        // #region AccountTransferPrice operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public AccountTransferPrice UpdateAccountTransferPrice(AccountTransferPrice accountTransferPrice)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

        //         AccountTransferPrice updatedEntity = null;

        //         if (accountTransferPrice.AccountTransferPriceId == 0)
        //         {
        //             accountTransferPrice.Year = GetSetup().Year;
        //             updatedEntity = accountTransferPriceRepository.Add(accountTransferPrice);
        //         }
        //         else
        //             updatedEntity = accountTransferPriceRepository.Update(accountTransferPrice);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteAccountTransferPrice(int accountTransferPriceId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

        //         accountTransferPriceRepository.Remove(accountTransferPriceId);
        //     });
        // }

        // public AccountTransferPrice GetAccountTransferPrice(int accountTransferPriceId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

        //         AccountTransferPrice accountTransferPriceEntity = accountTransferPriceRepository.Get(accountTransferPriceId);
        //         if (accountTransferPriceEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("AccountTransferPrice with ID of {0} is not in database", accountTransferPriceId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return accountTransferPriceEntity;
        //     });
        // }

        // public AccountTransferPriceData[] GetAllAccountTransferPrices()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

        //        // ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         //var setUp = setupRepository.Get().FirstOrDefault();

        //         List<AccountTransferPriceData> AccountTransferPrice = new List<AccountTransferPriceData>();
        //         IEnumerable<AccountTransferPriceInfo> accountTransferPriceInfos = accountTransferPriceRepository.GetAccountTransferPrices().ToArray();

        //         foreach (var accountTransferPriceInfo in accountTransferPriceInfos)
        //         {
        //             AccountTransferPrice.Add(
        //                 new AccountTransferPriceData
        //                 {
        //                     AccountTransferPriceId = accountTransferPriceInfo.AccountTransferPrice.EntityId,
        //                     AccountNo = accountTransferPriceInfo.AccountTransferPrice.AccountNo,
        //                     Category = accountTransferPriceInfo.AccountTransferPrice.Category,
        //                     CategoryName = accountTransferPriceInfo.AccountTransferPrice.Category.ToString(),
        //                     Rate = accountTransferPriceInfo.AccountTransferPrice.Rate,
        //                     Year = accountTransferPriceInfo.AccountTransferPrice.Year,
        //                     Period = accountTransferPriceInfo.AccountTransferPrice.Period,
        //                     SolutionId = accountTransferPriceInfo.AccountTransferPrice.SolutionId,
        //                     SolutionName= "",
        //                     AccountName = accountTransferPriceInfo.CustAccount !=null ?accountTransferPriceInfo.CustAccount.AccountName : string.Empty ,
        //                     Active = true,

        //                 });
        //         }

        //         return AccountTransferPrice.ToArray();
        //     });
        // }

        // #endregion

        // #region GeneralTransferPrice operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public GeneralTransferPrice UpdateGeneralTransferPrice(GeneralTransferPrice generalTransferPrice)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

        //         GeneralTransferPrice updatedEntity = null;

        //         if (generalTransferPrice.GeneralTransferPriceId == 0)
        //         {
        //             generalTransferPrice.Year = GetSetup().Year;
        //             updatedEntity = generalTransferPriceRepository.Add(generalTransferPrice);
        //         }
        //         else
        //             updatedEntity = generalTransferPriceRepository.Update(generalTransferPrice);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteGeneralTransferPrice(int generalTransferPriceId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

        //         generalTransferPriceRepository.Remove(generalTransferPriceId);
        //     });
        // }

        // public GeneralTransferPrice GetGeneralTransferPrice(int generalTransferPriceId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

        //         GeneralTransferPrice generalTransferPriceEntity = generalTransferPriceRepository.Get(generalTransferPriceId);
        //         if (generalTransferPriceEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("General Transfer Price with ID of {0} is not in database", generalTransferPriceId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return generalTransferPriceEntity;
        //     });
        // }

        // public GeneralTransferPriceData[] GetAllGeneralTransferPrices()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

        //         // ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
        //         //var setUp = setupRepository.Get().FirstOrDefault();

        //         List<GeneralTransferPriceData> GeneralTransferPrice = new List<GeneralTransferPriceData>();
        //         IEnumerable<GeneralTransferPrice> generalTransferPriceInfos = generalTransferPriceRepository.Get().ToArray();

        //         foreach (var generalTransferPriceInfo in generalTransferPriceInfos)
        //         {
        //             GeneralTransferPrice.Add(
        //                 new GeneralTransferPriceData
        //                 {
        //                     GeneralTransferPriceId = generalTransferPriceInfo.EntityId,
        //                     Category = generalTransferPriceInfo.Category,
        //                     CategoryName = generalTransferPriceInfo.Category.ToString(),
        //                     CurrencyType = generalTransferPriceInfo.CurrencyType,
        //                     CurrencyTypeName = generalTransferPriceInfo.CurrencyType.ToString(),
        //                     Rate = generalTransferPriceInfo.Rate,
        //                     Year = generalTransferPriceInfo.Year,
        //                     Period = generalTransferPriceInfo.Period,
        //                     DefinitionCode = generalTransferPriceInfo.DefinitionCode,
        //                     MISCode = generalTransferPriceInfo.MISCode,
        //                     CompanyCode = generalTransferPriceInfo.CompanyCode,
        //                     Active = true,

        //                 });
        //         }

        //         return GeneralTransferPrice.ToArray();
        //     });
        // }

        // public void DeleteGTPSelectedIds(string selectedIds)
        // {
        //     var connectionString = GetDataConnection();

        //     using (var con = new SqlConnection(connectionString))
        //     {
        //         var cmd = new SqlCommand("MultipleDeletion", con);
        //         cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //         cmd.Parameters.Add(new SqlParameter
        //         {
        //             ParameterName = "@IdLists",
        //             Value = selectedIds,
        //         });
        //         cmd.Parameters.Add(new SqlParameter
        //         {
        //             ParameterName = "@pageOwner",
        //             Value = "GTP"
        //         });
        //         cmd.CommandTimeout = 0;

        //         con.Open();

        //         cmd.ExecuteNonQuery();

        //         con.Close();
        //     }

        // }

        // #endregion

        // #region CustAccount operations

        // [OperationBehavior(TransactionScopeRequired = true)]


        // public CustAccount[] GetAllCustAccounts()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ICustAccountRepository CustAccountRepository = _DataRepositoryFactory.GetDataRepository<ICustAccountRepository>();

        //         IEnumerable<CustAccount> CustAccounts = CustAccountRepository.Get().ToArray();

        //         return CustAccounts.ToArray();
        //     });
        // }

        // public CustAccount[] GetCustAccounts(string searchType, string searchValue, int number)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR,  GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         ICustAccountRepository CustAccountRepository = _DataRepositoryFactory.GetDataRepository<ICustAccountRepository>();
        //         List<CustAccount> CustAccounts = CustAccountRepository.GetCustomerAccountBySearch(searchType, searchValue, number);


        //         return CustAccounts.ToArray();
        //     });
        // }


        // #endregion

        // #region BSExemption operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public BSExemption UpdateBSExemption(BSExemption bsExemption)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();

        //         BSExemption updatedEntity = null;

        //         if (bsExemption.BSExemptionId == 0)
        //         {

        //             updatedEntity = bsExemptionRepository.Add(bsExemption);
        //         }
        //         else
        //             updatedEntity = bsExemptionRepository.Update(bsExemption);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteBSExemption(int bsExemptionId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();

        //         bsExemptionRepository.Remove(bsExemptionId);
        //     });
        // }

        // public BSExemption GetBSExemption(int bsExemptionId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();

        //         BSExemption bsExemptionEntity = bsExemptionRepository.Get(bsExemptionId);
        //         if (bsExemptionEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("BSExemption with ID of {0} is not in database", bsExemptionId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return bsExemptionEntity;
        //     });
        // }

        // public BSExemption[] GetAllBSExemptions()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();


        //         IEnumerable<BSExemption> bsExemption = bsExemptionRepository.Get().ToArray();

        //         return bsExemption.ToArray();
        //     });
        // }

        // #endregion

        // #region MemoAccountMap operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public MemoAccountMap UpdateMemoAccountMap(MemoAccountMap memoAccountMap)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

        //         MemoAccountMap updatedEntity = null;

        //         if (memoAccountMap.MemoAccountMapId == 0)
        //         {
        //            updatedEntity = memoAccountMapRepository.Add(memoAccountMap);
        //         }
        //         else
        //             updatedEntity = memoAccountMapRepository.Update(memoAccountMap);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteMemoAccountMap(int memoAccountMapId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

        //         memoAccountMapRepository.Remove(memoAccountMapId);
        //     });
        // }

        // public MemoAccountMap GetMemoAccountMap(int memoAccountMapId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

        //         MemoAccountMap memoAccountMapEntity = memoAccountMapRepository.Get(memoAccountMapId);
        //         if (memoAccountMapEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("MemoAccountMap with ID of {0} is not in database", memoAccountMapId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return memoAccountMapEntity;
        //     });
        // }

        // public MemoAccountMapData[] GetAllMemoAccountMaps()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

        //         //var setUp = GetSetup();

        //         //IEnumerable<MemoAccountMap> memoAccountMap = memoAccountMapRepository.Get().Where (c=>c.Year == setUp.Year).ToArray();

        //         List<MemoAccountMapData> memoAccountMap = new List<MemoAccountMapData>();
        //         IEnumerable<MemoAccountMapInfo> memoAccountMapInfos = memoAccountMapRepository.GetMemoAccountMaps().ToArray();

        //         foreach (var memoAccountMapInfo in memoAccountMapInfos)
        //         {
        //             memoAccountMap.Add(
        //                 new MemoAccountMapData
        //                 {
        //                      MemoAccountMapId = memoAccountMapInfo.MemoAccountMap.EntityId,
        //                      AccountNo = memoAccountMapInfo.MemoAccountMap.AccountNo,
        //                      Code = memoAccountMapInfo.MemoAccountMap.Code,
        //                      Name = memoAccountMapInfo.MemoUnits != null ? memoAccountMapInfo.MemoUnits.Name : string.Empty,
        //                      AccountName = memoAccountMapInfo.CustAccount !=null? memoAccountMapInfo.CustAccount.AccountName: string.Empty,
        //                    //  bsCaptionInfo.Parent != null ? bsCaptionInfo.Parent.EntityId : 0,
        //                      Active = memoAccountMapInfo.MemoAccountMap.Active
        //                 });
        //         }



        //         return memoAccountMap.ToArray();
        //     });
        // }
        // #endregion

        // #region MemoGLMap operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public MemoGLMap UpdateMemoGLMap(MemoGLMap memoGLMap)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

        //         MemoGLMap updatedEntity = null;

        //         if (memoGLMap.MemoGLMapId == 0)
        //         {

        //             updatedEntity = memoGLMapRepository.Add(memoGLMap);
        //         }
        //         else
        //             updatedEntity = memoGLMapRepository.Update(memoGLMap);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteMemoGLMap(int memoGLMapId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

        //         memoGLMapRepository.Remove(memoGLMapId);
        //     });
        // }

        // public MemoGLMap GetMemoGLMap(int memoGLMapId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

        //         MemoGLMap memoGLMapEntity = memoGLMapRepository.Get(memoGLMapId);
        //         if (memoGLMapEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("MemoGLMap with ID of {0} is not in database", memoGLMapId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return memoGLMapEntity;
        //     });
        // }


        // public MemoGLMapData[] GetAllMemoGLMaps()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

        //         List<MemoGLMapData> memoGLMap = new List<MemoGLMapData>();
        //         IEnumerable<MemoGLMapInfo> memoGLMapInfos = memoGLMapRepository.GetMemoGLMaps().ToArray();

        //         foreach (var memoGLMapInfo in memoGLMapInfos)
        //         {
        //             memoGLMap.Add(
        //                 new MemoGLMapData
        //                 {
        //                     MemoGLMapId = memoGLMapInfo.MemoGLMap.EntityId,
        //                     Code = memoGLMapInfo.MemoGLMap.Code,
        //                     Name = memoGLMapInfo.MemoUnits.Name,
        //                     GLCode= memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.GL_Code : string.Empty,
        //                     GLDescription = memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.Description : string.Empty,
        //                     Active = memoGLMapInfo.MemoGLMap.Active
        //                 });
        //         }

        //         //memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.GL_Code : string.Empty,
        //         //memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.Description : string.Empty,

        //         return memoGLMap.ToArray();
        //     });
        // }

        // #endregion

        // #region MemoProductMap operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public MemoProductMap UpdateMemoProductMap(MemoProductMap memoProductMap)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

        //         MemoProductMap updatedEntity = null;

        //         if (memoProductMap.MemoProductMapId == 0)
        //         {
        //             updatedEntity = memoProductMapRepository.Add(memoProductMap);
        //         }
        //         else
        //             updatedEntity = memoProductMapRepository.Update(memoProductMap);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteMemoProductMap(int memoProductMapId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

        //         memoProductMapRepository.Remove(memoProductMapId);
        //     });
        // }

        // public MemoProductMap GetMemoProductMap(int memoProductMapId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

        //         MemoProductMap memoProductMapEntity = memoProductMapRepository.Get(memoProductMapId);
        //         if (memoProductMapEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("MemoProductMap with ID of {0} is not in database", memoProductMapId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return memoProductMapEntity;
        //     });
        // }

        // public MemoProductMapData[] GetAllMemoProductMaps()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

        //         List<MemoProductMapData> memoProductMap = new List<MemoProductMapData>();
        //         IEnumerable<MemoProductMapInfo> memoProductMapInfos = memoProductMapRepository.GetMemoProductMaps().ToArray();

        //         foreach (var memoProductMapInfo in memoProductMapInfos)
        //         {
        //             memoProductMap.Add(
        //                 new MemoProductMapData
        //                 {
        //                     MemoProductMapId = memoProductMapInfo.MemoProductMap.EntityId,
        //                     ProductCode = memoProductMapInfo.MemoProductMap.ProductCode,
        //                     ProductName = memoProductMapInfo.Product!=null ? memoProductMapInfo.Product.Name:string.Empty,
        //                      Code = memoProductMapInfo.MemoProductMap.Code,
        //                      UnitName = memoProductMapInfo.MemoUnits.Name,
        //                     Active = memoProductMapInfo.MemoProductMap.Active
        //                 });
        //         }


        //         return memoProductMap.ToArray();
        //     });
        // }
        // #endregion

        // #region MemoUnits operations

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public MemoUnits UpdateMemoUnits(MemoUnits memoUnit)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();

        //         MemoUnits updatedEntity = null;

        //         if (memoUnit.MemoUnitsId == 0)
        //         {

        //             updatedEntity = memoUnitRepository.Add(memoUnit);
        //         }
        //         else
        //             updatedEntity = memoUnitRepository.Update(memoUnit);

        //         return updatedEntity;
        //     });
        // }

        // [OperationBehavior(TransactionScopeRequired = true)]
        // public void DeleteMemoUnits(int memoUnitId)
        // {
        //     ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();

        //         memoUnitRepository.Remove(memoUnitId);
        //     });
        // }

        // public MemoUnits GetMemoUnits(int memoUnitId)
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();

        //         MemoUnits memoUnitEntity = memoUnitRepository.Get(memoUnitId);
        //         if (memoUnitEntity == null)
        //         {
        //             NotFoundException ex = new NotFoundException(string.Format("MemoUnits with ID of {0} is not in database", memoUnitId));
        //             throw new FaultException<NotFoundException>(ex, ex.Message);
        //         }

        //         return memoUnitEntity;
        //     });
        // }

        // public MemoUnits[] GetAllMemoUnits()
        // {
        //     return ExecuteFaultHandledOperation(() =>
        //     {
        //         var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER,GROUP_SUPER_BUSINESS,GROUP_SUPER_BUSINESS };
        //         AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //         IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();


        //         IEnumerable<MemoUnits> memoUnit = memoUnitRepository.Get().ToArray();

        //         return memoUnit.ToArray();
        //     });
        // }

        // #endregion

        #region UserMIS operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public UserMIS UpdateUserMIS(UserMIS userMIS)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

                UserMIS updatedEntity = null;

                if (userMIS.UserMISId == 0)
                {
                    updatedEntity = userMISRepository.Add(userMIS);
                }
                else
                    updatedEntity = userMISRepository.Update(userMIS);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUserMIS(int userMISId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

                userMISRepository.Remove(userMISId);
            });
        }

        public UserMIS GetUserMIS(int userMISId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

                UserMIS userMISEntity = userMISRepository.Get(userMISId);
                if (userMISEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UserMIS with ID of {0} is not in database", userMISId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return userMISEntity;
            });
        }

        public UserMIS GetUserMISByLoginID(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

                var setUp = GetSetup();

                UserMIS userMISEntity = userMISRepository.Get().Where(c => c.LoginID == loginID).FirstOrDefault();

                return userMISEntity;
            });
        }

        public UserMIS[] GetAllUserMISs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserMISRepository userMISRepository = _DataRepositoryFactory.GetDataRepository<IUserMISRepository>();

                var setup = GetSetup();

                IEnumerable<UserMIS> userMIS = userMISRepository.Get().ToArray();

                return userMIS.ToArray();
            });
        }


        #endregion

        #region UserClassificationMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public UserClassificationMap UpdateUserClassificationMap(UserClassificationMap userClassificationMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

                UserClassificationMap updatedEntity = null;

                if (userClassificationMap.UserClassificationMapId == 0)
                {
                    updatedEntity = userClassificationMapRepository.Add(userClassificationMap);
                }
                else
                    updatedEntity = userClassificationMapRepository.Update(userClassificationMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteUserClassificationMap(int userClassificationMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

                userClassificationMapRepository.Remove(userClassificationMapId);
            });
        }

        public UserClassificationMap GetUserClassificationMap(int userClassificationMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

                UserClassificationMap userClassificationMapEntity = userClassificationMapRepository.Get(userClassificationMapId);
                if (userClassificationMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("UserClassificationMap with ID of {0} is not in database", userClassificationMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return userClassificationMapEntity;
            });
        }

        public UserClassificationMap[] GetAllUserClassificationMaps(string loginID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IUserClassificationMapRepository userClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<IUserClassificationMapRepository>();

                var setup = GetSetup();

                IEnumerable<UserClassificationMap> userClassificationMap = userClassificationMapRepository.GetUserClassificationMaps(loginID).ToArray();

                return userClassificationMap.ToArray();
            });
        }


        #endregion

        #region TeamDefinition operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamDefinition UpdateTeamDefinition(TeamDefinition teamDefinition)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                TeamDefinition updatedEntity = null;

                if (teamDefinition.TeamDefinitionId == 0)
                {
                    if (GetSetup().Period == null)
                    {
                        teamDefinition.Year = GetSetup().Year;
                        updatedEntity = teamDefinitionRepository.Add(teamDefinition);
                    }
                    else
                    {
                        teamDefinition.Year = GetSetup().Year;
                        teamDefinition.Period = GetSetup().Period;
                        updatedEntity = teamDefinitionRepository.Add(teamDefinition);
                    }

                }
                else
                    updatedEntity = teamDefinitionRepository.Update(teamDefinition);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamDefinition(int teamDefinitionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                teamDefinitionRepository.Remove(teamDefinitionId);
            });
        }

        public TeamDefinition GetTeamDefinition(int teamDefinitionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                var setup = GetSetup();

                if (setup.Period == null)
                {
                    if (GetSetup().SwithMode == "Team Classification")
                    {

                        var query = (from b in teamClassificationRepository.Get().Take(1) select new { TeamDefinitionId = setup.LevelId.Value, Code = setup.LevelId.ToString(), Name = "Team", Position = 2, Year = setup.Year, CanClassified = false, CanUseStaffId = false, Period = b.Period, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion })
                             .Concat(from a in teamDefinitionRepository.Get() select new { TeamDefinitionId = a.TeamDefinitionId, Code = a.Code, Name = a.Name, Position = a.Position, Year = a.Year, CanClassified = a.CanClassified, CanUseStaffId = a.CanUseStaffId, Period = a.Period, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion }).Where(u => u.TeamDefinitionId == teamDefinitionId && u.Year == setup.Year).FirstOrDefault();

                        TeamDefinition teamDefinitionEntity = new TeamDefinition();

                        teamDefinitionEntity.TeamDefinitionId = query.TeamDefinitionId;
                        teamDefinitionEntity.Code = query.Code;
                        teamDefinitionEntity.Name = query.Name;
                        teamDefinitionEntity.Position = query.Position;
                        teamDefinitionEntity.Year = query.Year;
                        teamDefinitionEntity.CanClassified = query.CanClassified;
                        teamDefinitionEntity.CanUseStaffId = query.CanUseStaffId;
                        teamDefinitionEntity.Period = query.Period;
                        teamDefinitionEntity.CompanyCode = query.CompanyCode;
                        teamDefinitionEntity.Active = query.Active;
                        teamDefinitionEntity.Deleted = query.Deleted;
                        teamDefinitionEntity.CreatedBy = query.CreatedBy;
                        teamDefinitionEntity.CreatedOn = query.CreatedOn;
                        teamDefinitionEntity.UpdatedBy = query.UpdatedBy;
                        teamDefinitionEntity.UpdatedOn = query.UpdatedOn;
                        teamDefinitionEntity.RowVersion = query.RowVersion;

                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;

                    }
                    else
                    {
                        TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.TeamDefinitionId == teamDefinitionId && c.Year == setup.Year).FirstOrDefault();
                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;
                    }

                }
                else
                {
                    if (GetSetup().SwithMode == "Team Classification")
                    {

                        var query = (from b in teamClassificationRepository.Get().Take(1) select new { TeamDefinitionId = setup.LevelId.Value, Code = setup.LevelId.ToString(), Name = "Team", Position = 5, Year = setup.Year, CanClassified = false, CanUseStaffId = false, Period = setup.Period, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion })
                             .Concat(from a in teamDefinitionRepository.Get() select new { TeamDefinitionId = a.TeamDefinitionId, Code = a.Code, Name = a.Name, Position = a.Position, Year = a.Year, CanClassified = a.CanClassified, CanUseStaffId = a.CanUseStaffId, Period = a.Period, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion }).Where(u => u.TeamDefinitionId == teamDefinitionId && u.Year == setup.Year && u.Period == setup.Period).FirstOrDefault();

                        TeamDefinition teamDefinitionEntity = new TeamDefinition();

                        teamDefinitionEntity.TeamDefinitionId = query.TeamDefinitionId;
                        teamDefinitionEntity.Code = query.Code;
                        teamDefinitionEntity.Name = query.Name;
                        teamDefinitionEntity.Position = query.Position;
                        teamDefinitionEntity.Year = query.Year;
                        teamDefinitionEntity.CanClassified = query.CanClassified;
                        teamDefinitionEntity.CanUseStaffId = query.CanUseStaffId;
                        teamDefinitionEntity.Period = query.Period;
                        teamDefinitionEntity.CompanyCode = query.CompanyCode;
                        teamDefinitionEntity.Active = query.Active;
                        teamDefinitionEntity.Deleted = query.Deleted;
                        teamDefinitionEntity.CreatedBy = query.CreatedBy;
                        teamDefinitionEntity.CreatedOn = query.CreatedOn;
                        teamDefinitionEntity.UpdatedBy = query.UpdatedBy;
                        teamDefinitionEntity.UpdatedOn = query.UpdatedOn;
                        teamDefinitionEntity.RowVersion = query.RowVersion;

                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;

                    }
                    else
                    {
                        TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.TeamDefinitionId == teamDefinitionId && c.Year == setup.Year && c.Period == setup.Period).FirstOrDefault();
                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;
                    }

                }

                //ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                //TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get(teamDefinitionId);
                //if (teamDefinitionEntity == null)
                //{
                //    NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with ID of {0} is not in database", teamDefinitionId));
                //    throw new FaultException<NotFoundException>(ex, ex.Message);
                //}

                //return teamDefinitionEntity;
            });
        }

        public TeamDefinition GetTeamDefinitionByCode(string code)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                var setup = GetSetup();

                if (setup.Period == null)
                {
                    if (GetSetup().SwithMode == "Team Classification")
                    {

                        var query = (from b in teamClassificationRepository.Get().Take(1) select new { TeamDefinitionId = setup.LevelId.Value, Code = setup.LevelId.ToString(), Name = "Team", Position = 5, Year = setup.Year, CanClassified = false, CanUseStaffId = false, Period = b.Period, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion })
                             .Concat(from a in teamDefinitionRepository.Get() select new { TeamDefinitionId = a.TeamDefinitionId, Code = a.Code, Name = a.Name, Position = a.Position, Year = a.Year, CanClassified = a.CanClassified, CanUseStaffId = a.CanUseStaffId, Period = a.Period, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion }).Where(u => u.Code == code && u.Year == setup.Year).FirstOrDefault();

                        TeamDefinition teamDefinitionEntity = new TeamDefinition();

                        teamDefinitionEntity.TeamDefinitionId = query.TeamDefinitionId;
                        teamDefinitionEntity.Code = query.Code;
                        teamDefinitionEntity.Name = query.Name;
                        teamDefinitionEntity.Position = query.Position;
                        teamDefinitionEntity.Year = query.Year;
                        teamDefinitionEntity.CanClassified = query.CanClassified;
                        teamDefinitionEntity.CanUseStaffId = query.CanUseStaffId;
                        teamDefinitionEntity.Period = query.Period;
                        teamDefinitionEntity.CompanyCode = query.CompanyCode;
                        teamDefinitionEntity.Active = query.Active;
                        teamDefinitionEntity.Deleted = query.Deleted;
                        teamDefinitionEntity.CreatedBy = query.CreatedBy;
                        teamDefinitionEntity.CreatedOn = query.CreatedOn;
                        teamDefinitionEntity.UpdatedBy = query.UpdatedBy;
                        teamDefinitionEntity.UpdatedOn = query.UpdatedOn;
                        teamDefinitionEntity.RowVersion = query.RowVersion;

                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with Code of {0} is not in database", code));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;

                    }
                    else
                    {
                        TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.Code == code && c.Year == setup.Year).FirstOrDefault();
                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with Code of {0} is not in database", code));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;
                    }

                }
                else
                {
                    if (GetSetup().SwithMode == "Team Classification")
                    {

                        var query = (from b in teamClassificationRepository.Get().Take(1) select new { TeamDefinitionId = setup.LevelId.Value, Code = setup.LevelId.ToString(), Name = "Team", Position = 5, Year = setup.Year, CanClassified = false, CanUseStaffId = false, Period = setup.Period, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion })
                             .Concat(from a in teamDefinitionRepository.Get() select new { TeamDefinitionId = a.TeamDefinitionId, Code = a.Code, Name = a.Name, Position = a.Position, Year = a.Year, CanClassified = a.CanClassified, CanUseStaffId = a.CanUseStaffId, Period = a.Period, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion }).Where(u => u.Code == code && u.Year == setup.Year && u.Period == setup.Period).FirstOrDefault();

                        TeamDefinition teamDefinitionEntity = new TeamDefinition();

                        teamDefinitionEntity.TeamDefinitionId = query.TeamDefinitionId;
                        teamDefinitionEntity.Code = query.Code;
                        teamDefinitionEntity.Name = query.Name;
                        teamDefinitionEntity.Position = query.Position;
                        teamDefinitionEntity.Year = query.Year;
                        teamDefinitionEntity.CanClassified = query.CanClassified;
                        teamDefinitionEntity.CanUseStaffId = query.CanUseStaffId;
                        teamDefinitionEntity.Period = query.Period;
                        teamDefinitionEntity.CompanyCode = query.CompanyCode;
                        teamDefinitionEntity.Active = query.Active;
                        teamDefinitionEntity.Deleted = query.Deleted;
                        teamDefinitionEntity.CreatedBy = query.CreatedBy;
                        teamDefinitionEntity.CreatedOn = query.CreatedOn;
                        teamDefinitionEntity.UpdatedBy = query.UpdatedBy;
                        teamDefinitionEntity.UpdatedOn = query.UpdatedOn;
                        teamDefinitionEntity.RowVersion = query.RowVersion;

                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with Code of {0} is not in database", code));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;

                    }
                    else
                    {
                        TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.Code == code && c.Year == setup.Year && c.Period == setup.Period).FirstOrDefault();
                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("TeamDefinition with Code of {0} is not in database", code));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;
                    }

                }


                //ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                //var setUp = GetSetup();

                //if (setUp.Period == null)
                //{
                //    TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.Code == code && c.Year == setUp.Year).FirstOrDefault();

                //    return teamDefinitionEntity;
                //}
                //else
                //{
                //    TeamDefinition teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.Code == code && c.Year == setUp.Year && c.Period == setUp.Period).FirstOrDefault();

                //    return teamDefinitionEntity;
                //}


            });
        }

        public IEnumerable<TeamDefinition> GetAllTeamDefinitions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var setup = GetSetup();

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                if (setup.Period == null)
                {
                    if (GetSetup().SwithMode == "Team Classification")
                    {

                        var query = (from b in teamClassificationRepository.Get().Take(1) select new { TeamDefinitionId = setup.LevelId.Value, Code = setup.LevelId.ToString(), Name = "Team", Position = 2, Year = setup.Year, CanClassified = false, CanUseStaffId = false, Period = b.Period, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion })
                             .Concat(from a in teamDefinitionRepository.Get() select new { TeamDefinitionId = a.TeamDefinitionId, Code = a.Code, Name = a.Name, Position = a.Position, Year = a.Year, CanClassified = a.CanClassified, CanUseStaffId = a.CanUseStaffId, Period = a.Period, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion }).Where(u => u.Year == setup.Year);

                        var teamDefinitionEntity = from r in query
                                                   select new TeamDefinition()
                                                   {
                                                       TeamDefinitionId = r.TeamDefinitionId,
                                                       Code = r.Code,
                                                       Name = r.Name,
                                                       Position = r.Position,
                                                       Year = r.Year,
                                                       CanClassified = r.CanClassified,
                                                       CanUseStaffId = r.CanUseStaffId,
                                                       Period = r.Period,
                                                       CompanyCode = r.CompanyCode,
                                                       Active = r.Active,
                                                       Deleted = r.Deleted,
                                                       CreatedBy = r.CreatedBy,
                                                       CreatedOn = r.CreatedOn,
                                                       UpdatedBy = r.UpdatedBy,
                                                       UpdatedOn = r.UpdatedOn,
                                                       RowVersion = r.RowVersion,
                                                   };



                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;

                    }
                    else
                    {
                        IEnumerable<TeamDefinition> teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.Year == setup.Year).ToList();
                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;
                    }

                }
                else
                {
                    if (GetSetup().SwithMode == "Team Classification")
                    {


                        var query = (from b in teamClassificationRepository.Get().Take(1) select new { TeamDefinitionId = setup.LevelId.Value, Code = setup.LevelId.ToString(), Name = "Team", Position = 5, Year = setup.Year, CanClassified = false, CanUseStaffId = false, Period = b.Period, CompanyCode = b.CompanyCode, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion })
                           .Concat(from a in teamDefinitionRepository.Get() select new { TeamDefinitionId = a.TeamDefinitionId, Code = a.Code, Name = a.Name, Position = a.Position, Year = a.Year, CanClassified = a.CanClassified, CanUseStaffId = a.CanUseStaffId, Period = a.Period, CompanyCode = a.CompanyCode, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion }).Where(u => u.Year == setup.Year && u.Period == setup.Period);


                        var teamDefinitionEntity = from r in query
                                                   select new TeamDefinition()
                                                   {
                                                       TeamDefinitionId = r.TeamDefinitionId,
                                                       Code = r.Code,
                                                       Name = r.Name,
                                                       Position = r.Position,
                                                       Year = r.Year,
                                                       CanClassified = r.CanClassified,
                                                       CanUseStaffId = r.CanUseStaffId,
                                                       Period = r.Period,
                                                       CompanyCode = r.CompanyCode,
                                                       Active = r.Active,
                                                       Deleted = r.Deleted,
                                                       CreatedBy = r.CreatedBy,
                                                       CreatedOn = r.CreatedOn,
                                                       UpdatedBy = r.UpdatedBy,
                                                       UpdatedOn = r.UpdatedOn,
                                                       RowVersion = r.RowVersion,
                                                   };

                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;

                    }
                    else
                    {
                        //IEnumerable<TeamDefinition> teamDefinitionEntity = teamDefinitionRepository.Get().Where(c => c.Year == setup.Year && c.Period == setup.Period).ToList();
                        IEnumerable<TeamDefinition> teamDefinitionEntity = teamDefinitionRepository.Get().ToList();
                        if (teamDefinitionEntity == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }
                        return teamDefinitionEntity;
                    }

                }


                //ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();

                //var setup = GetSetup();

                //if (setup.Period == null)
                //{
                //    IEnumerable<TeamDefinition> teamDefinition = teamDefinitionRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                //    return teamDefinition.ToArray();
                //}
                //else
                //{
                //    IEnumerable<TeamDefinition> teamDefinition = teamDefinitionRepository.Get().Where(c => c.Year == setup.Year && c.Period == setup.Period).ToArray();

                //    return teamDefinition.ToArray();
                //}

            });
        }


        #endregion

        #region TeamClassificationType operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamClassificationType UpdateTeamClassificationType(TeamClassificationType teamClassificationType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

                TeamClassificationType updatedEntity = null;

                if (teamClassificationType.TeamClassificationTypeId == 0)
                {
                    teamClassificationType.Year = GetSetup().Year;
                    updatedEntity = teamClassificationTypeRepository.Add(teamClassificationType);
                }
                else
                    updatedEntity = teamClassificationTypeRepository.Update(teamClassificationType);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamClassificationType(int teamClassificationTypeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

                teamClassificationTypeRepository.Remove(teamClassificationTypeId);
            });
        }

        public TeamClassificationType GetTeamClassificationType(int teamClassificationTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

                TeamClassificationType teamClassificationTypeEntity = teamClassificationTypeRepository.Get(teamClassificationTypeId);
                if (teamClassificationTypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TeamClassificationType with ID of {0} is not in database", teamClassificationTypeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return teamClassificationTypeEntity;
            });
        }

        public TeamClassificationType[] GetAllTeamClassificationTypes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();

                var setup = GetSetup();

                IEnumerable<TeamClassificationType> teamClassificationType = teamClassificationTypeRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                return teamClassificationType.ToArray();
            });
        }

        #endregion

        #region TeamClassification operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamClassification UpdateTeamClassification(TeamClassification teamClassification)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                TeamClassification updatedEntity = null;

                if (teamClassification.TeamClassificationId == 0)
                {
                    if (GetSetup().Period == null)
                    {

                        teamClassification.Year = GetSetup().Year;
                        updatedEntity = teamClassificationRepository.Add(teamClassification);

                    }
                    else
                    {

                        teamClassification.Year = GetSetup().Year;
                        teamClassification.Period = GetSetup().Period;
                        updatedEntity = teamClassificationRepository.Add(teamClassification);
                    }

                }
                else
                    updatedEntity = teamClassificationRepository.Update(teamClassification);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamClassification(int teamClassificationId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                teamClassificationRepository.Remove(teamClassificationId);
            });
        }

        public TeamClassification GetTeamClassification(int teamClassificationId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                TeamClassification teamClassificationEntity = teamClassificationRepository.Get(teamClassificationId);
                if (teamClassificationEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TeamClassification with ID of {0} is not in database", teamClassificationId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return teamClassificationEntity;
            });
        }

        public TeamClassification[] GetAllTeamClassifications()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                var setup = GetSetup();

                if (setup.Period == null)
                {
                    IEnumerable<TeamClassification> teamClassification = teamClassificationRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                    return teamClassification.ToArray();
                }
                else
                {
                    IEnumerable<TeamClassification> teamClassification = teamClassificationRepository.Get().Where(c => c.Year == setup.Year && c.Period == setup.Period).ToArray();

                    return teamClassification.ToArray();
                }


            });
        }

        public TeamClassification[] GetTeamClassifications(string typeCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationTypeRepository teamClassificationTypeRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationTypeRepository>();
                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                var setup = GetSetup();

                var classificationType = teamClassificationTypeRepository.Get().Where(c => c.Code == typeCode).FirstOrDefault();

                IEnumerable<TeamClassification> teamClassification = null;

                if (setup.Period == null)
                {
                    if (classificationType != null)
                        teamClassification = teamClassificationRepository.Get().Where(c => c.Year == setup.Year && c.ClassificationTypeCode == typeCode && c.Level == classificationType.MaximumLevel).ToArray();

                    return teamClassification.ToArray();
                }
                else
                {
                    if (classificationType != null)
                        teamClassification = teamClassificationRepository.Get().Where(c => c.Year == setup.Year && c.ClassificationTypeCode == typeCode && c.Level == classificationType.MaximumLevel && c.Period == setup.Period).ToArray();

                    return teamClassification.ToArray();
                }

            });
        }

        #endregion

        #region Team operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Team UpdateTeam(Team team)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();
                IDatabaseRepository databaseRepository = _DataRepositoryFactory.GetDataRepository<IDatabaseRepository>();
                var companydb = databaseRepository.Get().Where(c => c.CompanyCode == DataConnector.CompanyCode).FirstOrDefault();

                Team updatedEntity = null;

                if (team.TeamId == 0)
                {

                    if (GetSetup().Period == null)
                    {
                        team.Year = GetSetup().Year;
                        team.CompanyCode = companydb.CompanyCode;
                        updatedEntity = teamRepository.Add(team);
                    }
                    else
                    {
                        team.Year = GetSetup().Year;
                        team.Period = GetSetup().Period;
                        team.CompanyCode = companydb.CompanyCode;
                        updatedEntity = teamRepository.Add(team);
                    }

                }
                else
                    team.CompanyCode = companydb.CompanyCode;
                updatedEntity = teamRepository.Update(team);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeam(int teamId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                teamRepository.Remove(teamId);
            });
        }

        public Team GetTeam(int teamId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                Team teamEntity = teamRepository.Get(teamId);
                if (teamEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Team with ID of {0} is not in database", teamId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return teamEntity;
            });
        }

        public Team[] GetParentTeams(string definitionCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                if (setUp.Period == null)
                {
                    var teamDefinition = teamDefinitionRepository.Get().Where(c => c.Code == definitionCode && c.Year == setUp.Year).FirstOrDefault();
                    var parentDefinition = teamDefinitionRepository.Get().Where(c => c.Position == (teamDefinition.Position + 1)).FirstOrDefault();

                    Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == parentDefinition.Code && c.Year == setUp.Year).OrderBy(c => c.Name).ToArray();

                    return teams;
                }
                else
                {
                    var teamDefinition = teamDefinitionRepository.Get().Where(c => c.Code == definitionCode && c.Year == setUp.Year && c.Period == setUp.Period).FirstOrDefault();
                    var parentDefinition = teamDefinitionRepository.Get().Where(c => c.Position == (teamDefinition.Position + 1)).FirstOrDefault();

                    Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == parentDefinition.Code && c.Year == setUp.Year && c.Period == setUp.Period).OrderBy(c => c.Name).ToArray();

                    return teams;
                }


            });
        }

        public Team[] GetTeamByLevel(int level)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                var teamDefinition = teamDefinitionRepository.Get().Where(c => c.Position == level).FirstOrDefault();

                Team[] teams = null;

                if (setUp.Period == null)
                {
                    if (teamDefinition != null)
                        teams = teamRepository.Get().Where(c => c.DefinitionCode == teamDefinition.Code && c.Year == setUp.Year).OrderBy(c => c.Name).ToArray();

                    return teams;
                }
                else
                {
                    if (teamDefinition != null)
                        teams = teamRepository.Get().Where(c => c.DefinitionCode == teamDefinition.Code && c.Year == setUp.Year && c.Period == setUp.Period).OrderBy(c => c.Name).ToArray();

                    return teams;
                }


            });
        }

        public IEnumerable<Team> GetTeamByDefinition(string definitionCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                var setup = GetSetup();

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();
                ITeamClassificationRepository teamClassificationRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationRepository>();

                if (setup.Period == null)
                {

                    if (GetSetup().SwithMode == "Team Classification")
                    {
                        var query = (from b in teamClassificationRepository.Get() select new { TeamId = b.TeamClassificationId, Code = b.Code, Name = b.Name, ParentCode = b.ParentCode, DefinitionCode = b.Level.ToString(), StaffId = "", CompanyCode = b.CompanyCode, Year = b.Year, Period = b.Period, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR })
                            .Concat(from a in teamRepository.Get() select new { TeamId = a.TeamId, Code = a.Code, Name = a.Name, ParentCode = a.ParentCode, DefinitionCode = a.DefinitionCode, StaffId = a.StaffId, CompanyCode = a.CompanyCode, Year = a.Year, Period = a.Period, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR }).Where(u => u.Year == setup.Year && u.DefinitionCode == definitionCode);

                        var teams = from r in query
                                    select new Team()
                                    {
                                        TeamId = r.TeamId,
                                        Code = r.Code,
                                        Name = r.Name,
                                        ParentCode = r.ParentCode,
                                        DefinitionCode = r.DefinitionCode,
                                        StaffId = r.StaffId,
                                        ModuleOwnerType = ModuleOwnerType.MPR,
                                        Year = r.Year,
                                        Period = r.Period,
                                        CompanyCode = r.CompanyCode,
                                        Active = r.Active,
                                        Deleted = r.Deleted,
                                        CreatedBy = r.CreatedBy,
                                        CreatedOn = r.CreatedOn,
                                        UpdatedBy = r.UpdatedBy,
                                        UpdatedOn = r.UpdatedOn,
                                        RowVersion = r.RowVersion,
                                    };



                        if (teams == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }

                        return teams;

                    }
                    else
                    {
                        Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == definitionCode && c.Year == setup.Year).OrderBy(c => c.Name).ToArray();

                        return teams;
                    }

                }
                else
                {

                    if (GetSetup().SwithMode == "Team Classification")
                    {
                        var query = (from b in teamClassificationRepository.Get() select new { TeamId = b.TeamClassificationId, Code = b.Code, Name = b.Name, ParentCode = b.ParentCode, DefinitionCode = b.Level.ToString(), StaffId = "", CompanyCode = b.CompanyCode, Year = b.Year, Period = b.Period, Active = b.Active, Deleted = b.Deleted, CreatedBy = b.CreatedBy, CreatedOn = b.CreatedOn, UpdatedBy = b.UpdatedBy, UpdatedOn = b.UpdatedOn, RowVersion = b.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR })
                           .Concat(from a in teamRepository.Get() select new { TeamId = a.TeamId, Code = a.Code, Name = a.Name, ParentCode = a.ParentCode, DefinitionCode = a.DefinitionCode, StaffId = a.StaffId, CompanyCode = a.CompanyCode, Year = a.Year, Period = a.Period, Active = a.Active, Deleted = a.Deleted, CreatedBy = a.CreatedBy, CreatedOn = a.CreatedOn, UpdatedBy = a.UpdatedBy, UpdatedOn = a.UpdatedOn, RowVersion = a.RowVersion, ModuleOwnerType = ModuleOwnerType.MPR }).Where(u => u.Year == setup.Year && u.DefinitionCode == definitionCode && u.Period == setup.Period);

                        var teams = from r in query
                                    select new Team()
                                    {
                                        TeamId = r.TeamId,
                                        Code = r.Code,
                                        Name = r.Name,
                                        ParentCode = r.ParentCode,
                                        DefinitionCode = r.DefinitionCode,
                                        StaffId = r.StaffId,
                                        ModuleOwnerType = ModuleOwnerType.MPR,
                                        Year = r.Year,
                                        Period = r.Period,
                                        CompanyCode = r.CompanyCode,
                                        Active = r.Active,
                                        Deleted = r.Deleted,
                                        CreatedBy = r.CreatedBy,
                                        CreatedOn = r.CreatedOn,
                                        UpdatedBy = r.UpdatedBy,
                                        UpdatedOn = r.UpdatedOn,
                                        RowVersion = r.RowVersion,
                                    };



                        if (teams == null)
                        {
                            NotFoundException ex = new NotFoundException(string.Format("Data not in database"));
                            throw new FaultException<NotFoundException>(ex, ex.Message);
                        }

                        return teams;

                    }
                    else
                    {
                        Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == definitionCode && c.Year == setup.Year && c.Period == setup.Period).OrderBy(c => c.Name).ToArray();

                        return teams;
                    }

                }

                ////  ITeamDefinitionRepository teamDefinitionRepository = _DataRepositoryFactory.GetDataRepository<ITeamDefinitionRepository>();
                //ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                //ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                //var setUp = setupRepository.Get().FirstOrDefault();

                //if (setUp.Period == null)
                //{
                //    Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == definitionCode && c.Year == setUp.Year).OrderBy(c => c.Name).ToArray();

                //    return teams;
                //}
                //else
                //{
                //    Team[] teams = teamRepository.Get().Where(c => c.DefinitionCode == definitionCode && c.Year == setUp.Year && c.Period == setUp.Period).OrderBy(c => c.Name).ToArray();

                //    return teams;
                //}


            });
        }

        public TeamData[] GetTeams()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                List<TeamData> team = new List<TeamData>();

                if (setUp.Period == null)
                {
                    IEnumerable<TeamInfo> teamInfos = teamRepository.GetTeams().OrderByDescending(c => c.Team.DefinitionCode).Where(c => c.Team.Year == setUp.Year).ToArray();

                    foreach (var teamInfo in teamInfos)
                    {
                        team.Add(
                            new TeamData
                            {
                                TeamId = teamInfo.Team.EntityId,
                                Code = teamInfo.Team.Code,
                                Name = teamInfo.Team.Name,
                                ModuleOwnerType = teamInfo.Team.ModuleOwnerType,
                                ModuleName = teamInfo.Team.ModuleOwnerType.ToString(),
                                //ParentId = teamInfo.Team.ParentId,
                                ParentCode = teamInfo.Parent != null ? teamInfo.Parent.Code : string.Empty,
                                ParentName = teamInfo.Parent != null ? teamInfo.Parent.Name : "",
                                DefinitionCode = teamInfo.Team.DefinitionCode,
                                CanClassified = true,
                                CanUseStaffId = true,
                                StaffId = teamInfo.Team.StaffId,
                                Position = 1
                            });
                    }

                    return team.Take(50).ToArray();
                }
                else
                {
                    IEnumerable<TeamInfo> teamInfos = teamRepository.GetTeams().OrderByDescending(c => c.Team.DefinitionCode).Where(c => c.Team.Year == setUp.Year && c.Team.Period == setUp.Period).ToArray();

                    foreach (var teamInfo in teamInfos)
                    {
                        team.Add(
                            new TeamData
                            {
                                TeamId = teamInfo.Team.EntityId,
                                Code = teamInfo.Team.Code,
                                Name = teamInfo.Team.Name,
                                ModuleOwnerType = teamInfo.Team.ModuleOwnerType,
                                ModuleName = teamInfo.Team.ModuleOwnerType.ToString(),
                                //ParentId = teamInfo.Team.ParentId,
                                ParentCode = teamInfo.Parent != null ? teamInfo.Parent.Code : string.Empty,
                                ParentName = teamInfo.Parent != null ? teamInfo.Parent.Name : "",
                                DefinitionCode = teamInfo.Team.DefinitionCode,
                                Period = teamInfo.Team.Period,
                                CanClassified = true,
                                CanUseStaffId = true,
                                StaffId = teamInfo.Team.StaffId,
                                Position = 1
                            });
                    }

                    return team.Take(50).ToArray();
                }


            });
        }



        public TeamData[] GetTeamsBySearch(string SearchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamRepository teamRepository = _DataRepositoryFactory.GetDataRepository<ITeamRepository>();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                List<TeamData> team = new List<TeamData>();

                if (setUp.Period == null)
                {
                    IEnumerable<TeamInfo> teamInfos = teamRepository.GetTeamsBySearch(SearchValue).OrderByDescending(c => c.Team.DefinitionCode).Where(c => c.Team.Year == setUp.Year).ToArray();

                    foreach (var teamInfo in teamInfos)
                    {
                        team.Add(
                            new TeamData
                            {
                                TeamId = teamInfo.Team.EntityId,
                                Code = teamInfo.Team.Code,
                                Name = teamInfo.Team.Name,
                                ModuleOwnerType = teamInfo.Team.ModuleOwnerType,
                                ModuleName = teamInfo.Team.ModuleOwnerType.ToString(),
                                //ParentId = teamInfo.Team.ParentId,
                                ParentCode = teamInfo.Parent != null ? teamInfo.Parent.Code : string.Empty,
                                ParentName = teamInfo.Parent != null ? teamInfo.Parent.Name : "",
                                DefinitionCode = teamInfo.Team.DefinitionCode,
                                CanClassified = true,
                                CanUseStaffId = true,
                                StaffId = teamInfo.Team.StaffId,
                                Position = 1
                            });
                    }

                    return team.ToArray();
                }
                else
                {
                    IEnumerable<TeamInfo> teamInfos = teamRepository.GetTeamsBySearch(SearchValue).OrderByDescending(c => c.Team.DefinitionCode).Where(c => c.Team.Year == setUp.Year && c.Team.Period == setUp.Period).ToArray();

                    foreach (var teamInfo in teamInfos)
                    {
                        team.Add(
                            new TeamData
                            {
                                TeamId = teamInfo.Team.EntityId,
                                Code = teamInfo.Team.Code,
                                Name = teamInfo.Team.Name,
                                ModuleOwnerType = teamInfo.Team.ModuleOwnerType,
                                ModuleName = teamInfo.Team.ModuleOwnerType.ToString(),
                                //ParentId = teamInfo.Team.ParentId,
                                ParentCode = teamInfo.Parent != null ? teamInfo.Parent.Code : string.Empty,
                                ParentName = teamInfo.Parent != null ? teamInfo.Parent.Name : "",
                                DefinitionCode = teamInfo.Team.DefinitionCode,
                                Period = teamInfo.Team.Period,
                                CanClassified = true,
                                CanUseStaffId = true,
                                StaffId = teamInfo.Team.StaffId,
                                Position = 1
                            });
                    }

                    return team.ToArray();
                }


            });
        }

        #endregion

        #region TeamClassificationMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamClassificationMap UpdateTeamClassificationMap(TeamClassificationMap teamClassificationMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

                TeamClassificationMap updatedEntity = null;

                if (teamClassificationMap.TeamClassificationMapId == 0)
                {
                    teamClassificationMap.Year = GetSetup().Year;
                    updatedEntity = teamClassificationMapRepository.Add(teamClassificationMap);
                }
                else
                    updatedEntity = teamClassificationMapRepository.Update(teamClassificationMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamClassificationMap(int teamClassificationMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

                teamClassificationMapRepository.Remove(teamClassificationMapId);
            });
        }

        public TeamClassificationMap GetTeamClassificationMap(int teamClassificationMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

                TeamClassificationMap teamClassificationMapEntity = teamClassificationMapRepository.Get(teamClassificationMapId);
                if (teamClassificationMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TeamClassificationMap with ID of {0} is not in database", teamClassificationMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return teamClassificationMapEntity;
            });
        }

        public TeamClassificationMap[] GetAllTeamClassificationMaps(string misCode, string definitionCode)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamClassificationMapRepository teamClassificationMapRepository = _DataRepositoryFactory.GetDataRepository<ITeamClassificationMapRepository>();

                var setup = GetSetup();

                IEnumerable<TeamClassificationMap> teamClassificationMap = teamClassificationMapRepository.Get().Where(c => c.Year == setup.Year && c.DefinitionCode == definitionCode && c.MisCode == misCode).ToArray();

                return teamClassificationMap.ToArray();
            });
        }

        #endregion

        #region AccountOfficerDetail operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public AccountOfficerDetail UpdateAccountOfficerDetail(AccountOfficerDetail accountOfficerDetail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

                AccountOfficerDetail updatedEntity = null;

                if (accountOfficerDetail.AccountOfficerDetailId == 0)
                {
                    accountOfficerDetail.Year = GetSetup().Year;
                    updatedEntity = accountOfficerDetailRepository.Add(accountOfficerDetail);
                }
                else
                    updatedEntity = accountOfficerDetailRepository.Update(accountOfficerDetail);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAccountOfficerDetail(int accountOfficerDetailId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

                accountOfficerDetailRepository.Remove(accountOfficerDetailId);
            });
        }

        public AccountOfficerDetail GetAccountOfficerDetail(int accountOfficerDetailId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

                AccountOfficerDetail accountOfficerDetailEntity = accountOfficerDetailRepository.Get(accountOfficerDetailId);
                if (accountOfficerDetailEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("AccountOfficerDetail with ID of {0} is not in database", accountOfficerDetailId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accountOfficerDetailEntity;
            });
        }

        public AccountOfficerDetail[] GetAllAccountOfficerDetails()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountOfficerDetailRepository accountOfficerDetailRepository = _DataRepositoryFactory.GetDataRepository<IAccountOfficerDetailRepository>();

                var setup = GetSetup();

                IEnumerable<AccountOfficerDetail> accountOfficerDetail = accountOfficerDetailRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                return accountOfficerDetail.ToArray();
            });
        }

        #endregion

        #region BranchDefaultMIS operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BranchDefaultMIS UpdateBranchDefaultMIS(BranchDefaultMIS branchDefaultMIS)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

                BranchDefaultMIS updatedEntity = null;

                if (branchDefaultMIS.BranchDefaultMISId == 0)
                {
                    branchDefaultMIS.Year = GetSetup().Year;
                    updatedEntity = branchDefaultMISRepository.Add(branchDefaultMIS);
                }
                else
                    updatedEntity = branchDefaultMISRepository.Update(branchDefaultMIS);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBranchDefaultMIS(int branchDefaultMISId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

                branchDefaultMISRepository.Remove(branchDefaultMISId);
            });
        }

        public BranchDefaultMIS GetBranchDefaultMIS(int branchDefaultMISId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

                BranchDefaultMIS branchDefaultMISEntity = branchDefaultMISRepository.Get(branchDefaultMISId);
                if (branchDefaultMISEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BranchDefaultMIS with ID of {0} is not in database", branchDefaultMISId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return branchDefaultMISEntity;
            });
        }

        public BranchDefaultMIS[] GetAllBranchDefaultMISs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBranchDefaultMISRepository branchDefaultMISRepository = _DataRepositoryFactory.GetDataRepository<IBranchDefaultMISRepository>();

                var setup = GetSetup();

                IEnumerable<BranchDefaultMIS> branchDefaultMIS = branchDefaultMISRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                return branchDefaultMIS.ToArray();
            });
        }

        #endregion

        #region ManagementTree operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public ManagementTree UpdateManagementTree(ManagementTree managementTree)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

                ManagementTree updatedEntity = null;

                if (managementTree.ManagementTreeId == 0)
                {
                    managementTree.Year = GetSetup().Year;
                    updatedEntity = managementTreeRepository.Add(managementTree);
                }
                else
                    updatedEntity = managementTreeRepository.Update(managementTree);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteManagementTree(int managementTreeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

                managementTreeRepository.Remove(managementTreeId);
            });
        }

        public ManagementTree GetManagementTree(int managementTreeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

                ManagementTree managementTreeEntity = managementTreeRepository.Get(managementTreeId);
                if (managementTreeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ManagementTree with ID of {0} is not in database", managementTreeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return managementTreeEntity;
            });
        }

        public ManagementTreeData[] GetAllManagementTrees()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IManagementTreeRepository managementTreeRepository = _DataRepositoryFactory.GetDataRepository<IManagementTreeRepository>();

                var setup = GetSetup();
                string curYr = setup.Year;
                //IEnumerable<ManagementTreeData> managementTree = managementTreeRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                List<ManagementTreeData> managementTree = new List<ManagementTreeData>();
                IEnumerable<ManagementTreeInfo> managementTreeInfos = managementTreeRepository.GetManagementTrees(curYr).ToArray();

                foreach (var managementTreeInfo in managementTreeInfos)
                {
                    managementTree.Add(
                        new ManagementTreeData
                        {
                            ManagementTreeId = managementTreeInfo.ManagementTree.EntityId,
                            AccountNo = managementTreeInfo.ManagementTree.AccountNo,
                            TeamDefinitionCode = managementTreeInfo.ManagementTree.TeamDefinitionCode,
                            TeamDefinitionName = managementTreeInfo.TeamDefinition.Name,
                            TeamCode = managementTreeInfo.ManagementTree.TeamCode,
                            TeamName = managementTreeInfo.Team.Name,
                            AccountOfficerDefinitionCode = managementTreeInfo.ManagementTree != null ? managementTreeInfo.ManagementTree.AccountOfficerDefinitionCode : string.Empty,
                            AccountOfficerDefinitionName = managementTreeInfo.AccountOfficerDefinition != null ? managementTreeInfo.AccountOfficerDefinition.Name : string.Empty,
                            AccountOfficerCode = managementTreeInfo.ManagementTree != null ? managementTreeInfo.ManagementTree.AccountOfficerCode : string.Empty,
                            AccountOfficerName = managementTreeInfo.AccountOfficer != null ? managementTreeInfo.AccountOfficer.Name : string.Empty,
                            Rate = managementTreeInfo.ManagementTree.Rate,
                            Year = managementTreeInfo.ManagementTree.Year,
                            Active = managementTreeInfo.ManagementTree.Active
                        });
                }


                return managementTree.ToArray();
            });
        }

        #endregion

        #region AccountMIS operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public AccountMIS UpdateAccountMIS(AccountMIS accountMIS)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

                AccountMIS updatedEntity = null;

                if (accountMIS.AccountMISId == 0)
                {
                    accountMIS.Year = GetSetup().Year;
                    updatedEntity = accountMISRepository.Add(accountMIS);
                }
                else
                    updatedEntity = accountMISRepository.Update(accountMIS);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAccountMIS(int accountMISId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

                accountMISRepository.Remove(accountMISId);
            });
        }

        public AccountMIS GetAccountMIS(int accountMISId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

                AccountMIS accountMISEntity = accountMISRepository.Get(accountMISId);
                if (accountMISEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("AccountMIS with ID of {0} is not in database", accountMISId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accountMISEntity;
            });
        }

        public AccountMISData[] GetAllAccountMISs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountMISRepository accountMISRepository = _DataRepositoryFactory.GetDataRepository<IAccountMISRepository>();

                //var setup = GetSetup();

                //IEnumerable<AccountMIS> accountMIS = accountMISRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                //return accountMIS.ToArray();

                ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                var setUp = setupRepository.Get().FirstOrDefault();

                List<AccountMISData> accountMIS = new List<AccountMISData>();
                IEnumerable<AccountMISInfo> accountMISInfos = accountMISRepository.GetAccountMISs().Where(c => c.AccountMIS.Year == setUp.Year).ToArray();

                foreach (var accountMISInfo in accountMISInfos)
                {
                    accountMIS.Add(
                        new AccountMISData
                        {
                            AccountMISId = accountMISInfo.AccountMIS.EntityId,
                            AccountNo = accountMISInfo.AccountMIS.AccountNo,
                            TeamDefinitionCode = accountMISInfo.AccountMIS.TeamDefinitionCode,
                            TeamDefinitionName = accountMISInfo.TeamDefinition.Name,
                            TeamCode = accountMISInfo.AccountMIS.TeamCode,
                            TeamName = accountMISInfo.Team.Name,
                            AccountOfficerDefinitionCode = accountMISInfo.AccountMIS != null ? accountMISInfo.AccountMIS.AccountOfficerDefinitionCode : string.Empty,
                            AccountOfficerDefinitionName = accountMISInfo.AccountOfficerDefinition != null ? accountMISInfo.AccountOfficerDefinition.Name : string.Empty,
                            AccountOfficerCode = accountMISInfo.AccountMIS != null ? accountMISInfo.AccountMIS.AccountOfficerCode : string.Empty,
                            AccountOfficerName = accountMISInfo.AccountOfficer != null ? accountMISInfo.AccountOfficer.Name : string.Empty,
                            AccountName = accountMISInfo.CustAccount != null ? accountMISInfo.CustAccount.AccountName : string.Empty,
                            //Rate = accountMISInfo.AccountMIS.Rate,
                            Year = accountMISInfo.AccountMIS.Year,
                            Active = accountMISInfo.AccountMIS.Active
                        });
                }


                return accountMIS.ToArray();
            });
        }

        public void DeleteSelectedIds(string selectedIds)
        {
            var connectionString = GetDataConnection();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("MultipleDeletion", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@IdLists",
                    Value = selectedIds,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@pageOwner",
                    Value = "AcctMIS"
                });
                cmd.CommandTimeout = 0;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

        }

        #endregion

        #region MISReplacement operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MISReplacement UpdateMISReplacement(MISReplacement misReplacement)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

                MISReplacement updatedEntity = null;

                if (misReplacement.MISReplacementId == 0)
                {
                    misReplacement.Year = GetSetup().Year;
                    updatedEntity = misReplacementRepository.Add(misReplacement);
                }
                else
                    updatedEntity = misReplacementRepository.Update(misReplacement);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMISReplacement(int misReplacementId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

                misReplacementRepository.Remove(misReplacementId);
            });
        }

        public MISReplacement GetMISReplacement(int misReplacementId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

                MISReplacement misReplacementEntity = misReplacementRepository.Get(misReplacementId);
                if (misReplacementEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MISReplacement with ID of {0} is not in database", misReplacementId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return misReplacementEntity;
            });
        }

        public MISReplacement[] GetAllMISReplacements()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISReplacementRepository misReplacementRepository = _DataRepositoryFactory.GetDataRepository<IMISReplacementRepository>();

                var setup = GetSetup();

                IEnumerable<MISReplacement> misReplacement = misReplacementRepository.Get().Where(c => c.Year == setup.Year).ToArray();

                return misReplacement.ToArray();
            });
        }

        #endregion

        #region SetUp operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SetUp UpdateMPRSetup(SetUp mprMPRSetup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

                SetUp updatedEntity = null;

                if (mprMPRSetup.SetupId == 0)
                    updatedEntity = setUpRepository.Add(mprMPRSetup);
                else
                    updatedEntity = setUpRepository.Update(mprMPRSetup);

                return updatedEntity;
            });
        }

        public SetUp GetFirstMPRSetup()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

                SetUp setUpEntity = setUpRepository.Get().FirstOrDefault();

                //}

                return setUpEntity;
            });

        }

        public MPRSetupData[] GetFirstMPRSetups()
        {

            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRSetUpRepository setUpRepository = _DataRepositoryFactory.GetDataRepository<IMPRSetUpRepository>();


                List<MPRSetupData> mprSetUp = new List<MPRSetupData>();
                IEnumerable<MPRSetUpInfo> mprSetupInfos = setUpRepository.GetFirstMPRSetUps().ToArray();

                foreach (var mprSetupInfo in mprSetupInfos)
                {
                    mprSetUp.Add(
                        new MPRSetupData
                        {
                            SetupId = mprSetupInfo.SetUp.EntityId,
                            ExcoDefinitionCode = mprSetupInfo.SetUp.ExcoDefinitionCode,
                            ExcoTeamCode = mprSetupInfo.SetUp.ExcoTeamCode,
                            AccountLenght = mprSetupInfo.SetUp.AccountLenght,
                            Year = mprSetupInfo.SetUp.Year,
                            PoolOption = mprSetupInfo.SetUp.PoolOption,
                            PoolName = mprSetupInfo.SetUp.PoolOption.ToString(),
                            Period = mprSetupInfo.SetUp.Period,
                            SwithMode = mprSetupInfo.SetUp.SwithMode,
                            LevelId = mprSetupInfo.SetUp.LevelId,
                            Active = mprSetupInfo.SetUp.Active
                        });
                }

                return mprSetUp.ToArray();
            });
        }

        #endregion

        #region TransferPrice operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public TransferPrice UpdateTransferPrice(TransferPrice transferPrice)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

                TransferPrice updatedEntity = null;

                if (transferPrice.TransferPriceId == 0)
                {
                    transferPrice.Year = GetSetup().Year;
                    updatedEntity = transferPriceRepository.Add(transferPrice);
                }
                else
                    updatedEntity = transferPriceRepository.Update(transferPrice);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTransferPrice(int transferPriceId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

                transferPriceRepository.Remove(transferPriceId);
            });
        }

        public TransferPrice GetTransferPrice(int transferPriceId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

                TransferPrice transferPriceEntity = transferPriceRepository.Get(transferPriceId);
                if (transferPriceEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("TransferPrice with ID of {0} is not in database", transferPriceId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return transferPriceEntity;
            });
        }

        public TransferPriceData[] GetAllTransferPrices()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITransferPriceRepository transferPriceRepository = _DataRepositoryFactory.GetDataRepository<ITransferPriceRepository>();

                //var setUp = GetSetup();

                //IEnumerable<TransferPrice> transferPrice = transferPriceRepository.Get().Where (c=>c.Year == setUp.Year).ToArray();

                List<TransferPriceData> transferPrice = new List<TransferPriceData>();
                IEnumerable<TransferPriceInfo> transferPriceInfos = transferPriceRepository.GetTransferPrices().ToArray();

                foreach (var transferPriceInfo in transferPriceInfos)
                {
                    transferPrice.Add(
                        new TransferPriceData
                        {
                            TransferPriceId = transferPriceInfo.TransferPrice.EntityId,
                            ProductCode = transferPriceInfo.TransferPrice.ProductCode,
                            ProductName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.Product.Name : string.Empty,
                            CaptionCode = transferPriceInfo.TransferPrice.CaptionCode,
                            CaptionName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.BSCaption.CaptionName : string.Empty,
                            Rate = transferPriceInfo.TransferPrice.Rate,
                            DefinitionCode = transferPriceInfo.TransferPrice.DefinitionCode,
                            DefinitionName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.TeamDefinition.Name : string.Empty,
                            MisCode = transferPriceInfo.TransferPrice.MisCode,
                            MisName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.Team.Name : string.Empty,
                            Year = transferPriceInfo.TransferPrice.Year,
                            Period = transferPriceInfo.TransferPrice.Period,
                            SolutionId = transferPriceInfo.TransferPrice.SolutionId,
                            SolutionName = string.Empty,
                            CompanyCode = transferPriceInfo.TransferPrice.CompanyCode,
                            Active = transferPriceInfo.TransferPrice.Active
                        });
                }


                return transferPrice.ToArray();
            });
        }
        #endregion

        #region AccountTransferPrice operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public AccountTransferPrice UpdateAccountTransferPrice(AccountTransferPrice accountTransferPrice)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

                AccountTransferPrice updatedEntity = null;

                if (accountTransferPrice.AccountTransferPriceId == 0)
                {
                    accountTransferPrice.Year = GetSetup().Year;
                    updatedEntity = accountTransferPriceRepository.Add(accountTransferPrice);
                }
                else
                    updatedEntity = accountTransferPriceRepository.Update(accountTransferPrice);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAccountTransferPrice(int accountTransferPriceId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

                accountTransferPriceRepository.Remove(accountTransferPriceId);
            });
        }

        public AccountTransferPrice GetAccountTransferPrice(int accountTransferPriceId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

                AccountTransferPrice accountTransferPriceEntity = accountTransferPriceRepository.Get(accountTransferPriceId);
                if (accountTransferPriceEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("AccountTransferPrice with ID of {0} is not in database", accountTransferPriceId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return accountTransferPriceEntity;
            });
        }

        public AccountTransferPriceData[] GetAllAccountTransferPrices()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccountTransferPriceRepository accountTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IAccountTransferPriceRepository>();

                // ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                //var setUp = setupRepository.Get().FirstOrDefault();

                List<AccountTransferPriceData> AccountTransferPrice = new List<AccountTransferPriceData>();
                IEnumerable<AccountTransferPriceInfo> accountTransferPriceInfos = accountTransferPriceRepository.GetAccountTransferPrices().ToArray();

                foreach (var accountTransferPriceInfo in accountTransferPriceInfos)
                {
                    AccountTransferPrice.Add(
                        new AccountTransferPriceData
                        {
                            AccountTransferPriceId = accountTransferPriceInfo.AccountTransferPrice.EntityId,
                            AccountNo = accountTransferPriceInfo.AccountTransferPrice.AccountNo,
                            Category = accountTransferPriceInfo.AccountTransferPrice.Category,
                            CategoryName = accountTransferPriceInfo.AccountTransferPrice.Category.ToString(),
                            Rate = accountTransferPriceInfo.AccountTransferPrice.Rate,
                            Year = accountTransferPriceInfo.AccountTransferPrice.Year,
                            Period = accountTransferPriceInfo.AccountTransferPrice.Period,
                            SolutionId = accountTransferPriceInfo.AccountTransferPrice.SolutionId,
                            SolutionName = "",
                            AccountName = accountTransferPriceInfo.CustAccount != null ? accountTransferPriceInfo.CustAccount.AccountName : string.Empty,
                            Active = true,

                        });
                }

                return AccountTransferPrice.ToArray();
            });
        }

        #endregion

        #region GeneralTransferPrice operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public GeneralTransferPrice UpdateGeneralTransferPrice(GeneralTransferPrice generalTransferPrice)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

                GeneralTransferPrice updatedEntity = null;

                if (generalTransferPrice.GeneralTransferPriceId == 0)
                {
                    generalTransferPrice.Year = GetSetup().Year;
                    updatedEntity = generalTransferPriceRepository.Add(generalTransferPrice);
                }
                else
                    updatedEntity = generalTransferPriceRepository.Update(generalTransferPrice);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteGeneralTransferPrice(int generalTransferPriceId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

                generalTransferPriceRepository.Remove(generalTransferPriceId);
            });
        }

        public GeneralTransferPrice GetGeneralTransferPrice(int generalTransferPriceId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

                GeneralTransferPrice generalTransferPriceEntity = generalTransferPriceRepository.Get(generalTransferPriceId);
                if (generalTransferPriceEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("General Transfer Price with ID of {0} is not in database", generalTransferPriceId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return generalTransferPriceEntity;
            });
        }

        public GeneralTransferPriceData[] GetAllGeneralTransferPrices()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IGeneralTransferPriceRepository generalTransferPriceRepository = _DataRepositoryFactory.GetDataRepository<IGeneralTransferPriceRepository>();

                // ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();
                //var setUp = setupRepository.Get().FirstOrDefault();

                List<GeneralTransferPriceData> GeneralTransferPrice = new List<GeneralTransferPriceData>();
                IEnumerable<GeneralTransferPrice> generalTransferPriceInfos = generalTransferPriceRepository.Get().ToArray();

                foreach (var generalTransferPriceInfo in generalTransferPriceInfos)
                {
                    GeneralTransferPrice.Add(
                        new GeneralTransferPriceData
                        {
                            GeneralTransferPriceId = generalTransferPriceInfo.EntityId,
                            Category = generalTransferPriceInfo.Category,
                            CategoryName = generalTransferPriceInfo.Category.ToString(),
                            CurrencyType = generalTransferPriceInfo.CurrencyType,
                            CurrencyTypeName = generalTransferPriceInfo.CurrencyType.ToString(),
                            Rate = generalTransferPriceInfo.Rate,
                            Year = generalTransferPriceInfo.Year,
                            Period = generalTransferPriceInfo.Period,
                            DefinitionCode = generalTransferPriceInfo.DefinitionCode,
                            MISCode = generalTransferPriceInfo.MISCode,
                            CompanyCode = generalTransferPriceInfo.CompanyCode,
                            Active = true,

                        });
                }

                return GeneralTransferPrice.ToArray();
            });
        }

        public void DeleteGTPSelectedIds(string selectedIds)
        {
            var connectionString = GetDataConnection();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("MultipleDeletion", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@IdLists",
                    Value = selectedIds,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@pageOwner",
                    Value = "GTP"
                });
                cmd.CommandTimeout = 0;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

        }

        #endregion

        #region CustAccount operations

        [OperationBehavior(TransactionScopeRequired = true)]


        public CustAccount[] GetAllCustAccounts()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICustAccountRepository CustAccountRepository = _DataRepositoryFactory.GetDataRepository<ICustAccountRepository>();

                IEnumerable<CustAccount> CustAccounts = CustAccountRepository.Get().ToArray();

                return CustAccounts.ToArray();
            });
        }

        public CustAccount[] GetCustAccounts(string searchType, string searchValue, int number)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICustAccountRepository CustAccountRepository = _DataRepositoryFactory.GetDataRepository<ICustAccountRepository>();
                List<CustAccount> CustAccounts = CustAccountRepository.GetCustomerAccountBySearch(searchType, searchValue, number);


                return CustAccounts.ToArray();
            });
        }


        #endregion

        #region BSExemption operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public BSExemption UpdateBSExemption(BSExemption bsExemption)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();

                BSExemption updatedEntity = null;

                if (bsExemption.BSExemptionId == 0)
                {

                    updatedEntity = bsExemptionRepository.Add(bsExemption);
                }
                else
                    updatedEntity = bsExemptionRepository.Update(bsExemption);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteBSExemption(int bsExemptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();

                bsExemptionRepository.Remove(bsExemptionId);
            });
        }

        public BSExemption GetBSExemption(int bsExemptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();

                BSExemption bsExemptionEntity = bsExemptionRepository.Get(bsExemptionId);
                if (bsExemptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BSExemption with ID of {0} is not in database", bsExemptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return bsExemptionEntity;
            });
        }

        public BSExemption[] GetAllBSExemptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IBSExemptionRepository bsExemptionRepository = _DataRepositoryFactory.GetDataRepository<IBSExemptionRepository>();


                IEnumerable<BSExemption> bsExemption = bsExemptionRepository.Get().ToArray();

                return bsExemption.ToArray();
            });
        }

        #endregion

        #region MemoAccountMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MemoAccountMap UpdateMemoAccountMap(MemoAccountMap memoAccountMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

                MemoAccountMap updatedEntity = null;

                if (memoAccountMap.MemoAccountMapId == 0)
                {
                    updatedEntity = memoAccountMapRepository.Add(memoAccountMap);
                }
                else
                    updatedEntity = memoAccountMapRepository.Update(memoAccountMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMemoAccountMap(int memoAccountMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

                memoAccountMapRepository.Remove(memoAccountMapId);
            });
        }

        public MemoAccountMap GetMemoAccountMap(int memoAccountMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

                MemoAccountMap memoAccountMapEntity = memoAccountMapRepository.Get(memoAccountMapId);
                if (memoAccountMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MemoAccountMap with ID of {0} is not in database", memoAccountMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return memoAccountMapEntity;
            });
        }

        public MemoAccountMapData[] GetAllMemoAccountMaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoAccountMapRepository memoAccountMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoAccountMapRepository>();

                //var setUp = GetSetup();

                //IEnumerable<MemoAccountMap> memoAccountMap = memoAccountMapRepository.Get().Where (c=>c.Year == setUp.Year).ToArray();

                List<MemoAccountMapData> memoAccountMap = new List<MemoAccountMapData>();
                IEnumerable<MemoAccountMapInfo> memoAccountMapInfos = memoAccountMapRepository.GetMemoAccountMaps().ToArray();

                foreach (var memoAccountMapInfo in memoAccountMapInfos)
                {
                    memoAccountMap.Add(
                        new MemoAccountMapData
                        {
                            MemoAccountMapId = memoAccountMapInfo.MemoAccountMap.EntityId,
                            AccountNo = memoAccountMapInfo.MemoAccountMap.AccountNo,
                            Code = memoAccountMapInfo.MemoAccountMap.Code,
                            Name = memoAccountMapInfo.MemoUnits != null ? memoAccountMapInfo.MemoUnits.Name : string.Empty,
                            AccountName = memoAccountMapInfo.CustAccount != null ? memoAccountMapInfo.CustAccount.AccountName : string.Empty,
                            //  bsCaptionInfo.Parent != null ? bsCaptionInfo.Parent.EntityId : 0,
                            Active = memoAccountMapInfo.MemoAccountMap.Active
                        });
                }



                return memoAccountMap.ToArray();
            });
        }
        #endregion

        #region MemoGLMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MemoGLMap UpdateMemoGLMap(MemoGLMap memoGLMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

                MemoGLMap updatedEntity = null;

                if (memoGLMap.MemoGLMapId == 0)
                {

                    updatedEntity = memoGLMapRepository.Add(memoGLMap);
                }
                else
                    updatedEntity = memoGLMapRepository.Update(memoGLMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMemoGLMap(int memoGLMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

                memoGLMapRepository.Remove(memoGLMapId);
            });
        }

        public MemoGLMap GetMemoGLMap(int memoGLMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

                MemoGLMap memoGLMapEntity = memoGLMapRepository.Get(memoGLMapId);
                if (memoGLMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MemoGLMap with ID of {0} is not in database", memoGLMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return memoGLMapEntity;
            });
        }


        public MemoGLMapData[] GetAllMemoGLMaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoGLMapRepository memoGLMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoGLMapRepository>();

                List<MemoGLMapData> memoGLMap = new List<MemoGLMapData>();
                IEnumerable<MemoGLMapInfo> memoGLMapInfos = memoGLMapRepository.GetMemoGLMaps().ToArray();

                foreach (var memoGLMapInfo in memoGLMapInfos)
                {
                    memoGLMap.Add(
                        new MemoGLMapData
                        {
                            MemoGLMapId = memoGLMapInfo.MemoGLMap.EntityId,
                            Code = memoGLMapInfo.MemoGLMap.Code,
                            Name = memoGLMapInfo.MemoUnits.Name,
                            GLCode = memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.GL_Code : string.Empty,
                            GLDescription = memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.Description : string.Empty,
                            Active = memoGLMapInfo.MemoGLMap.Active
                        });
                }

                //memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.GL_Code : string.Empty,
                //memoGLMapInfo.GLDefinition != null ? memoGLMapInfo.GLDefinition.Description : string.Empty,

                return memoGLMap.ToArray();
            });
        }

        #endregion

        #region MemoProductMap operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MemoProductMap UpdateMemoProductMap(MemoProductMap memoProductMap)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

                MemoProductMap updatedEntity = null;

                if (memoProductMap.MemoProductMapId == 0)
                {
                    updatedEntity = memoProductMapRepository.Add(memoProductMap);
                }
                else
                    updatedEntity = memoProductMapRepository.Update(memoProductMap);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMemoProductMap(int memoProductMapId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

                memoProductMapRepository.Remove(memoProductMapId);
            });
        }

        public MemoProductMap GetMemoProductMap(int memoProductMapId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

                MemoProductMap memoProductMapEntity = memoProductMapRepository.Get(memoProductMapId);
                if (memoProductMapEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MemoProductMap with ID of {0} is not in database", memoProductMapId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return memoProductMapEntity;
            });
        }

        public MemoProductMapData[] GetAllMemoProductMaps()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoProductMapRepository memoProductMapRepository = _DataRepositoryFactory.GetDataRepository<IMemoProductMapRepository>();

                List<MemoProductMapData> memoProductMap = new List<MemoProductMapData>();
                IEnumerable<MemoProductMapInfo> memoProductMapInfos = memoProductMapRepository.GetMemoProductMaps().ToArray();

                foreach (var memoProductMapInfo in memoProductMapInfos)
                {
                    memoProductMap.Add(
                        new MemoProductMapData
                        {
                            MemoProductMapId = memoProductMapInfo.MemoProductMap.EntityId,
                            ProductCode = memoProductMapInfo.MemoProductMap.ProductCode,
                            ProductName = memoProductMapInfo.Product != null ? memoProductMapInfo.Product.Name : string.Empty,
                            Code = memoProductMapInfo.MemoProductMap.Code,
                            UnitName = memoProductMapInfo.MemoUnits.Name,
                            Active = memoProductMapInfo.MemoProductMap.Active
                        });
                }


                return memoProductMap.ToArray();
            });
        }
        #endregion

        #region MemoUnits operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MemoUnits UpdateMemoUnits(MemoUnits memoUnit)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();

                MemoUnits updatedEntity = null;

                if (memoUnit.MemoUnitsId == 0)
                {

                    updatedEntity = memoUnitRepository.Add(memoUnit);
                }
                else
                    updatedEntity = memoUnitRepository.Update(memoUnit);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMemoUnits(int memoUnitId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();

                memoUnitRepository.Remove(memoUnitId);
            });
        }

        public MemoUnits GetMemoUnits(int memoUnitId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();

                MemoUnits memoUnitEntity = memoUnitRepository.Get(memoUnitId);
                if (memoUnitEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MemoUnits with ID of {0} is not in database", memoUnitId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return memoUnitEntity;
            });
        }

        public MemoUnits[] GetAllMemoUnits()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMemoUnitsRepository memoUnitRepository = _DataRepositoryFactory.GetDataRepository<IMemoUnitsRepository>();


                IEnumerable<MemoUnits> memoUnit = memoUnitRepository.Get().ToArray();

                return memoUnit.ToArray();
            });
        }

        #endregion

        #region CaptionMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CaptionMapping UpdateCaptionMapping(CaptionMapping captionMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaptionMappingRepository captionMappingRepository = _DataRepositoryFactory.GetDataRepository<ICaptionMappingRepository>();

                CaptionMapping updatedEntity = null;

                if (captionMapping.CaptionMappingId == 0)
                {

                    updatedEntity = captionMappingRepository.Add(captionMapping);
                }
                else
                    updatedEntity = captionMappingRepository.Update(captionMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCaptionMapping(int captionMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaptionMappingRepository captionMappingRepository = _DataRepositoryFactory.GetDataRepository<ICaptionMappingRepository>();

                captionMappingRepository.Remove(captionMappingId);
            });
        }

        public CaptionMapping GetCaptionMapping(int captionMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaptionMappingRepository captionMappingRepository = _DataRepositoryFactory.GetDataRepository<ICaptionMappingRepository>();

                CaptionMapping captionMappingEntity = captionMappingRepository.Get(captionMappingId);
                if (captionMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("BSExemption with ID of {0} is not in database", captionMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return captionMappingEntity;
            });
        }

        public CaptionMapping[] GetAllCaptionMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaptionMappingRepository captionMappingRepository = _DataRepositoryFactory.GetDataRepository<ICaptionMappingRepository>();


                IEnumerable<CaptionMapping> captionMapping = captionMappingRepository.Get().ToArray();

                return captionMapping.ToArray();
            });
        }

        #endregion

        #region RatioCaptionMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RatioCaptionMapping UpdateRatioCaptionMapping(RatioCaptionMapping ratioCaptionMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatioCaptionMappingRepository ratioCaptionMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatioCaptionMappingRepository>();

                RatioCaptionMapping updatedEntity = null;

                if (ratioCaptionMapping.RatioCaptionMappingId == 0)
                {

                    updatedEntity = ratioCaptionMappingRepository.Add(ratioCaptionMapping);
                }
                else
                    updatedEntity = ratioCaptionMappingRepository.Update(ratioCaptionMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRatioCaptionMapping(int ratioCaptionMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatioCaptionMappingRepository ratioCaptionMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatioCaptionMappingRepository>();

                ratioCaptionMappingRepository.Remove(ratioCaptionMappingId);
            });
        }

        public RatioCaptionMapping GetRatioCaptionMapping(int ratioCaptionMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatioCaptionMappingRepository ratioCaptionMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatioCaptionMappingRepository>();

                RatioCaptionMapping ratioCaptionMappingEntity = ratioCaptionMappingRepository.Get(ratioCaptionMappingId);
                if (ratioCaptionMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("RatioCaptionMapping with ID of {0} is not in database", ratioCaptionMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ratioCaptionMappingEntity;
            });
        }

        public RatioCaptionMapping[] GetAllRatioCaptionMappings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatioCaptionMappingRepository ratioCaptionMappingRepository = _DataRepositoryFactory.GetDataRepository<IRatioCaptionMappingRepository>();


                IEnumerable<RatioCaptionMapping> ratioCaptionMapping = ratioCaptionMappingRepository.Get().ToArray();

                return ratioCaptionMapping.ToArray();
            });
        }

        #endregion

        #region Ratios operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Ratios UpdateRatios(Ratios ratios)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatiosRepository ratiosRepository = _DataRepositoryFactory.GetDataRepository<IRatiosRepository>();

                Ratios updatedEntity = null;

                if (ratios.RatiosId == 0)
                {

                    updatedEntity = ratiosRepository.Add(ratios);
                }
                else
                    updatedEntity = ratiosRepository.Update(ratios);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRatios(int ratiosId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatiosRepository ratiosRepository = _DataRepositoryFactory.GetDataRepository<IRatiosRepository>();

                ratiosRepository.Remove(ratiosId);
            });
        }

        public Ratios GetRatios(int ratiosId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatiosRepository ratiosRepository = _DataRepositoryFactory.GetDataRepository<IRatiosRepository>();

                Ratios ratiosEntity = ratiosRepository.Get(ratiosId);
                if (ratiosEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Ratios with ID of {0} is not in database", ratiosId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ratiosEntity;
            });
        }

        public Ratios[] GetAllRatios()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRatiosRepository ratiosRepository = _DataRepositoryFactory.GetDataRepository<IRatiosRepository>();


                IEnumerable<Ratios> ratios = ratiosRepository.Get().ToArray();

                return ratios.ToArray();
            });
        }

        #endregion

        #region AbcRatio operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public AbcRatio UpdateAbcRatio(AbcRatio abcRatio)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAbcRatioRepository abcRatioRepository = _DataRepositoryFactory.GetDataRepository<IAbcRatioRepository>();

                AbcRatio updatedEntity = null;

                if (abcRatio.AbcRatioId == 0)
                {

                    updatedEntity = abcRatioRepository.Add(abcRatio);
                }
                else
                    updatedEntity = abcRatioRepository.Update(abcRatio);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAbcRatio(int abcRatioId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAbcRatioRepository abcRatioRepository = _DataRepositoryFactory.GetDataRepository<IAbcRatioRepository>();

                abcRatioRepository.Remove(abcRatioId);
            });
        }

        public AbcRatio GetAbcRatio(int abcRatioId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAbcRatioRepository abcRatioRepository = _DataRepositoryFactory.GetDataRepository<IAbcRatioRepository>();

                AbcRatio abcRatioEntity = abcRatioRepository.Get(abcRatioId);
                if (abcRatioEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("AbcRatio with ID of {0} is not in database", abcRatioId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return abcRatioEntity;
            });
        }

        public AbcRatio[] GetAllAbcRatio()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAbcRatioRepository abcRatioRepository = _DataRepositoryFactory.GetDataRepository<IAbcRatioRepository>();


                IEnumerable<AbcRatio> abcRatio = abcRatioRepository.Get().ToArray();

                return abcRatio.ToArray();
            });
        }

        #endregion

        #region Sbu operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Sbu UpdateSbu(Sbu sbu)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuRepository sbuRepository = _DataRepositoryFactory.GetDataRepository<ISbuRepository>();

                Sbu updatedEntity = null;

                if (sbu.SbuId == 0)
                {

                    updatedEntity = sbuRepository.Add(sbu);
                }
                else
                    updatedEntity = sbuRepository.Update(sbu);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSbu(int sbuId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuRepository sbuRepository = _DataRepositoryFactory.GetDataRepository<ISbuRepository>();

                sbuRepository.Remove(sbuId);
            });
        }

        public Sbu GetSbu(int sbuId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuRepository sbuRepository = _DataRepositoryFactory.GetDataRepository<ISbuRepository>();

                Sbu sbuEntity = sbuRepository.Get(sbuId);
                if (sbuEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Sbu with ID of {0} is not in database", sbuId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sbuEntity;
            });
        }

        public Sbu[] GetAllSbu()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuRepository sbuRepository = _DataRepositoryFactory.GetDataRepository<ISbuRepository>();


                IEnumerable<Sbu> sbu = sbuRepository.Get().ToArray();

                return sbu.ToArray();
            });
        }

        #endregion

        #region SbuType operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public SbuType UpdateSbuType(SbuType sbuType)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuTypeRepository sbuTypeRepository = _DataRepositoryFactory.GetDataRepository<ISbuTypeRepository>();

                SbuType updatedEntity = null;

                if (sbuType.SbuTypeId == 0)
                {

                    updatedEntity = sbuTypeRepository.Add(sbuType);
                }
                else
                    updatedEntity = sbuTypeRepository.Update(sbuType);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSbuType(int sbuTypeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuTypeRepository sbuTypeRepository = _DataRepositoryFactory.GetDataRepository<ISbuTypeRepository>();

                sbuTypeRepository.Remove(sbuTypeId);
            });
        }

        public SbuType GetSbuType(int sbuTypeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuTypeRepository sbuTypeRepository = _DataRepositoryFactory.GetDataRepository<ISbuTypeRepository>();

                SbuType sbuTypeEntity = sbuTypeRepository.Get(sbuTypeId);
                if (sbuTypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("SbuType with ID of {0} is not in database", sbuTypeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return sbuTypeEntity;
            });
        }

        public SbuType[] GetAllSbuType()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISbuTypeRepository sbuTypeRepository = _DataRepositoryFactory.GetDataRepository<ISbuTypeRepository>();


                IEnumerable<SbuType> sbuType = sbuTypeRepository.Get().ToArray();

                return sbuType.ToArray();
            });
        }

        #endregion

        #region Servicese operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Servicese UpdateServices(Servicese services)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IServiceseRepository servicesRepository = _DataRepositoryFactory.GetDataRepository<IServiceseRepository>();

                Servicese updatedEntity = null;

                if (services.ServicesId == 0)
                {

                    updatedEntity = servicesRepository.Add(services);
                }
                else
                    updatedEntity = servicesRepository.Update(services);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteServices(int servicesId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IServiceseRepository servicesRepository = _DataRepositoryFactory.GetDataRepository<IServiceseRepository>();

                servicesRepository.Remove(servicesId);
            });
        }

        public Servicese GetServices(int servicesId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IServiceseRepository servicesRepository = _DataRepositoryFactory.GetDataRepository<IServiceseRepository>();

                Servicese servicesEntity = servicesRepository.Get(servicesId);
                if (servicesEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Services with ID of {0} is not in database", servicesId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return servicesEntity;
            });
        }

        public Servicese[] GetAllServices()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IServiceseRepository servicesRepository = _DataRepositoryFactory.GetDataRepository<IServiceseRepository>();


                IEnumerable<Servicese> services = servicesRepository.Get().ToArray();

                return services.ToArray();
            });
        }

        #endregion

        #region MessagingSubscription operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public MessagingSubscription UpdateMessagingSubscription(MessagingSubscription messagingSubscription)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMessagingSubscriptionRepository messagingSubscriptionRepository = _DataRepositoryFactory.GetDataRepository<IMessagingSubscriptionRepository>();

                MessagingSubscription updatedEntity = null;

                if (messagingSubscription.MessagingSubscriptionId == 0)
                {
                    //messagingSubscription.Year = GetSetup().Year;
                    updatedEntity = messagingSubscriptionRepository.Add(messagingSubscription);
                    MessagingSubscriptionTriggeredBy(messagingSubscription);
                }
                else
                    updatedEntity = messagingSubscriptionRepository.Update(messagingSubscription);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMessagingSubscription(int messagingSubscriptionId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMessagingSubscriptionRepository messagingSubscriptionRepository = _DataRepositoryFactory.GetDataRepository<IMessagingSubscriptionRepository>();

                messagingSubscriptionRepository.Remove(messagingSubscriptionId);
            });
        }

        public MessagingSubscription GetMessagingSubscription(int messagingSubscriptionId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMessagingSubscriptionRepository messagingSubscriptionRepository = _DataRepositoryFactory.GetDataRepository<IMessagingSubscriptionRepository>();

                MessagingSubscription messagingSubscriptionEntity = messagingSubscriptionRepository.Get(messagingSubscriptionId);
                if (messagingSubscriptionEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MessagingSubscription with ID of {0} is not in database", messagingSubscriptionId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return messagingSubscriptionEntity;
            });
        }


        //public MessagingSubscription GetMessagingSubscriptionByRecipients(string recipients)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IMessagingSubscriptionRepository messagingSubscriptionRepository = _DataRepositoryFactory.GetDataRepository<IMessagingSubscriptionRepository>();

        //        MessagingSubscription messagingSubscriptionEntity = messagingSubscriptionRepository.GetByRecipients(recipients);
        //        if (messagingSubscriptionEntity == null)
        //        {
        //            NotFoundException ex = new NotFoundException(string.Format("MessagingSubscription with ID of {0} is not in database", recipients));
        //            throw new FaultException<NotFoundException>(ex, ex.Message);
        //        }

        //        return messagingSubscriptionEntity;
        //    });
        //}



        public Revenue[] GetMessagingSubscriptionByRecipients(string recipients)
        {
            var connectionString = GetDataConnection();

            var revenues = new List<Revenue>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("MPR_GetEmailRecipients", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Levels",
                    Value = recipients,
                });

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var revenue = new Revenue();

                    if (reader["RevenueId"] != DBNull.Value)
                        revenue.RevenueId = int.Parse(reader["RevenueId"].ToString());

                    if (reader["TeamCode"] != DBNull.Value)
                        revenue.TeamCode = reader["TeamCode"].ToString();

                    if (reader["Narrative"] != DBNull.Value)
                        revenue.Narrative = reader["Narrative"].ToString();

                    if (reader["GLCode"] != DBNull.Value)
                        revenue.GLCode = reader["GLCode"].ToString();

                    if (reader["BranchCode"] != DBNull.Value)
                        revenue.BranchCode = reader["BranchCode"].ToString();

                    if (reader["Active"] != DBNull.Value)
                        revenue.Active = bool.Parse(reader["Active"].ToString());

                    revenues.Add(revenue);
                }

                con.Close();
            }

            return revenues.ToArray();
        }

        public DateTime[] GetRecipents()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<DateTime> refno;
            var recipentList = new List<DateTime>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("mpr_GetReportDates", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var myRecipent = new RecipentsModel();
                        if (reader["Rundate"] != DBNull.Value)
                            myRecipent.Rundate = DateTime.Parse(reader["Rundate"].ToString());
                        recipentList.Add(myRecipent.Rundate);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return recipentList.ToArray();
        }

        public string[] GetReports()
        {
            //var connectionString = IFRSContext.GetDataConnection();
            var connectionString = GetDataConnection();

            List<string> refno;
            var reportsList = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("mpr_get_email_reports", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;


                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        var myReport = new ReportModel();
                        if (reader["Description"] != DBNull.Value)
                            myReport.Description = reader["Description"].ToString();
                        reportsList.Add(myReport.Description);
                    }
                    reader.Close();
                    con.Close();
                }

                con.Close();
            }
            return reportsList.ToArray();
        }



        public void MessagingSubscriptionTriggeredBy(MessagingSubscription messagingSubscription)
        {
            var connectionString = GetDataConnection();

            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("sp_start_job", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@eMessage",
                    Value = messagingSubscription.Recipents,
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@eMessage",
                    Value = messagingSubscription.eMessage
                });

                cmd.CommandTimeout = 0;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

        }

        #endregion

        #region Staffs operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Staffs UpdateStaffs(Staffs staffs)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffsRepository staffRepository = _DataRepositoryFactory.GetDataRepository<IStaffsRepository>();

                Staffs updatedEntity = null;

                if (staffs.StaffId == 0)
                    updatedEntity = staffRepository.Add(staffs);
                else
                    updatedEntity = staffRepository.Update(staffs);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteStaffs(int staffId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffsRepository staffRepository = _DataRepositoryFactory.GetDataRepository<IStaffsRepository>();

                staffRepository.Remove(staffId);
            });
        }

        public Staffs GetStaffs(int staffId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffsRepository staffRepository = _DataRepositoryFactory.GetDataRepository<IStaffsRepository>();

                Staffs staffEntity = staffRepository.Get(staffId);
                if (staffEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Staffs with ID of {0} is not in database", staffId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return staffEntity;
            });
        }

        public Staffs[] GetAllStaffs()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IStaffsRepository staffRepository = _DataRepositoryFactory.GetDataRepository<IStaffsRepository>();

                IEnumerable<Staffs> staffs = staffRepository.Get().Where(c => c.Active);

                return staffs.ToArray();
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

        private SetUp GetSetup()
        {
            ISetUpRepository setupRepository = _DataRepositoryFactory.GetDataRepository<ISetUpRepository>();

            var setup = setupRepository.Get().FirstOrDefault();
            if (setup == null)
            {
                NotFoundException ex = new NotFoundException(string.Format("MPR setup information is not in database"));
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            return setup;
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

        #region CRB Data
        public crb_Data[] GetAllCrbData()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICrb_DataRepository crbDataRepository = _DataRepositoryFactory.GetDataRepository<ICrb_DataRepository>();

                IEnumerable<crb_Data> crbdata = crbDataRepository.Get();

                return crbdata.ToArray();
            });
        }

        public IEnumerable<crb_Data> crbData()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICrb_DataRepository crbDataRepository = _DataRepositoryFactory.GetDataRepository<ICrb_DataRepository>();

                IEnumerable<crb_Data> crbdata = crbDataRepository.Get();

                return crbdata.ToArray();
            });
        }

        #endregion

        #region Account Interest
        public account_interest[] GellALlAccountInterest()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAccount_interestRepository accountinterestRepository = _DataRepositoryFactory.GetDataRepository<IAccount_interestRepository>();

                IEnumerable<account_interest> accountinterest = accountinterestRepository.Get();

                return accountinterest.ToArray();
            });
        }

        #endregion

        #region Product Interest
        public product_interest[] GetAllProductInterest_OLD()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductInterestRepository productinterestRepository = _DataRepositoryFactory.GetDataRepository<IProductInterestRepository>();

                IEnumerable<product_interest> productinterest = productinterestRepository.Get();

                return productinterest.ToArray();
            });
        }
        #endregion

        #region Team Structure

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamStructure UpdateTeamStructure(TeamStructure teamstructure)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository tsRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                TeamStructure updatedEntity = null;

                if (teamstructure.Team_StructureId == 0)
                {

                    updatedEntity = tsRepository.Add(teamstructure);
                }
                else
                    updatedEntity = tsRepository.Update(teamstructure);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamStructure(int Team_StructureId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository tsRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                tsRepository.Remove(Team_StructureId);
            });
        }

        public TeamStructure GetTeamStructure(int Team_StructureId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository tsRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                TeamStructure tsEntity = tsRepository.Get(Team_StructureId);
                if (tsEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Team structure with ID of {0} is not in database", Team_StructureId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return tsEntity;
            });
        }
        public TeamStructure[] GetAllTeamStructure()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository teamstructureRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                IEnumerable<TeamStructure> teamstructure = teamstructureRepository.Get();

                return teamstructure.ToArray();
            });
        }

        public TeamStructure[] GetTeamStructureUsingParams(string SearchValue, string year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository teamstructureRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                IEnumerable<TeamStructure> teamstructure = teamstructureRepository.GetTeamstructureByParams(SearchValue, year);

                return teamstructure.ToArray();
            });
        }

        public TeamStructure[] TeamstructureByParameters(string selectedDefinitionCode, string SearchValue, string year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository teamstructureRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                IEnumerable<TeamStructure> teamstructure = teamstructureRepository.GetTeamstructureByParameters(selectedDefinitionCode, SearchValue, year);

                return teamstructure.ToArray();
            });
        }

        public TeamStructure[] GetTeamstructureByParamsAndeSetUp(string code, string SearchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository teamstructureRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                IEnumerable<TeamStructure> teamstructure = teamstructureRepository.GetTeamstructureByParamsAndeSetUp(code, SearchValue);

                return teamstructure.ToArray();
            });
        }

        public TeamStructure[] GetTeamStructureUsingSetUp()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository teamstructureRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                IEnumerable<TeamStructure> teamstructure = teamstructureRepository.GetTeamstructureBySetUp();

                return teamstructure.ToArray();
            });
        }

        public TeamStructure[] GetTeamStructureUsingDefinitionCode(string code)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository teamstructureRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                IEnumerable<TeamStructure> teamstructure = teamstructureRepository.GetTeamstructureByDefinitionCode(code);

                return teamstructure.ToArray();
            });
        }

        public TeamStructure[] GetTeamStructureUsingDefinitionCodeMonthly(string code)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository teamstructureRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                IEnumerable<TeamStructure> teamstructure = teamstructureRepository.GetTeamstructureByDefinitionCodeMonthly(code);

                return teamstructure.ToArray();
            });
        }

        public TeamStructure[] GetTeamstructureByParamsAndeSetUpMonthly(string code, string SearchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository teamstructureRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                IEnumerable<TeamStructure> teamstructure = teamstructureRepository.GetTeamstructureByParamsAndeSetUpMonthly(code, SearchValue);

                return teamstructure.ToArray();
            });
        }

        public TeamStructure[] GetTeamStructureUsingSetUpMonthly()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository teamstructureRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                IEnumerable<TeamStructure> teamstructure = teamstructureRepository.GetTeamstructureBySetUpMonthly();

                return teamstructure.ToArray();
            });
        }

        //public TeamStructure GetTeamStructureTop1(string branch, string year)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        ITeamStructureRepository teamstructureRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

        //        TeamStructure teamstructure = teamstructureRepository.GetTeamStructureTop1(branch, year);

        //        return teamstructure;
        //    });
        //}

        public TeamStructure GetTeamStructureTop1(string branch, string defcode, string year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureRepository teamstructureRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureRepository>();

                TeamStructure teamstructure = teamstructureRepository.GetTeamStructureTop1(branch, defcode, year);

                return teamstructure;
            });
        }


        #endregion

        #region product_interestData

        public product_interestData[] GetAllProductInterest()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductInterestRepository productinterestRepository = _DataRepositoryFactory.GetDataRepository<IProductInterestRepository>();

                List<product_interestData> productinterest = new List<product_interestData>();
                IEnumerable<ProductInterestInfo> productinterestInfos = productinterestRepository.GetProductInterests().ToArray();

                foreach (var productinterestInfo in productinterestInfos)
                {
                    productinterest.Add(
                        new product_interestData
                        {
                            // TransferPriceId = transferPriceInfo.TransferPrice.EntityId,
                            product_interestId = productinterestInfo.product_interest.EntityId,
                            //ProductCode = transferPriceInfo.TransferPrice.ProductCode,
                            ProductCode = productinterestInfo.product_interest.ProductCode,
                            // ProductName = transferPriceInfo.TransferPrice != null ? transferPriceInfo.Product.Name : string.Empty,
                            ProductName = productinterestInfo.product_interest != null ? productinterestInfo.Product.Name : string.Empty,
                            //Category = (Shared.Core.Framework.AccountTypeEnum)productinterestInfo.product_interest.Category,
                            Category = productinterestInfo.product_interest.Category,
                            CategoryName = productinterestInfo.product_interest.Category.ToString(),
                            InterestRate = productinterestInfo.product_interest.InterestRate

                        });
                }

                return productinterest.ToArray();
            });
        }

        public PublicSectorData[] GetAllPublicSectorData_1()
        {

            var connectionString = GetDataConnection();

            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var publicSectorDatas = new List<PublicSectorData>();
            using (var con = new SqlConnection(connectionString))
            {
                //var cmd = new SqlCommand("spp_ifrs_BondConsolidated_Get", con);
                var cmd = new SqlCommand("vw_mpr_public_sector", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var publicSectorData = new PublicSectorData();

                    if (reader["Account_Number"] != DBNull.Value)
                        publicSectorData.Account_Number = reader["Account_Number"].ToString();

                    if (reader["CustomerName"] != DBNull.Value)
                        publicSectorData.CustomerName = reader["CustomerName"].ToString();

                    if (reader["ProductCode"] != DBNull.Value)
                        publicSectorData.ProductCode = reader["ProductCode"].ToString();

                    publicSectorDatas.Add(publicSectorData);
                }

                con.Close();
            }

            return publicSectorDatas.ToArray();
        }


        public PublicSectorData[] GetAllPublicSectorData()
        {

            var connectionString = GetDataConnection();

            //var connectionString = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

            var publicSectorDatas = new List<PublicSectorData>();
            using (var con = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("spp_vw_mpr_public_sector", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var publicSectorData = new PublicSectorData();

                    if (reader["Account_Number"] != DBNull.Value)
                        publicSectorData.Account_Number = reader["Account_Number"].ToString();

                    if (reader["CustomerName"] != DBNull.Value)
                        publicSectorData.CustomerName = reader["CustomerName"].ToString();

                    if (reader["ProductCode"] != DBNull.Value)
                        publicSectorData.ProductCode = reader["ProductCode"].ToString();

                    publicSectorDatas.Add(publicSectorData);
                }

                con.Close();
            }

            return publicSectorDatas.ToArray();
        }

        #endregion


        #region Corporate Adjustment operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public CorporateAdjustment UpdateCorporateAdjustment(CorporateAdjustment corporateadjustment)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICorporateAdjustmentRepository corporateadjustmentRepository = _DataRepositoryFactory.GetDataRepository<ICorporateAdjustmentRepository>();

                CorporateAdjustment updatedEntity = null;

                if (corporateadjustment.CorporateAdjustmentId == 0)
                {

                    updatedEntity = corporateadjustmentRepository.Add(corporateadjustment);
                }
                else
                    updatedEntity = corporateadjustmentRepository.Update(corporateadjustment);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCorporateAdjustment(int CorporateAdjustmentId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICorporateAdjustmentRepository corporateadjustmentRepository = _DataRepositoryFactory.GetDataRepository<ICorporateAdjustmentRepository>();

                corporateadjustmentRepository.Remove(CorporateAdjustmentId);
            });
        }

        public CorporateAdjustment GetCorporateAdjustment(int CorporateAdjustmentId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICorporateAdjustmentRepository corporateadjustmentRepository = _DataRepositoryFactory.GetDataRepository<ICorporateAdjustmentRepository>();

                CorporateAdjustment corporateadjustmentEntity = corporateadjustmentRepository.Get(CorporateAdjustmentId);
                if (corporateadjustmentEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("CorporateAdjustment with ID of {0} is not in database", CorporateAdjustmentId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return corporateadjustmentEntity;
            });
        }

        public CorporateAdjustment[] GetAllCorporateAdjustment()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICorporateAdjustmentRepository corporateadjustmentRepository = _DataRepositoryFactory.GetDataRepository<ICorporateAdjustmentRepository>();


                IEnumerable<CorporateAdjustment> corporateadjustment = corporateadjustmentRepository.Get();

                return corporateadjustment.ToArray();
            });
        }

        #endregion

        #region Caption Transfer Price operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public caption_transfer_price UpdateCaptionTransferPrice(caption_transfer_price ctp)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaption_transfer_priceRepository ctpRepository = _DataRepositoryFactory.GetDataRepository<ICaption_transfer_priceRepository>();

                caption_transfer_price updatedEntity = null;

                if (ctp.caption_transfer_price_Id == 0)
                {

                    updatedEntity = ctpRepository.Add(ctp);
                }
                else
                    updatedEntity = ctpRepository.Update(ctp);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCaptionTransferPrice(int caption_transfer_price_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaption_transfer_priceRepository ctpRepository = _DataRepositoryFactory.GetDataRepository<ICaption_transfer_priceRepository>();

                ctpRepository.Remove(caption_transfer_price_Id);
            });
        }

        public caption_transfer_price GetCaptionTransferPrice(int caption_transfer_price_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaption_transfer_priceRepository ctpRepository = _DataRepositoryFactory.GetDataRepository<ICaption_transfer_priceRepository>();

                caption_transfer_price ctpEntity = ctpRepository.Get(caption_transfer_price_Id);
                if (ctpEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("caption transfer price with ID of {0} is not in database", caption_transfer_price_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ctpEntity;
            });
        }

        public caption_transfer_price[] GetAllCaptionTransferPrice()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICaption_transfer_priceRepository ctpRepository = _DataRepositoryFactory.GetDataRepository<ICaption_transfer_priceRepository>();


                IEnumerable<caption_transfer_price> ctp = ctpRepository.Get();

                return ctp.ToArray();
            });
        }

        #endregion


        #region Asset Type operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public AssetType UpdateAssetType(AssetType assettype)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAssetTypeRepository atRepository = _DataRepositoryFactory.GetDataRepository<IAssetTypeRepository>();

                AssetType updatedEntity = null;

                if (assettype.AssetType_Id == 0)
                {

                    updatedEntity = atRepository.Add(assettype);
                }
                else
                    updatedEntity = atRepository.Update(assettype);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteAssetType(int AssetType_Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAssetTypeRepository atRepository = _DataRepositoryFactory.GetDataRepository<IAssetTypeRepository>();

                atRepository.Remove(AssetType_Id);
            });
        }

        public AssetType GetAssetType(int AssetType_Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAssetTypeRepository atRepository = _DataRepositoryFactory.GetDataRepository<IAssetTypeRepository>();

                AssetType assettypeEntity = atRepository.Get(AssetType_Id);
                if (assettypeEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Asset Type with ID of {0} is not in database", AssetType_Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return assettypeEntity;
            });
        }

        public AssetType[] GetAllAssetType()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IAssetTypeRepository atRepository = _DataRepositoryFactory.GetDataRepository<IAssetTypeRepository>();


                IEnumerable<AssetType> atype = atRepository.Get();

                return atype.ToArray();
            });
        }

        #endregion


        #region Custormer MIS operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public Customermis UpdateCustomermis(Customermis customermis)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICustomermisRepository customermisRepository = _DataRepositoryFactory.GetDataRepository<ICustomermisRepository>();

                Customermis updatedEntity = null;

                if (customermis.CustomermisId == 0)
                {

                    updatedEntity = customermisRepository.Add(customermis);
                }
                else
                    updatedEntity = customermisRepository.Update(customermis);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteCustomermis(int CustomermisId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICustomermisRepository customermisRepository = _DataRepositoryFactory.GetDataRepository<ICustomermisRepository>();

                customermisRepository.Remove(CustomermisId);
            });
        }

        public Customermis GetCustomermis(int CustomermisId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICustomermisRepository customermisRepository = _DataRepositoryFactory.GetDataRepository<ICustomermisRepository>();

                Customermis customermisEntity = customermisRepository.Get(CustomermisId);
                if (customermisEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Customer MIS with ID of {0} is not in database", CustomermisId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return customermisEntity;
            });
        }

        public Customermis[] GetAllCustomermis()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ICustomermisRepository customermisRepository = _DataRepositoryFactory.GetDataRepository<ICustomermisRepository>();


                IEnumerable<Customermis> customermis = customermisRepository.Get().ToArray();

                return customermis.ToArray();
            });
        }
        #endregion

        #region PPR operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public PPR UpdatePPR(PPR ppr)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPPRRepository pprRepository = _DataRepositoryFactory.GetDataRepository<IPPRRepository>();

                PPR updatedEntity = null;

                if (ppr.PPRId == 0)
                {

                    updatedEntity = pprRepository.Add(ppr);
                }
                else
                    updatedEntity = pprRepository.Update(ppr);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeletePPR(int PPRId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPPRRepository pprRepository = _DataRepositoryFactory.GetDataRepository<IPPRRepository>();

                pprRepository.Remove(PPRId);
            });
        }

        public PPR GetPPR(int PPRId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPPRRepository pprRepository = _DataRepositoryFactory.GetDataRepository<IPPRRepository>();

                PPR pprEntity = pprRepository.Get(PPRId);
                if (pprEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("PPR with ID of {0} is not in database", PPRId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return pprEntity;
            });
        }

        public PPR[] GetAllPPR()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IPPRRepository pprRepository = _DataRepositoryFactory.GetDataRepository<IPPRRepository>();

                IEnumerable<PPR> ppr = pprRepository.Get().ToArray();

                return ppr.ToArray();
            });
        }
        #endregion

        #region Risk Adjusted Charge operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public RiskAdjustedCharge UpdateRiskAdjustedCharge(RiskAdjustedCharge rac)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRiskAdjustedChargeRepository racRepository = _DataRepositoryFactory.GetDataRepository<IRiskAdjustedChargeRepository>();

                RiskAdjustedCharge updatedEntity = null;

                if (rac.RiskAdjustedChargeId == 0)
                {

                    updatedEntity = racRepository.Add(rac);
                }
                else
                    updatedEntity = racRepository.Update(rac);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteRiskAdjustedCharge(int RiskAdjustedChargeId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRiskAdjustedChargeRepository racRepository = _DataRepositoryFactory.GetDataRepository<IRiskAdjustedChargeRepository>();

                racRepository.Remove(RiskAdjustedChargeId);
            });
        }

        public RiskAdjustedCharge GetRiskAdjustedCharge(int RiskAdjustedChargeId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRiskAdjustedChargeRepository racRepository = _DataRepositoryFactory.GetDataRepository<IRiskAdjustedChargeRepository>();

                RiskAdjustedCharge racEntity = racRepository.Get(RiskAdjustedChargeId);
                if (racEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Risk Adjusted Charge with ID of {0} is not in database", RiskAdjustedChargeId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return racEntity;
            });
        }

        public RiskAdjustedCharge[] GetAllRiskAdjustedCharge()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IRiskAdjustedChargeRepository racRepository = _DataRepositoryFactory.GetDataRepository<IRiskAdjustedChargeRepository>();

                IEnumerable<RiskAdjustedCharge> rac = racRepository.Get().ToArray();

                return rac.ToArray();
            });
        }
        #endregion


        #region MPR ScoreCard Metrics

        [OperationBehavior(TransactionScopeRequired = true)]
        public ScoreCardMetrics UpdateScoreCardMetric(ScoreCardMetrics scorecardmetric)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMetricsRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsRepository>();

                ScoreCardMetrics updatedEntity = null;

                if (scorecardmetric.MetricId == 0)
                {
                    updatedEntity = scmRepository.Add(scorecardmetric);
                }
                else
                    updatedEntity = scmRepository.Update(scorecardmetric);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteScoreCardMetric(int MetricId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMetricsRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsRepository>();

                scmRepository.Remove(MetricId);
            });
        }

        public ScoreCardMetrics GetScoreCardMetric(int MetricId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMetricsRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsRepository>();

                ScoreCardMetrics scmEntity = scmRepository.Get(MetricId);
                if (scmEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Metric with ID of {0} is not in database", MetricId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scmEntity;
            });
        }
        public ScoreCardMetrics[] GetAllScoreCardMetrics()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMetricsRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsRepository>();

                IEnumerable<ScoreCardMetrics> scm = scmRepository.Get();

                return scm.ToArray();
            });
        }

        //public ScoreCardMetrics[] GetScoreCardMetricsUsingSearchValue(string SearchValue)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IScoreCardMetricsRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsRepository>();

        //        IEnumerable<ScoreCardMetrics> scm = scmRepository.GetMetricsBySearchValue(SearchValue);

        //        return scm.ToArray();
        //    });
        //}

        //public ScoreCardMetricsData[] GetScoreCardMetricsUsingSetUp_1()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //    var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
        //    AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //    //IScoreCardMetricsRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsRepository>();

        //    //IEnumerable<ScoreCardMetrics> scm = scmRepository.GetMetricsBySetUp();

        //    //return scm.ToArray();


        //    IScoreCardMetricsRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsRepository>();

        //   // List<ScoreCardMetricsData> scm = new List<ScoreCardMetricsData>();
        //   var scm = scmRepository.GetMetricsBySetUp2();

        //        // IEnumerable<ScoreCardMetricsInfo> scm2 = scmRepository.GetMetricsBySetUp2();

        //        //foreach (var m in scm2)
        //        //{
        //        //    var dr = new ScoreCardMetricsData()
        //        //    {                  
        //        //        MetricId = m.MetricId,
        //        //        Metric_Code = m.Metric_Code,
        //        //        Metric_Description = m.Metric_Description,
        //        //        Metric = m.Metric,
        //        //        MisCode = m.MisCode,
        //        //        Metric_Score_determinant = m.Metric_Score_determinant,
        //        //        Metric_Position = m.Metric_Position,
        //        //        Period = m.Period,
        //        //        Year = m.Year,
        //        //        PerspectiveId = m.PerspectiveId,
        //        //        Perspective = m.perspective
        //        //    };

        //        //    scm.Add(dr);
        //        //    }

        //        return scm.ToList();
        //    });
        //}


        public ScoreCardMetricsData[] GetScoreCardMetricsUsingSetUp()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMetricsRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsRepository>();

                List<ScoreCardMetricsData> scw = new List<ScoreCardMetricsData>();

                IEnumerable<ScoreCardMetricsInfo> scw2 = scmRepository.GetMetricsBySetUp();

                foreach (var a in scw2)
                {
                    var dr = new ScoreCardMetricsData()
                    {
                        MetricId = a.MetricId,
                        Metric_Code = a.Metric_Code,
                        Metric_Description = a.Metric_Description,
                        Metric = a.Metric,
                        MisCode = a.MisCode,
                        Metric_Score_determinant = a.Metric_Score_determinant,
                        Metric_Position = a.Metric_Position,
                        Period = a.Period,
                        Year = a.Year,
                        PerspectiveId = a.PerspectiveId,
                        Perspective = a.perspective
                    };
                    scw.Add(dr);
                }

                return scw.ToArray();
            });
        }

        public ScoreCardMetricsData[] GetScoreCardMetricsUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMetricsRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsRepository>();

                List<ScoreCardMetricsData> scw = new List<ScoreCardMetricsData>();

                IEnumerable<ScoreCardMetricsInfo> scw2 = scmRepository.GetMetricsBySearchValue(searchvalue);

                foreach (var a in scw2)
                {
                    var dr = new ScoreCardMetricsData()
                    {
                        MetricId = a.MetricId,
                        Metric_Code = a.Metric_Code,
                        Metric_Description = a.Metric_Description,
                        Metric = a.Metric,
                        MisCode = a.MisCode,
                        Metric_Score_determinant = a.Metric_Score_determinant,
                        Metric_Position = a.Metric_Position,
                        Period = a.Period,
                        Year = a.Year,
                        PerspectiveId = a.PerspectiveId,
                        Perspective = a.perspective
                    };
                    scw.Add(dr);
                }

                return scw.ToArray();
            });
        }

        #endregion



        #region MPR ScoreCard Weight

        [OperationBehavior(TransactionScopeRequired = true)]
        public ScoreCardWeight UpdateScoreCardWeight(ScoreCardWeight scorecardweight)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardWeightRepository scwRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardWeightRepository>();

                ScoreCardWeight updatedEntity = null;

                if (scorecardweight.WeightId == 0)
                {
                    updatedEntity = scwRepository.Add(scorecardweight);
                }
                else
                    updatedEntity = scwRepository.Update(scorecardweight);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteScoreCardWeight(int WeightId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardWeightRepository scwRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardWeightRepository>();

                scwRepository.Remove(WeightId);
            });
        }

        public ScoreCardWeight GetScoreCardWeight(int WeightId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardWeightRepository scwRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardWeightRepository>();

                ScoreCardWeight scwEntity = scwRepository.Get(WeightId);
                if (scwEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Weight with ID of {0} is not in database", WeightId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scwEntity;
            });
        }
        public ScoreCardWeight[] GetAllScoreCardWeight()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardWeightRepository scwRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardWeightRepository>();

                IEnumerable<ScoreCardWeight> scw = scwRepository.Get();

                return scw.ToArray();
            });
        }

        public ScoreCardWeightData[] GetScoreCardWeightWITHMetrics()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardWeightRepository scwRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardWeightRepository>();

                List<ScoreCardWeightData> scw = new List<ScoreCardWeightData>();

                IEnumerable<ScoreCardWeightInfo> scw2 = scwRepository.GetScoreCardWeightANDMetrics();

                foreach(var w in scw2)
                {
                    var dr = new ScoreCardWeightData()
                    {
                        //WeightId = w.ScoreCardWeight.WeightId,
                        //Metric_Code = w.ScoreCardWeight.Metric_Code,
                        //Metric = w.Metric,
                        //Weight = w.ScoreCardWeight.Weight,
                        //Year = w.ScoreCardWeight.Year,
                        //Period = w.ScoreCardWeight.Period


                        WeightId = w.WeightId,
                        Metric_Code = w.Metric_Code,
                        Metric = w.Metric,
                        Weight = w.Weight,
                        Year = w.Year,
                        Period = w.Period
                    };
                    scw.Add(dr);
                }

                return scw.ToArray();
            });
        }

        //public ScoreCardMetrics[] GetScoreCardMetricsUsingSearchValue(string SearchValue)
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IScoreCardMetricsRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsRepository>();

        //        IEnumerable<ScoreCardMetrics> scm = scmRepository.GetMetricsBySearchValue(SearchValue);

        //        return scm.ToArray();
        //    });
        //}

        //public ScoreCardMetrics[] GetScoreCardMetricsUsingSetUp()
        //{
        //    return ExecuteFaultHandledOperation(() =>
        //    {
        //        var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
        //        AllowAccessToOperation(SOLUTION_NAME, groupNames);

        //        IScoreCardMetricsRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsRepository>();

        //        IEnumerable<ScoreCardMetrics> scm = scmRepository.GetMetricsBySetUp();

        //        return scm.ToArray();
        //    });
        //}

        #endregion

        #region MPR ScoreCard Perspective

        [OperationBehavior(TransactionScopeRequired = true)]
        public ScoreCardPerspective UpdateScorecardPerspective(ScoreCardPerspective scPerspective)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardPerspectiveRepository scpRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardPerspectiveRepository>();

                ScoreCardPerspective updatedEntity = null;

                if (scPerspective.PerspectiveId == 0)
                {
                    updatedEntity = scpRepository.Add(scPerspective);
                }
                else
                    updatedEntity = scpRepository.Update(scPerspective);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteScorecardPerspective(int PerspectiveId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardPerspectiveRepository scpRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardPerspectiveRepository>();

                scpRepository.Remove(PerspectiveId);
            });
        }

        public ScoreCardPerspective GetScorecardPerspective(int PerspectiveId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardPerspectiveRepository scpRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardPerspectiveRepository>();

                ScoreCardPerspective scpEntity = scpRepository.Get(PerspectiveId);
                if (scpEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Perspective with ID of {0} is not in database", PerspectiveId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scpEntity;
            });
        }
        public ScoreCardPerspective[] GetAllScorecardPerspective()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardPerspectiveRepository scpRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardPerspectiveRepository>();

                IEnumerable<ScoreCardPerspective> scp = scpRepository.Get();

                return scp.ToArray();
            });
        }

        #endregion

        #region MPR ScoreCard Mapping

        [OperationBehavior(TransactionScopeRequired = true)]
        public ScoreCardMapping UpdateScoreCardMapping(ScoreCardMapping scorecardmapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMappingRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMappingRepository>();

                ScoreCardMapping updatedEntity = null;

                if (scorecardmapping.MappingId == 0)
                {
                    updatedEntity = scmRepository.Add(scorecardmapping);
                }
                else
                    updatedEntity = scmRepository.Update(scorecardmapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteScoreCardMapping(int MappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMappingRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMappingRepository>();

                scmRepository.Remove(MappingId);
            });
        }

        public ScoreCardMapping GetScoreCardMapping(int MappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMappingRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMappingRepository>();

                ScoreCardMapping scmEntity = scmRepository.Get(MappingId);
                if (scmEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Mapping with ID of {0} is not in database", MappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scmEntity;
            });
        }
        public ScoreCardMapping[] GetAllScoreCardMapping()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMappingRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMappingRepository>();

                IEnumerable<ScoreCardMapping> scm = scmRepository.Get();

                return scm.ToArray();
            });
        }

        public ScoreCardMappingData[] GetScoreCardMappingUsingSearchValue(string SearchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMappingRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMappingRepository>();

                List<ScoreCardMappingData> scm = new List<ScoreCardMappingData>();

                IEnumerable<ScoreCardMappingInfo> scm2 = scmRepository.GetMappingBySearchValue(SearchValue);

                foreach (var s in scm2)
                {
                    var dr = new ScoreCardMappingData()
                    {
                        MappingId = s.MappingId,
                        Actual_Caption = s.Actual_Caption,
                        Budget_Caption = s.Budget_Caption,
                        Mapping_code = s.Mapping_code,
                        Metric = s.Metric,
                        Period = s.Period,
                        Year = s.Year
                    };
                    scm.Add(dr);
                }

                return scm.ToArray();
            });
        }

        public ScoreCardMappingData[] GetScoreCardMappingUsingSetUp()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMappingRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMappingRepository>();
                List<ScoreCardMappingData> scm = new List<ScoreCardMappingData>();
                IEnumerable<ScoreCardMappingInfo> scm2 = scmRepository.GetMappingBySetUp();

                foreach (var s in scm2)
                {
                    var dr = new ScoreCardMappingData()
                    {
                        MappingId = s.MappingId,
                        Actual_Caption = s.Actual_Caption,
                        Budget_Caption = s.Budget_Caption,
                        Mapping_code = s.Mapping_code,
                        Metric = s.Metric,
                        Period = s.Period,
                        Year = s.Year
                    };
                    scm.Add(dr);
                }

                return scm.ToArray();
            });
        }

        #endregion

        #region MPR ScoreCard

        [OperationBehavior(TransactionScopeRequired = true)]
        public ScoreCard UpdateScoreCard(ScoreCard scorecard)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardRepository scRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardRepository>();

                ScoreCard updatedEntity = null;

                if (scorecard.mpr_scorecard_stgId == 0)
                {
                    updatedEntity = scRepository.Add(scorecard);
                }
                else
                    updatedEntity = scRepository.Update(scorecard);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteScoreCard(int mpr_scorecard_stgId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardRepository scRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardRepository>();

                scRepository.Remove(mpr_scorecard_stgId);
            });
        }

        public ScoreCard GetScoreCard(int mpr_scorecard_stgId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardRepository scRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardRepository>();

                ScoreCard scEntity = scRepository.Get(mpr_scorecard_stgId);
                if (scEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Score card with ID of {0} is not in database", mpr_scorecard_stgId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scEntity;
            });
        }
        public ScoreCard[] GetAllScoreCard()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardRepository scRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardRepository>();

                IEnumerable<ScoreCard> sc = scRepository.Get();

                return sc.ToArray();
            });
        }
    
        public ScoreCard[] ScoreCardCaptions()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardRepository scRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardRepository>();

                IEnumerable<ScoreCard> sc = scRepository.GetScoreCardCaptions();

                return sc.ToArray();
            });
        }

        #endregion

        #region MIS Transfer price

        [OperationBehavior(TransactionScopeRequired = true)]
        public MISTransferPrice UpdateMISTransferPrice(MISTransferPrice mistransferprice)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISTransferPriceRepository mistpRepository = _DataRepositoryFactory.GetDataRepository<IMISTransferPriceRepository>();

                MISTransferPrice updatedEntity = null;

                if (mistransferprice.mistransferpriceId == 0)
                {
                    updatedEntity = mistpRepository.Add(mistransferprice);
                }
                else
                    updatedEntity = mistpRepository.Update(mistransferprice);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMISTransferPrice(int mistransferpriceId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISTransferPriceRepository mistpRepository = _DataRepositoryFactory.GetDataRepository<IMISTransferPriceRepository>();

                mistpRepository.Remove(mistransferpriceId);
            });
        }

        public MISTransferPrice GetMISTransferPrice(int mistransferpriceId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISTransferPriceRepository mistpRepository = _DataRepositoryFactory.GetDataRepository<IMISTransferPriceRepository>();

                MISTransferPrice mistpEntity = mistpRepository.Get(mistransferpriceId);
                if (mistpEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MIS Transfer Price with ID of {0} is not in database", mistransferpriceId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return mistpEntity;
            });
        }
        public MISTransferPrice[] GetAllMISTransferPrice()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISTransferPriceRepository mistpRepository = _DataRepositoryFactory.GetDataRepository<IMISTransferPriceRepository>();

                IEnumerable<MISTransferPrice> mistp = mistpRepository.Get();

                return mistp.ToArray();
            });
        }

        public MISTransferPriceData[] GetMISTransferPriceUsingSetUp()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISTransferPriceRepository mistpRepository = _DataRepositoryFactory.GetDataRepository<IMISTransferPriceRepository>();

                // IEnumerable<MISTransferPrice> mistp = mistpRepository.GetMISTransferPricebySetUp();

                List<MISTransferPriceData> mistp = new List<MISTransferPriceData>();

                IEnumerable<MISTransferPriceInfo> mistp2 = mistpRepository.GetMISTransferPricebySetUp();

                foreach (var d in mistp2)
                {
                    var dr = new MISTransferPriceData()
                    {
                        mistransferpriceId = d.mistransferpriceId,
                        DefinitionCode = d.DefinitionCode,
                        MisCode = d.MisCode,
                        BalanceSheetCategory = d.BalanceSheetCategory,
                        BSCategoryName = d.BalanceSheetCategory.ToString(),
                        CurrencyType = d.CurrencyType,
                        CurrencyTypeName = d.CurrencyType.ToString(),
                        Rate = d.Rate,
                        Period = d.Period,
                        Year = d.Year,
                        SolutionId = d.SolutionId,
                        CompanyCode = d.CompanyCode,
                    };
                    mistp.Add(dr);
                }

                return mistp.ToArray();
            });
        }

        public MISTransferPriceData[] GetMISTransferPricebyParams(string defCode, string miscode, string category, string currency, int year, int period)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                //IMISTransferPriceRepository mistpRepository = _DataRepositoryFactory.GetDataRepository<IMISTransferPriceRepository>();

                //IEnumerable<MISTransferPrice> mistp = mistpRepository.GetMISTransferPricebyParams(defCode, miscode, category, currency, year, period);

                //return mistp.ToArray();

                IMISTransferPriceRepository mistpRepository = _DataRepositoryFactory.GetDataRepository<IMISTransferPriceRepository>();

                List<MISTransferPriceData> mistp = new List<MISTransferPriceData>();

                IEnumerable<MISTransferPriceInfo> mistp2 = mistpRepository.GetMISTransferPricebySetUp();

                foreach (var d in mistp2)
                {
                    var dr = new MISTransferPriceData()
                    {
                        mistransferpriceId = d.mistransferpriceId,
                        DefinitionCode = d.DefinitionCode,
                        MisCode = d.MisCode,
                        BalanceSheetCategory = d.BalanceSheetCategory,
                        BSCategoryName = d.BalanceSheetCategory.ToString(),
                        CurrencyType = d.CurrencyType,
                        CurrencyTypeName = d.CurrencyType.ToString(),
                        Rate = d.Rate,
                        Period = d.Period,
                        Year = d.Year,
                        SolutionId = d.SolutionId,
                        CompanyCode = d.CompanyCode,
                    };
                    mistp.Add(dr);
                }

                return mistp.ToArray();
            });
        }

        #endregion


    # region Product Transfer Price

        [OperationBehavior(TransactionScopeRequired = true)]
        public ProductTransferPrice Updateproducttransferprice(ProductTransferPrice producttransferprice)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTransferPriceRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IProductTransferPriceRepository>();

                ProductTransferPrice updatedEntity = null;

                if (producttransferprice.ID == 0)
                {
                    updatedEntity = scmRepository.Add(producttransferprice);
                }
                else
                    updatedEntity = scmRepository.Update(producttransferprice);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void Deleteproducttransferprice(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTransferPriceRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IProductTransferPriceRepository>();

                scmRepository.Remove(ID);
            });
        }

        public ProductTransferPrice Getproducttransferprice(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTransferPriceRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IProductTransferPriceRepository>();

                ProductTransferPrice scmEntity = scmRepository.Get(ID);
                if (scmEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ProductTransferPrice with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scmEntity;
            });
        }
        public ProductTransferPrice[] GetAllProductTransferPrice()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTransferPriceRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IProductTransferPriceRepository>();

                IEnumerable<ProductTransferPrice> scm = scmRepository.Get();

                return scm.ToArray();
            });
        }


        public ProductTransferPriceData[] GetProductTransferPriceUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IProductTransferPriceRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IProductTransferPriceRepository>();

                List<ProductTransferPriceData> scw = new List<ProductTransferPriceData>();

                IEnumerable<ProductTransferPriceInfo> scw2 = scmRepository.GetProductTransferPriceBySearchValue(searchvalue);

                foreach (var a in scw2)
                {
                    var dr = new ProductTransferPriceData()
                    {
                        ID = a.ID,
                        ProductCode = a.ProductCode,
                        ProductName = a.ProductName,
                        Rating = a.Rating,
                        Description = a.Description,
                        Category = a.Category,
                        BSCategoryName = a.Category.ToString(),
                    };
                    scw.Add(dr);
                }

                return scw.ToArray();
            });
        }

        #endregion

        #region Team Bank

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamBank UpdateTeamBank(TeamBank teambank)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamBankRepository teambankRepository = _DataRepositoryFactory.GetDataRepository<ITeamBankRepository>();

                TeamBank updatedEntity = null;

                if (teambank.TeamBankId == 0)
                {

                    updatedEntity = teambankRepository.Add(teambank);
                }
                else
                    updatedEntity = teambankRepository.Update(teambank);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamBank(int teambankId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamBankRepository teambankRepository = _DataRepositoryFactory.GetDataRepository<ITeamBankRepository>();

                teambankRepository.Remove(teambankId);
            });
        }

        public TeamBank GetTeamBank(int teambankId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamBankRepository teambankRepository = _DataRepositoryFactory.GetDataRepository<ITeamBankRepository>();

                TeamBank teambankEntity = teambankRepository.Get(teambankId);
                if (teambankEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Team bank with ID of {0} is not in database", teambankId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return teambankEntity;
            });
        }
        public TeamBank[] GetAllTeamBanks()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamBankRepository teambankRepository = _DataRepositoryFactory.GetDataRepository<ITeamBankRepository>();

                IEnumerable<TeamBank> teambank = teambankRepository.Get();

                return teambank.ToArray();
            });
        }

        public TeamBank[] GetTeamBanksUsingParams(string searchvalue, int year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamBankRepository teambankRepository = _DataRepositoryFactory.GetDataRepository<ITeamBankRepository>();

                IEnumerable<TeamBank> teambank = teambankRepository.GetTeamBanksBySearchValue(searchvalue, year);

                return teambank.ToArray();
            });
        }

        public TeamBank[] GetTeamBankUsingDefinitionCode(string code)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamBankRepository teambankRepository = _DataRepositoryFactory.GetDataRepository<ITeamBankRepository>();

                IEnumerable<TeamBank> teambank = teambankRepository.GetTeamBankItemsByYear(code);

                return teambank.ToArray();
            });
        }

        #endregion


        #region ScoreCard Metrics KBL

        [OperationBehavior(TransactionScopeRequired = true)]
        public ScoreCardMetricsKBL UpdateScoreCardMetricsKBL(ScoreCardMetricsKBL scorecardmetricsKBL)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMetricsKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsKBLRepository>();

                ScoreCardMetricsKBL updatedEntity = null;

                if (scorecardmetricsKBL.MetricID == 0)
                {
                    updatedEntity = scmRepository.Add(scorecardmetricsKBL);
                }
                else
                    updatedEntity = scmRepository.Update(scorecardmetricsKBL);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteScoreCardMetricsKBL(int MetricID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMetricsKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsKBLRepository>();

                scmRepository.Remove(MetricID);
            });
        }

        public ScoreCardMetricsKBL GetScoreCardMetricsKBL(int MetricID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMetricsKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsKBLRepository>();

                ScoreCardMetricsKBL scmEntity = scmRepository.Get(MetricID);
                if (scmEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Scorecard metrics with ID of {0} is not in database", MetricID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scmEntity;
            });
        }
        public ScoreCardMetricsKBL[] GetAllScoreCardMetricsKBL()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMetricsKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsKBLRepository>();

                IEnumerable<ScoreCardMetricsKBL> scm = scmRepository.Get();

                return scm.ToArray();
            });
        }


        public ScoreCardMetricsKBL[] GetScoreCardMetricsKBLUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMetricsKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsKBLRepository>();

                //List<ScoreCardMetricsKBL> scw = new List<ScoreCardMetricsKBL>();

                IEnumerable<ScoreCardMetricsKBL> scw = scmRepository.GetMetricsBySearchValue(searchvalue);
              
                return scw.ToArray();
            });
        }

        public ScoreCardMetricsKBL[] GetScoreCardMetricsKBLUsingYear(int year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMetricsKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMetricsKBLRepository>();

                //List<ScoreCardMetricsKBL> scw = new List<ScoreCardMetricsKBL>();

                IEnumerable<ScoreCardMetricsKBL> scw = scmRepository.GetMetricsByYear(year);

                return scw.ToArray();
            });
        }

        #endregion

        #region ScoreCard KPI Types KBL

        [OperationBehavior(TransactionScopeRequired = true)]
        public ScoreCardKPITypesKBL UpdateScoreCardKPITypesKBL(ScoreCardKPITypesKBL scorecardKPItypesKBL)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardKPITypesKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardKPITypesKBLRepository>();

                ScoreCardKPITypesKBL updatedEntity = null;

                if (scorecardKPItypesKBL.ID == 0)
                {
                    updatedEntity = scmRepository.Add(scorecardKPItypesKBL);
                }
                else
                    updatedEntity = scmRepository.Update(scorecardKPItypesKBL);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteScoreCardKPITypesKBL(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardKPITypesKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardKPITypesKBLRepository>();

                scmRepository.Remove(ID);
            });
        }

        public ScoreCardKPITypesKBL GetScoreCardKPITypesKBL(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardKPITypesKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardKPITypesKBLRepository>();

                ScoreCardKPITypesKBL scmEntity = scmRepository.Get(ID);
                if (scmEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Scorecard kpi with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scmEntity;
            });
        }
        public ScoreCardKPITypesKBL[] GetAllScoreCardKPITypesKBL()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardKPITypesKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardKPITypesKBLRepository>();

                IEnumerable<ScoreCardKPITypesKBL> scm = scmRepository.Get();

                return scm.ToArray();
            });
        }


        public ScoreCardKPITypesKBL[] GetScoreCardKPITypesKBLUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardKPITypesKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardKPITypesKBLRepository>();

                //List<ScoreCardMetricsKBL> scw = new List<ScoreCardMetricsKBL>();

                IEnumerable<ScoreCardKPITypesKBL> scw = scmRepository.GetScoreCardKPITypesKBLBySearchValue(searchvalue);

                return scw.ToArray();
            });
        }

        public ScoreCardKPITypesKBL[] GetScoreCardKPITypesKBLByPeriodYearKPIType(int period, int year, string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardKPITypesKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardKPITypesKBLRepository>();

                //List<ScoreCardMetricsKBL> scw = new List<ScoreCardMetricsKBL>();

                IEnumerable<ScoreCardKPITypesKBL> scw = scmRepository.GetScoreCardKPITypesKBLByPeriodYearKPIType(period, year, searchvalue);

                return scw.ToArray();
            });
        }

        #endregion

        #region ScoreCard Set Metric Target KBL

        [OperationBehavior(TransactionScopeRequired = true)]
        public ScoreCardSetMetricTargetKBL UpdateScoreCardSetMetricTargetKBL(ScoreCardSetMetricTargetKBL scoreCcardsetmetrictarget)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardSetMetricTargetKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardSetMetricTargetKBLRepository>();

                ScoreCardSetMetricTargetKBL updatedEntity = null;

                if (scoreCcardsetmetrictarget.ID == 0)
                {
                    updatedEntity = scmRepository.Add(scoreCcardsetmetrictarget);
                }
                else
                    updatedEntity = scmRepository.Update(scoreCcardsetmetrictarget);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteScoreCardSetMetricTargetKBL(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardSetMetricTargetKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardSetMetricTargetKBLRepository>();

                scmRepository.Remove(ID);
            });
        }

        public ScoreCardSetMetricTargetKBL GetScoreCardSetMetricTargetKBL(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardSetMetricTargetKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardSetMetricTargetKBLRepository>();

                ScoreCardSetMetricTargetKBL scmEntity = scmRepository.Get(ID);
                if (scmEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ScoreCardS etMetricTarget with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scmEntity;
            });
        }
        public ScoreCardSetMetricTargetKBL[] GetAllScoreCardSetMetricTargetKBL()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardSetMetricTargetKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardSetMetricTargetKBLRepository>();

                IEnumerable<ScoreCardSetMetricTargetKBL> scm = scmRepository.Get();

                return scm.ToArray();
            });
        }


        public ScoreCardSetMetricTargetKBL[] GetScoreCardSetMetricTargetKBLUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardSetMetricTargetKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardSetMetricTargetKBLRepository>();

                //List<ScoreCardMetricsKBL> scw = new List<ScoreCardMetricsKBL>();

                IEnumerable<ScoreCardSetMetricTargetKBL> scw = scmRepository.GetScoreCardSetMetricTargetKBLBySearchValue(searchvalue);

                return scw.ToArray();
            });
        }

        public ScoreCardSetMetricTargetKBL[] GetScoreCardSetMetricTargetKBLByPeriodANDYear(int period, int year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardSetMetricTargetKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardSetMetricTargetKBLRepository>();

                //List<ScoreCardMetricsKBL> scw = new List<ScoreCardMetricsKBL>();

                IEnumerable<ScoreCardSetMetricTargetKBL> scw = scmRepository.GetScoreCardSetMetricTargetKBLByPeriodANDYear(period, year);

                return scw.ToArray();
            });
        }

        #endregion

        # region ScoreCard MIS Mapping KBL

        [OperationBehavior(TransactionScopeRequired = true)]
        public ScoreCardMISMappingKBL UpdateScoreCardMISMappingKBL(ScoreCardMISMappingKBL scorecardMISmapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMISMappingKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMISMappingKBLRepository>();

                ScoreCardMISMappingKBL updatedEntity = null;

                if (scorecardMISmapping.ID == 0)
                {
                    updatedEntity = scmRepository.Add(scorecardMISmapping);
                }
                else
                    updatedEntity = scmRepository.Update(scorecardMISmapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteScoreCardMISMappingKBL(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMISMappingKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMISMappingKBLRepository>();

                scmRepository.Remove(ID);
            });
        }

        public ScoreCardMISMappingKBL GetScoreCardMISMappingKBL(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMISMappingKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMISMappingKBLRepository>();

                ScoreCardMISMappingKBL scmEntity = scmRepository.Get(ID);
                if (scmEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("ScoreCard MIS Mapping with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return scmEntity;
            });
        }
        public ScoreCardMISMappingKBL[] GetAllScoreCardMISMappingKBL()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMISMappingKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMISMappingKBLRepository>();

                IEnumerable<ScoreCardMISMappingKBL> scm = scmRepository.Get();

                return scm.ToArray();
            });
        }


        public ScoreCardMISMappingKBL[] GetScoreCardMISMappingKBLUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IScoreCardMISMappingKBLRepository scmRepository = _DataRepositoryFactory.GetDataRepository<IScoreCardMISMappingKBLRepository>();

                //List<ScoreCardMetricsKBL> scw = new List<ScoreCardMetricsKBL>();

                IEnumerable<ScoreCardMISMappingKBL> scw = scmRepository.GetScoreCardMISMappingKBLBySearchValue(searchvalue);

                return scw.ToArray();
            });
        }

        #endregion

        # region Income Cash Vault Schedule

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeCashVaultSchedule UpdateIncomeCashVaultSchedule(IncomeCashVaultSchedule incomecashvaultschedule)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCashVaultScheduleRepository icsRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCashVaultScheduleRepository>();

                IncomeCashVaultSchedule updatedEntity = null;

                if (incomecashvaultschedule.ID == 0)
                {
                    updatedEntity = icsRepository.Add(incomecashvaultschedule);
                }
                else
                    updatedEntity = icsRepository.Update(incomecashvaultschedule);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeCashVaultSchedule(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCashVaultScheduleRepository icsRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCashVaultScheduleRepository>();

                icsRepository.Remove(ID);
            });
        }

        public IncomeCashVaultSchedule GetIncomeCashVaultSchedule(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCashVaultScheduleRepository icsRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCashVaultScheduleRepository>();

                IncomeCashVaultSchedule icsEntity = icsRepository.Get(ID);
                if (icsEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Income CashVault Schedule with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return icsEntity;
            });
        }
        public IncomeCashVaultSchedule[] GetAllIncomeCashVaultScheduleL()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCashVaultScheduleRepository icsRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCashVaultScheduleRepository>();

                IEnumerable<IncomeCashVaultSchedule> ics = icsRepository.Get();

                return ics.ToArray();
            });
        }

        #endregion

        # region Income Setup

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeSetup UpdateIncomeSetup(IncomeSetup incomesetup)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeSetupRepository isRepository = _DataRepositoryFactory.GetDataRepository<IIncomeSetupRepository>();

                IncomeSetup updatedEntity = null;

                if (incomesetup.ID == 0)
                {
                    updatedEntity = isRepository.Add(incomesetup);
                }
                else
                    updatedEntity = isRepository.Update(incomesetup);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeSetup(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeSetupRepository isRepository = _DataRepositoryFactory.GetDataRepository<IIncomeSetupRepository>();

                isRepository.Remove(ID);
            });
        }

        public IncomeSetup GetIncomeSetup(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeSetupRepository isRepository = _DataRepositoryFactory.GetDataRepository<IIncomeSetupRepository>();

                IncomeSetup isEntity = isRepository.Get(ID);
                if (isEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Income setup with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return isEntity;
            });
        }
        public IncomeSetup[] GetAllIncomeSetup()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeSetupRepository isRepository = _DataRepositoryFactory.GetDataRepository<IIncomeSetupRepository>();

                IEnumerable<IncomeSetup> ics = isRepository.Get();

                return ics.ToArray();
            });
        }

        public IncomeSetup GetLatestIncomeSetup()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeSetupRepository isRepository = _DataRepositoryFactory.GetDataRepository<IIncomeSetupRepository>();

                IncomeSetup ics = isRepository.LatestIncomeSetUp();

                return ics;
            });
        }

        #endregion

        # region Slary Schedule

        [OperationBehavior(TransactionScopeRequired = true)]
        public SlarySchedule UpdateSlarySchedule(SlarySchedule slaryschedule)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISlaryScheduleRepository isRepository = _DataRepositoryFactory.GetDataRepository<ISlaryScheduleRepository>();

                SlarySchedule updatedEntity = null;

                if (slaryschedule.ID == 0)
                {
                    updatedEntity = isRepository.Add(slaryschedule);
                }
                else
                    updatedEntity = isRepository.Update(slaryschedule);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteSlarySchedule(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISlaryScheduleRepository isRepository = _DataRepositoryFactory.GetDataRepository<ISlaryScheduleRepository>();

                isRepository.Remove(ID);
            });
        }

        public SlarySchedule GetSlarySchedule(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISlaryScheduleRepository isRepository = _DataRepositoryFactory.GetDataRepository<ISlaryScheduleRepository>();

                SlarySchedule isEntity = isRepository.Get(ID);
                if (isEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Income setup with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return isEntity;
            });
        }
        public SlarySchedule[] GetAllSlarySchedule()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ISlaryScheduleRepository isRepository = _DataRepositoryFactory.GetDataRepository<ISlaryScheduleRepository>();

                IEnumerable<SlarySchedule> ics = isRepository.Get();

                return ics.ToArray();
            });
        }

        #endregion

        # region Income Other Breakdown

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeOtherBreakdown UpdateIncomeOtherBreakdown(IncomeOtherBreakdown iob)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeOtherBreakdownRepository isRepository = _DataRepositoryFactory.GetDataRepository<IIncomeOtherBreakdownRepository>();

                IncomeOtherBreakdown updatedEntity = null;

                if (iob.ID == 0)
                {
                    updatedEntity = isRepository.Add(iob);
                }
                else
                    updatedEntity = isRepository.Update(iob);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeOtherBreakdown(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeOtherBreakdownRepository isRepository = _DataRepositoryFactory.GetDataRepository<IIncomeOtherBreakdownRepository>();

                isRepository.Remove(ID);
            });
        }

        public IncomeOtherBreakdown GetIncomeOtherBreakdown(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeOtherBreakdownRepository isRepository = _DataRepositoryFactory.GetDataRepository<IIncomeOtherBreakdownRepository>();

                IncomeOtherBreakdown isEntity = isRepository.Get(ID);
                if (isEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Income other Breakdownwith ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return isEntity;
            });
        }
        public IncomeOtherBreakdown[] GetAllIncomeOtherBreakdown()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeOtherBreakdownRepository isRepository = _DataRepositoryFactory.GetDataRepository<IIncomeOtherBreakdownRepository>();

                IEnumerable<IncomeOtherBreakdown> ics = isRepository.Get();

                return ics.ToArray();
            });
        }

        #endregion

        # region Download Base Fintrak FinalManual

        [OperationBehavior(TransactionScopeRequired = true)]
        public DownloadBaseFintrakFinalManual UpdateDDBaseFFM(DownloadBaseFintrakFinalManual ddb)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IDownloadBaseFintrakFinalManualRepository isRepository = _DataRepositoryFactory.GetDataRepository<IDownloadBaseFintrakFinalManualRepository>();

                DownloadBaseFintrakFinalManual updatedEntity = null;

                if (ddb.ID == 0)
                {
                    updatedEntity = isRepository.Add(ddb);
                }
                else
                    updatedEntity = isRepository.Update(ddb);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteDDBaseFFM(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IDownloadBaseFintrakFinalManualRepository isRepository = _DataRepositoryFactory.GetDataRepository<IDownloadBaseFintrakFinalManualRepository>();

                isRepository.Remove(ID);
            });
        }

        public DownloadBaseFintrakFinalManual GetDDBaseFFM(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IDownloadBaseFintrakFinalManualRepository isRepository = _DataRepositoryFactory.GetDataRepository<IDownloadBaseFintrakFinalManualRepository>();

                DownloadBaseFintrakFinalManual isEntity = isRepository.Get(ID);
                if (isEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Download base ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return isEntity;
            });
        }
        public DownloadBaseFintrakFinalManual[] GetAllDDBaseFFM()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IDownloadBaseFintrakFinalManualRepository isRepository = _DataRepositoryFactory.GetDataRepository<IDownloadBaseFintrakFinalManualRepository>();

                IEnumerable<DownloadBaseFintrakFinalManual> ics = isRepository.Get();

                return ics.ToArray();
            });
        }

        #endregion

        # region MPRReportStatus

        [OperationBehavior(TransactionScopeRequired = true)]
        public MPRReportStatus UpdateMPRReportStatus(MPRReportStatus mprreportstatus)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRReportStatusRepository isRepository = _DataRepositoryFactory.GetDataRepository<IMPRReportStatusRepository>();

                MPRReportStatus updatedEntity = null;

                if (mprreportstatus.MPRReportStatusId == 0)
                {
                    updatedEntity = isRepository.Add(mprreportstatus);
                }
                else
                    updatedEntity = isRepository.Update(mprreportstatus);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMPRReportStatus(int MPRReportStatusId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRReportStatusRepository isRepository = _DataRepositoryFactory.GetDataRepository<IMPRReportStatusRepository>();

                isRepository.Remove(MPRReportStatusId);
            });
        }

        public MPRReportStatus GetMPRReportStatus(int MPRReportStatusId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRReportStatusRepository isRepository = _DataRepositoryFactory.GetDataRepository<IMPRReportStatusRepository>();

                MPRReportStatus isEntity = isRepository.Get(MPRReportStatusId);
                if (isEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Download base ID of {0} is not in database", MPRReportStatusId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return isEntity;
            });
        }
        public MPRReportStatus[] GetAllMPRReportStatus()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMPRReportStatusRepository isRepository = _DataRepositoryFactory.GetDataRepository<IMPRReportStatusRepository>();

                IEnumerable<MPRReportStatus> ics = isRepository.Get();

                return ics.ToArray();
            });
        }

        #endregion

        #region FinstatMapping operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public FinstatMapping UpdateFinstatMapping(FinstatMapping finstatMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFinstatMappingRepository finstatMappingRepository = _DataRepositoryFactory.GetDataRepository<IFinstatMappingRepository>();

                FinstatMapping updatedEntity = null;

                if (finstatMapping.FinstatMappingId == 0)
                {

                    updatedEntity = finstatMappingRepository.Add(finstatMapping);
                }
                else
                    updatedEntity = finstatMappingRepository.Update(finstatMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFinstatMapping(int finstatMappingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFinstatMappingRepository finstatMappingRepository = _DataRepositoryFactory.GetDataRepository<IFinstatMappingRepository>();

                finstatMappingRepository.Remove(finstatMappingId);
            });
        }

        public FinstatMapping GetFinstatMapping(int finstatMappingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFinstatMappingRepository finstatMappingRepository = _DataRepositoryFactory.GetDataRepository<IFinstatMappingRepository>();

                FinstatMapping finstatMappingEntity = finstatMappingRepository.Get(finstatMappingId);
                if (finstatMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FinstatMapping with ID of {0} is not in database", finstatMappingId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return finstatMappingEntity;
            });
        }

        public FinstatMapping[] GetAllFinstatMapping()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFinstatMappingRepository finstatMappingRepository = _DataRepositoryFactory.GetDataRepository<IFinstatMappingRepository>();


                IEnumerable<FinstatMapping> finstatMapping = finstatMappingRepository.Get().ToArray();

                return finstatMapping.ToArray();
            });
        }

        #endregion

        #region IncomeAccountsTreeMisCodes

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeAccountsTreeMisCodes UpdateIncomeAccountsTreeMisCodes(IncomeAccountsTreeMisCodes incomeAccountsTreeMisCodes)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeMisCodesRepository incomeAccountsTreeMisCodesRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeMisCodesRepository>();

                IncomeAccountsTreeMisCodes updatedEntity = null;

                if (incomeAccountsTreeMisCodes.ID == 0)
                {

                    updatedEntity = incomeAccountsTreeMisCodesRepository.Add(incomeAccountsTreeMisCodes);
                }
                else
                    updatedEntity = incomeAccountsTreeMisCodesRepository.Update(incomeAccountsTreeMisCodes);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeAccountsTreeMisCodes(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeMisCodesRepository incomeAccountsTreeMisCodesRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeMisCodesRepository>();

                incomeAccountsTreeMisCodesRepository.Remove(ID);
            });
        }

        public IncomeAccountsTreeMisCodes GetIncomeAccountsTreeMisCodes(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeMisCodesRepository incomeAccountsTreeMisCodesRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeMisCodesRepository>();

                IncomeAccountsTreeMisCodes incomeAccountsTreeMisCodesEntity = incomeAccountsTreeMisCodesRepository.Get(ID);
                if (incomeAccountsTreeMisCodesEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeAccountsTreeMisCodes with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeAccountsTreeMisCodesEntity;
            });
        }

        public IncomeAccountsTreeMisCodes[] GetAllIncomeAccountsTreeMisCodes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeMisCodesRepository incomeAccountsTreeMisCodesRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeMisCodesRepository>();


                IEnumerable<IncomeAccountsTreeMisCodes> incomeAccountsTreeMisCodes = incomeAccountsTreeMisCodesRepository.Get().ToArray();

                return incomeAccountsTreeMisCodes.ToArray();
            });
        }

        public IncomeAccountsTreeMisCodes[] GetByAccountNumber(string AccountNumber)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeMisCodesRepository incomeAccountsTreeMisCodesRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeMisCodesRepository>();

                IEnumerable<IncomeAccountsTreeMisCodes> incomeAccountsTreeMisCodes = incomeAccountsTreeMisCodesRepository.GetByAccountNumber(AccountNumber);

                return incomeAccountsTreeMisCodes.ToArray();
            });
        }

        #endregion

        #region IncomeAccountsTreeAccount

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeAccountsTreeAccount UpdateIncomeAccountsTreeAccount(IncomeAccountsTreeAccount incomeAccountsTreeAccount)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeAccountRepository incomeAccountsTreeAccountRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeAccountRepository>();

                IncomeAccountsTreeAccount updatedEntity = null;

                if (incomeAccountsTreeAccount.ID == 0)
                {

                    updatedEntity = incomeAccountsTreeAccountRepository.Add(incomeAccountsTreeAccount);
                }
                else
                    updatedEntity = incomeAccountsTreeAccountRepository.Update(incomeAccountsTreeAccount);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeAccountsTreeAccount(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeAccountRepository incomeAccountsTreeAccountRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeAccountRepository>();

                incomeAccountsTreeAccountRepository.Remove(ID);
            });
        }

        public IncomeAccountsTreeAccount GetIncomeAccountsTreeAccount(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeAccountRepository incomeAccountsTreeAccountRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeAccountRepository>();

                IncomeAccountsTreeAccount incomeAccountsTreeAccountEntity = incomeAccountsTreeAccountRepository.Get(ID);
                if (incomeAccountsTreeAccountEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeAccountsTreeAccount with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeAccountsTreeAccountEntity;
            });
        }

        public IncomeAccountsTreeAccount[] GetAllIncomeAccountsTreeAccount()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeAccountRepository incomeAccountsTreeAccountRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeAccountRepository>();


                IEnumerable<IncomeAccountsTreeAccount> incomeAccountsTreeAccount = incomeAccountsTreeAccountRepository.Get().ToArray();

                return incomeAccountsTreeAccount.ToArray();
            });
        }

        public IncomeAccountsTreeAccount[] FilterByAccountNumber(string AccountNumber)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeAccountRepository incomeAccountsTreeAccountRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeAccountRepository>();

                IEnumerable<IncomeAccountsTreeAccount> incomeAccountsTreeAccount = incomeAccountsTreeAccountRepository.FilterByAccountNumber(AccountNumber);

                return incomeAccountsTreeAccount.ToArray();
            });
        }

        #endregion

        #region IncomePoolRateSbu

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomePoolRateSbu UpdateIncomePoolRateSbu(IncomePoolRateSbu incomePoolRateSbu)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomePoolRateSbuRepository incomePoolRateSbuRepository = _DataRepositoryFactory.GetDataRepository<IIncomePoolRateSbuRepository>();

                IncomePoolRateSbu updatedEntity = null;

                if (incomePoolRateSbu.ID == 0)
                {

                    updatedEntity = incomePoolRateSbuRepository.Add(incomePoolRateSbu);
                }
                else
                    updatedEntity = incomePoolRateSbuRepository.Update(incomePoolRateSbu);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomePoolRateSbu(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomePoolRateSbuRepository incomePoolRateSbuRepository = _DataRepositoryFactory.GetDataRepository<IIncomePoolRateSbuRepository>();

                incomePoolRateSbuRepository.Remove(ID);
            });
        }

        public IncomePoolRateSbu GetIncomePoolRateSbu(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomePoolRateSbuRepository incomePoolRateSbuRepository = _DataRepositoryFactory.GetDataRepository<IIncomePoolRateSbuRepository>();

                IncomePoolRateSbu incomePoolRateSbuEntity = incomePoolRateSbuRepository.Get(ID);
                if (incomePoolRateSbuEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomePoolRateSbu with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomePoolRateSbuEntity;
            });
        }

        public IncomePoolRateSbu[] GetAllIncomePoolRateSbu()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomePoolRateSbuRepository incomePoolRateSbuRepository = _DataRepositoryFactory.GetDataRepository<IIncomePoolRateSbuRepository>();


                IEnumerable<IncomePoolRateSbu> incomePoolRateSbu = incomePoolRateSbuRepository.Get().ToArray();

                return incomePoolRateSbu.ToArray();
            });
        }

        #endregion

        #region IncomePoolRateSbuYear

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomePoolRateSbuYear UpdateIncomePoolRateSbuYear(IncomePoolRateSbuYear incomePoolRateSbuYear)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomePoolRateSbuYearRepository incomePoolRateSbuYearRepository = _DataRepositoryFactory.GetDataRepository<IIncomePoolRateSbuYearRepository>();

                IncomePoolRateSbuYear updatedEntity = null;

                if (incomePoolRateSbuYear.ID == 0)
                {

                    updatedEntity = incomePoolRateSbuYearRepository.Add(incomePoolRateSbuYear);
                }
                else
                    updatedEntity = incomePoolRateSbuYearRepository.Update(incomePoolRateSbuYear);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomePoolRateSbuYear(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomePoolRateSbuYearRepository incomePoolRateSbuYearRepository = _DataRepositoryFactory.GetDataRepository<IIncomePoolRateSbuYearRepository>();

                incomePoolRateSbuYearRepository.Remove(ID);
            });
        }

        public IncomePoolRateSbuYear GetIncomePoolRateSbuYear(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomePoolRateSbuYearRepository incomePoolRateSbuYearRepository = _DataRepositoryFactory.GetDataRepository<IIncomePoolRateSbuYearRepository>();

                IncomePoolRateSbuYear incomePoolRateSbuYearEntity = incomePoolRateSbuYearRepository.Get(ID);
                if (incomePoolRateSbuYearEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomePoolRateSbuYear with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomePoolRateSbuYearEntity;
            });
        }

        public IncomePoolRateSbuYear[] GetAllIncomePoolRateSbuYear()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomePoolRateSbuYearRepository incomePoolRateSbuYearRepository = _DataRepositoryFactory.GetDataRepository<IIncomePoolRateSbuYearRepository>();


                IEnumerable<IncomePoolRateSbuYear> incomePoolRateSbuYear = incomePoolRateSbuYearRepository.Get().ToArray();

                return incomePoolRateSbuYear.ToArray();
            });
        }

        #endregion

        #region IncomeAccountsFintrak

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeAccountsFintrak UpdateIncomeAccountsFintrak(IncomeAccountsFintrak incomeAccountsFintrak)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsFintrakRepository incomeAccountsFintrakRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsFintrakRepository>();

                IncomeAccountsFintrak updatedEntity = null;

                if (incomeAccountsFintrak.ID == 0)
                {

                    updatedEntity = incomeAccountsFintrakRepository.Add(incomeAccountsFintrak);
                }
                else
                    updatedEntity = incomeAccountsFintrakRepository.Update(incomeAccountsFintrak);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeAccountsFintrak(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsFintrakRepository incomeAccountsFintrakRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsFintrakRepository>();

                incomeAccountsFintrakRepository.Remove(ID);
            });
        }

        public IncomeAccountsFintrak GetIncomeAccountsFintrak(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsFintrakRepository incomeAccountsFintrakRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsFintrakRepository>();

                IncomeAccountsFintrak incomeAccountsFintrakEntity = incomeAccountsFintrakRepository.Get(ID);
                if (incomeAccountsFintrakEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeAccountsFintrak with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeAccountsFintrakEntity;
            });
        }

        public IncomeAccountsFintrak[] GetAllIncomeAccountsFintrak()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsFintrakRepository incomeAccountsFintrakRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsFintrakRepository>();


                IEnumerable<IncomeAccountsFintrak> incomeAccountsFintrak = incomeAccountsFintrakRepository.Get().ToArray();

                return incomeAccountsFintrak.ToArray();
            });
        }

        #endregion

        #region IncomeAccountsNpl

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeAccountsNpl UpdateIncomeAccountsNpl(IncomeAccountsNpl incomeAccountsNpl)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsNplRepository incomeAccountsNplRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsNplRepository>();

                IncomeAccountsNpl updatedEntity = null;

                if (incomeAccountsNpl.ID == 0)
                {

                    updatedEntity = incomeAccountsNplRepository.Add(incomeAccountsNpl);
                }
                else
                    updatedEntity = incomeAccountsNplRepository.Update(incomeAccountsNpl);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeAccountsNpl(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsNplRepository incomeAccountsNplRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsNplRepository>();

                incomeAccountsNplRepository.Remove(ID);
            });
        }

        public IncomeAccountsNpl GetIncomeAccountsNpl(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsNplRepository incomeAccountsNplRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsNplRepository>();

                IncomeAccountsNpl incomeAccountsNplEntity = incomeAccountsNplRepository.Get(ID);
                if (incomeAccountsNplEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeAccountsNpl with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeAccountsNplEntity;
            });
        }

        public IncomeAccountsNpl[] GetAllIncomeAccountsNpl()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsNplRepository incomeAccountsNplRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsNplRepository>();


                IEnumerable<IncomeAccountsNpl> incomeAccountsNpl = incomeAccountsNplRepository.Get().ToArray();

                return incomeAccountsNpl.ToArray();
            });
        }

        public IncomeAccountsNplData[] GetAllIncomeAccountsCustomers()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsNplRepository acRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsNplRepository>();

                List<IncomeAccountsNplData> scw = new List<IncomeAccountsNplData>();

                IEnumerable<IncomeAccountsNplInfo> scw2 = acRepository.GetIncomeAccountsCustomers();

                foreach (var w in scw2)
                {
                    var dr = new IncomeAccountsNplData()
                    {
                        AccountNumber = w.AccountNumber,
                        CustomerName = w.CustomerName,
                    };
                    scw.Add(dr);
                }

                return scw.ToArray();
            });
        }

        #endregion


        #region IncomeCommFeeMis

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeCommFeeMis UpdateIncomeCommFeeMis(IncomeCommFeeMis incomeCommFeeMis)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCommFeeMisRepository incomeCommFeeMisRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCommFeeMisRepository>();

                IncomeCommFeeMis updatedEntity = null;

                if (incomeCommFeeMis.ID == 0)
                {

                    updatedEntity = incomeCommFeeMisRepository.Add(incomeCommFeeMis);
                }
                else
                    updatedEntity = incomeCommFeeMisRepository.Update(incomeCommFeeMis);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeCommFeeMis(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCommFeeMisRepository incomeCommFeeMisRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCommFeeMisRepository>();

                incomeCommFeeMisRepository.Remove(ID);
            });
        }

        public IncomeCommFeeMis GetIncomeCommFeeMis(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCommFeeMisRepository incomeCommFeeMisRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCommFeeMisRepository>();

                IncomeCommFeeMis incomeCommFeeMisEntity = incomeCommFeeMisRepository.Get(ID);
                if (incomeCommFeeMisEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeCommFeeMis with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeCommFeeMisEntity;
            });
        }

        public IncomeCommFeeMis[] GetAllIncomeCommFeeMis()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCommFeeMisRepository incomeCommFeeMisRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCommFeeMisRepository>();


                IEnumerable<IncomeCommFeeMis> incomeCommFeeMis = incomeCommFeeMisRepository.Get().ToArray();

                return incomeCommFeeMis.ToArray();
            });
        }

        #endregion


        #region IncomeMisCodes

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeMisCodes UpdateIncomeMisCodes(IncomeMisCodes incomeMisCodes)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeMisCodesRepository incomeMisCodesRepository = _DataRepositoryFactory.GetDataRepository<IIncomeMisCodesRepository>();

                IncomeMisCodes updatedEntity = null;

                if (incomeMisCodes.ID == 0)
                {

                    updatedEntity = incomeMisCodesRepository.Add(incomeMisCodes);
                }
                else
                    updatedEntity = incomeMisCodesRepository.Update(incomeMisCodes);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeMisCodes(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeMisCodesRepository incomeMisCodesRepository = _DataRepositoryFactory.GetDataRepository<IIncomeMisCodesRepository>();

                incomeMisCodesRepository.Remove(ID);
            });
        }

        public IncomeMisCodes GetIncomeMisCodes(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeMisCodesRepository incomeMisCodesRepository = _DataRepositoryFactory.GetDataRepository<IIncomeMisCodesRepository>();

                IncomeMisCodes incomeMisCodesEntity = incomeMisCodesRepository.Get(ID);
                if (incomeMisCodesEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeMisCodes with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeMisCodesEntity;
            });
        }

        public IncomeMisCodes[] GetAllIncomeMisCodes()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeMisCodesRepository incomeMisCodesRepository = _DataRepositoryFactory.GetDataRepository<IIncomeMisCodesRepository>();


                IEnumerable<IncomeMisCodes> incomeMisCodes = incomeMisCodesRepository.Get().ToArray();

                return incomeMisCodes.ToArray();
            });
        }

        #endregion

        #region IncomeCurrency

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeCurrency UpdateIncomeCurrency(IncomeCurrency incomeCurrency)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCurrencyRepository incomeCurrencyRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCurrencyRepository>();

                IncomeCurrency updatedEntity = null;

                if (incomeCurrency.ID == 0)
                {

                    updatedEntity = incomeCurrencyRepository.Add(incomeCurrency);
                }
                else
                    updatedEntity = incomeCurrencyRepository.Update(incomeCurrency);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeCurrency(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCurrencyRepository incomeCurrencyRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCurrencyRepository>();

                incomeCurrencyRepository.Remove(ID);
            });
        }

        public IncomeCurrency GetIncomeCurrency(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCurrencyRepository incomeCurrencyRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCurrencyRepository>();

                IncomeCurrency incomeCurrencyEntity = incomeCurrencyRepository.Get(ID);
                if (incomeCurrencyEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeCurrency with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeCurrencyEntity;
            });
        }

        public IncomeCurrency[] GetAllIncomeCurrency()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCurrencyRepository incomeCurrencyRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCurrencyRepository>();


                IEnumerable<IncomeCurrency> incomeCurrency = incomeCurrencyRepository.Get().ToArray();

                return incomeCurrency.ToArray();
            });
        }

        #endregion

        #region IncomeMemorep

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeMemorep UpdateIncomeMemorep(IncomeMemorep incomeMemorep)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeMemorepRepository incomeMemorepRepository = _DataRepositoryFactory.GetDataRepository<IIncomeMemorepRepository>();

                IncomeMemorep updatedEntity = null;

                if (incomeMemorep.ID == 0)
                {

                    updatedEntity = incomeMemorepRepository.Add(incomeMemorep);
                }
                else
                    updatedEntity = incomeMemorepRepository.Update(incomeMemorep);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeMemorep(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeMemorepRepository incomeMemorepRepository = _DataRepositoryFactory.GetDataRepository<IIncomeMemorepRepository>();

                incomeMemorepRepository.Remove(ID);
            });
        }

        public IncomeMemorep GetIncomeMemorep(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeMemorepRepository incomeMemorepRepository = _DataRepositoryFactory.GetDataRepository<IIncomeMemorepRepository>();

                IncomeMemorep incomeMemorepEntity = incomeMemorepRepository.Get(ID);
                if (incomeMemorepEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeMemorep with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeMemorepEntity;
            });
        }

        public IncomeMemorep[] GetAllIncomeMemorep()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeMemorepRepository incomeMemorepRepository = _DataRepositoryFactory.GetDataRepository<IIncomeMemorepRepository>();


                IEnumerable<IncomeMemorep> incomeMemorep = incomeMemorepRepository.Get().ToArray();

                return incomeMemorep.ToArray();
            });
        }

        #endregion

        #region IncomeSplitPoolsRate

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeSplitPoolsRate UpdateIncomeSplitPoolsRate(IncomeSplitPoolsRate incomeSplitPoolsRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeSplitPoolsRateRepository incomeSplitPoolsRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeSplitPoolsRateRepository>();

                IncomeSplitPoolsRate updatedEntity = null;

                if (incomeSplitPoolsRate.ID == 0)
                {

                    updatedEntity = incomeSplitPoolsRateRepository.Add(incomeSplitPoolsRate);
                }
                else
                    updatedEntity = incomeSplitPoolsRateRepository.Update(incomeSplitPoolsRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeSplitPoolsRate(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeSplitPoolsRateRepository incomeSplitPoolsRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeSplitPoolsRateRepository>();

                incomeSplitPoolsRateRepository.Remove(ID);
            });
        }

        public IncomeSplitPoolsRate GetIncomeSplitPoolsRate(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeSplitPoolsRateRepository incomeSplitPoolsRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeSplitPoolsRateRepository>();

                IncomeSplitPoolsRate incomeSplitPoolsRateEntity = incomeSplitPoolsRateRepository.Get(ID);
                if (incomeSplitPoolsRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeSplitPoolsRate with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeSplitPoolsRateEntity;
            });
        }

        public IncomeSplitPoolsRate[] GetAllIncomeSplitPoolsRate()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeSplitPoolsRateRepository incomeSplitPoolsRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeSplitPoolsRateRepository>();


                IEnumerable<IncomeSplitPoolsRate> incomeSplitPoolsRate = incomeSplitPoolsRateRepository.Get().ToArray();

                return incomeSplitPoolsRate.ToArray();
            });
        }

        #endregion

        #region IncomeAccountsUnit

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeAccountsUnit UpdateIncomeAccountsUnit(IncomeAccountsUnit incomeAccountsUnit)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsUnitRepository incomeAccountsUnitRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsUnitRepository>();

                IncomeAccountsUnit updatedEntity = null;

                if (incomeAccountsUnit.ID == 0)
                {

                    updatedEntity = incomeAccountsUnitRepository.Add(incomeAccountsUnit);
                }
                else
                    updatedEntity = incomeAccountsUnitRepository.Update(incomeAccountsUnit);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeAccountsUnit(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsUnitRepository incomeAccountsUnitRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsUnitRepository>();

                incomeAccountsUnitRepository.Remove(ID);
            });
        }

        public IncomeAccountsUnit GetIncomeAccountsUnit(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsUnitRepository incomeAccountsUnitRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsUnitRepository>();

                IncomeAccountsUnit incomeAccountsUnitEntity = incomeAccountsUnitRepository.Get(ID);
                if (incomeAccountsUnitEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeAccountsUnit with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeAccountsUnitEntity;
            });
        }

        public IncomeAccountsUnit[] GetAllIncomeAccountsUnit()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsUnitRepository incomeAccountsUnitRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsUnitRepository>();


                IEnumerable<IncomeAccountsUnit> incomeAccountsUnit = incomeAccountsUnitRepository.Get().ToArray();

                return incomeAccountsUnit.ToArray();
            });
        }

        #endregion

        #region Team StructureALL

        [OperationBehavior(TransactionScopeRequired = true)]
        public TeamStructureALL UpdateTeamStructureALL(TeamStructureALL TeamStructureALL)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository tsRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                TeamStructureALL updatedEntity = null;

                if (TeamStructureALL.Team_StructureId == 0)
                {

                    updatedEntity = tsRepository.Add(TeamStructureALL);
                }
                else
                    updatedEntity = tsRepository.Update(TeamStructureALL);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteTeamStructureALL(int Team_StructureId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository tsRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                tsRepository.Remove(Team_StructureId);
            });
        }

        public TeamStructureALL GetTeamStructureALL(int Team_StructureId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository tsRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                TeamStructureALL tsEntity = tsRepository.Get(Team_StructureId);
                if (tsEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Team structure with ID of {0} is not in database", Team_StructureId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return tsEntity;
            });
        }
        public TeamStructureALL[] GetAllTeamStructureALL()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository TeamStructureALLRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                IEnumerable<TeamStructureALL> TeamStructureALL = TeamStructureALLRepository.Get();

                return TeamStructureALL.ToArray();
            });
        }

        public TeamStructureALL[] GetTeamStructureALLUsingParams(string SearchValue, int year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository TeamStructureALLRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                IEnumerable<TeamStructureALL> TeamStructureALL = TeamStructureALLRepository.GetTeamStructureALLByParams(SearchValue, year);

                return TeamStructureALL.ToArray();
            });
        }

        public TeamStructureALL[] TeamStructureALLByParameters(string selectedDefinitionCode, string SearchValue, int year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository TeamStructureALLRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                IEnumerable<TeamStructureALL> TeamStructureALL = TeamStructureALLRepository.GetTeamStructureALLByParameters(selectedDefinitionCode, SearchValue, year);

                return TeamStructureALL.ToArray();
            });
        }

        public TeamStructureALL[] GetTeamStructureALLByParamsAndeSetUp(string code, string SearchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository TeamStructureALLRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                IEnumerable<TeamStructureALL> TeamStructureALL = TeamStructureALLRepository.GetTeamStructureALLByParamsAndeSetUp(code, SearchValue);

                return TeamStructureALL.ToArray();
            });
        }

        public TeamStructureALL[] GetTeamStructureALLUsingSetUp()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository TeamStructureALLRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                IEnumerable<TeamStructureALL> TeamStructureALL = TeamStructureALLRepository.GetTeamStructureALLBySetUp();

                return TeamStructureALL.ToArray();
            });
        }

        public TeamStructureALL[] GetTeamStructureALLUsingDefinitionCode(string code)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository TeamStructureALLRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                IEnumerable<TeamStructureALL> TeamStructureALL = TeamStructureALLRepository.GetTeamStructureALLByDefinitionCode(code);

                return TeamStructureALL.ToArray();
            });
        }

        public TeamStructureALL[] GetTeamStructureALLUsingDefinitionCodeMonthly(string code)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository TeamStructureALLRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                IEnumerable<TeamStructureALL> TeamStructureALL = TeamStructureALLRepository.GetTeamStructureALLByDefinitionCodeMonthly(code);

                return TeamStructureALL.ToArray();
            });
        }

        public TeamStructureALL[] GetTeamStructureALLByParamsAndeSetUpMonthly(string code, string SearchValue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository TeamStructureALLRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                IEnumerable<TeamStructureALL> TeamStructureALL = TeamStructureALLRepository.GetTeamStructureALLByParamsAndeSetUpMonthly(code, SearchValue);

                return TeamStructureALL.ToArray();
            });
        }

        public TeamStructureALL[] GetTeamStructureALLUsingSetUpMonthly()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository TeamStructureALLRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                IEnumerable<TeamStructureALL> TeamStructureALL = TeamStructureALLRepository.GetTeamStructureALLBySetUpMonthly();

                return TeamStructureALL.ToArray();
            });
        }

        public TeamStructureALL GetTeamStructureALLTop1(string branch, string defcode, int year)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS, GROUP_SUPER_BUSINESS, GROUP_USER_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                ITeamStructureALLRepository TeamStructureALLRepository = _DataRepositoryFactory.GetDataRepository<ITeamStructureALLRepository>();

                TeamStructureALL TeamStructureALL = TeamStructureALLRepository.GetTeamStructureALLTop1(branch, defcode, year);

                return TeamStructureALL;
            });
        }


        #endregion

        #region IncomeFintrakAccountsSegment operations

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeFintrakAccountsSegment UpdateIncomeFintrakAccountsSegment(IncomeFintrakAccountsSegment ifas)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeFintrakAccountsSegmentRepository ifasRepository = _DataRepositoryFactory.GetDataRepository<IIncomeFintrakAccountsSegmentRepository>();

                IncomeFintrakAccountsSegment updatedEntity = null;

                if (ifas.Id == 0)
                {

                    updatedEntity = ifasRepository.Add(ifas);
                }
                else
                    updatedEntity = ifasRepository.Update(ifas);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeFintrakAccountsSegment(int Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeFintrakAccountsSegmentRepository ifasRepository = _DataRepositoryFactory.GetDataRepository<IIncomeFintrakAccountsSegmentRepository>();

                ifasRepository.Remove(Id);
            });
        }

        public IncomeFintrakAccountsSegment GetIncomeFintrakAccountsSegment(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeFintrakAccountsSegmentRepository ifasRepository = _DataRepositoryFactory.GetDataRepository<IIncomeFintrakAccountsSegmentRepository>();

                IncomeFintrakAccountsSegment ifasEntity = ifasRepository.Get(Id);
                if (ifasEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeFintrakAccountsSegment with ID of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return ifasEntity;
            });
        }

        public IncomeFintrakAccountsSegment[] GetAllIncomeFintrakAccountsSegment()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeFintrakAccountsSegmentRepository ifasRepository = _DataRepositoryFactory.GetDataRepository<IIncomeFintrakAccountsSegmentRepository>();


                IEnumerable<IncomeFintrakAccountsSegment> ifas = ifasRepository.Get().ToArray();

                return ifas.ToArray();
            });
        }

        public IncomeFintrakAccountsSegment[] GetAccountsSegmentByCustomerIdBank(string customerId, string bank)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeFintrakAccountsSegmentRepository ifasRepository = _DataRepositoryFactory.GetDataRepository<IIncomeFintrakAccountsSegmentRepository>();


                IEnumerable<IncomeFintrakAccountsSegment> ifas = ifasRepository.GetAccountsSegmentByCustomerIdBank(customerId, bank).ToArray();

                return ifas.ToArray();
            });
        }

        #endregion

        #region FTPRiskRatings

        [OperationBehavior(TransactionScopeRequired = true)]
        public FTPRiskRatings UpdateFTPRiskRatings(FTPRiskRatings fTPRiskRatings)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFTPRiskRatingsRepository fTPRiskRatingsRepository = _DataRepositoryFactory.GetDataRepository<IFTPRiskRatingsRepository>();

                FTPRiskRatings updatedEntity = null;

                if (fTPRiskRatings.ID == 0)
                {

                    updatedEntity = fTPRiskRatingsRepository.Add(fTPRiskRatings);
                }
                else
                    updatedEntity = fTPRiskRatingsRepository.Update(fTPRiskRatings);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteFTPRiskRatings(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFTPRiskRatingsRepository fTPRiskRatingsRepository = _DataRepositoryFactory.GetDataRepository<IFTPRiskRatingsRepository>();

                fTPRiskRatingsRepository.Remove(ID);
            });
        }

        public FTPRiskRatings GetFTPRiskRatings(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFTPRiskRatingsRepository fTPRiskRatingsRepository = _DataRepositoryFactory.GetDataRepository<IFTPRiskRatingsRepository>();

                FTPRiskRatings fTPRiskRatingsEntity = fTPRiskRatingsRepository.Get(ID);
                if (fTPRiskRatingsEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("FTPRiskRatings with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return fTPRiskRatingsEntity;
            });
        }

        public FTPRiskRatings[] GetAllFTPRiskRatings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFTPRiskRatingsRepository fTPRiskRatingsRepository = _DataRepositoryFactory.GetDataRepository<IFTPRiskRatingsRepository>();


                IEnumerable<FTPRiskRatings> fTPRiskRatings = fTPRiskRatingsRepository.Get().ToArray();

                return fTPRiskRatings.ToArray();
            });
        }

        public FTPRiskRatings[] GetFTPRiskRatingsUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IFTPRiskRatingsRepository fTPRiskRatingsRepository = _DataRepositoryFactory.GetDataRepository<IFTPRiskRatingsRepository>();

                IEnumerable<FTPRiskRatings> fTPRiskRatings = fTPRiskRatingsRepository.GetFTPRiskRatingsBySearchValue(searchvalue);



                return fTPRiskRatings.ToArray();
            });
        }

        #endregion

        #region IncomeCaptionPoolRate

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeCaptionPoolRate UpdateIncomeCaptionPoolRate(IncomeCaptionPoolRate incomeCaptionPoolRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCaptionPoolRateRepository incomeCaptionPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCaptionPoolRateRepository>();

                IncomeCaptionPoolRate updatedEntity = null;

                if (incomeCaptionPoolRate.ID == 0)
                {
                    if (incomeCaptionPoolRateRepository.ValidateIncomeCaptionPoolRate(incomeCaptionPoolRate.Caption, incomeCaptionPoolRate.Year) == null)
                        updatedEntity = incomeCaptionPoolRateRepository.Add(incomeCaptionPoolRate);
                }
                else
                    updatedEntity = incomeCaptionPoolRateRepository.Update(incomeCaptionPoolRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeCaptionPoolRate(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCaptionPoolRateRepository incomeCaptionPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCaptionPoolRateRepository>();

                incomeCaptionPoolRateRepository.Remove(ID);
            });
        }

        public IncomeCaptionPoolRate GetIncomeCaptionPoolRate(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCaptionPoolRateRepository incomeCaptionPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCaptionPoolRateRepository>();

                IncomeCaptionPoolRate incomeCaptionPoolRateEntity = incomeCaptionPoolRateRepository.Get(ID);
                if (incomeCaptionPoolRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeCaptionPoolRate with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeCaptionPoolRateEntity;
            });
        }

        public IncomeCaptionPoolRate[] GetAllIncomeCaptionPoolRate()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCaptionPoolRateRepository incomeCaptionPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCaptionPoolRateRepository>();


                IEnumerable<IncomeCaptionPoolRate> incomeCaptionPoolRate = incomeCaptionPoolRateRepository.Get().ToArray();

                return incomeCaptionPoolRate.ToArray();
            });
        }

        public IncomeCaptionPoolRate[] GetIncomeCaptionPoolRateUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCaptionPoolRateRepository incomeCaptionPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCaptionPoolRateRepository>();

                IEnumerable<IncomeCaptionPoolRate> incomeCaptionPoolRate = incomeCaptionPoolRateRepository.GetIncomeCaptionPoolRateBySearchValue(searchvalue);



                return incomeCaptionPoolRate.ToArray();
            });
        }

        #endregion

        #region IncomeAccountsTreeAccountTEMP

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeAccountsTreeAccountTEMP UpdateIncomeAccountsTreeAccountTEMP(IncomeAccountsTreeAccountTEMP iatat)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeAccountTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeAccountTEMPRepository>();

                IncomeAccountsTreeAccountTEMP updatedEntity = null;

                //if (iatat.ID == 0)
                //{

                //    updatedEntity = iatatRepository.Add(iatat);
                //}
                //else
                //    updatedEntity = iatatRepository.Update(iatat);


                iatat.ApprovalStatus = "AWAITINGAPPROVAL";
                if (iatat.ID == 0)
                {

                    updatedEntity = iatatRepository.Add(iatat);
                }
                else
                {
                    if (GetIncomeAccountsTreeAccountTEMP(iatat.ID).ApprovalStatus == "AWAITINGAPPROVAL")
                        updatedEntity = iatatRepository.Update(iatat);
                }
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeAccountsTreeAccountTEMP(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeAccountTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeAccountTEMPRepository>();

                //to ensure only awaitingaprroval item in the DB is updated
                if (GetIncomeAccountsTreeAccountTEMP(ID).ApprovalStatus == "AWAITINGAPPROVAL")
                    iatatRepository.Remove(ID);
            });
        }

        public IncomeAccountsTreeAccountTEMP GetIncomeAccountsTreeAccountTEMP(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeAccountTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeAccountTEMPRepository>();

                IncomeAccountsTreeAccountTEMP iatatEntity = iatatRepository.Get(ID);
                if (iatatEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeAccountsTreeAccountTEMP with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return iatatEntity;
            });
        }

        public IncomeAccountsTreeAccountTEMP[] GetAllIncomeAccountsTreeAccountTEMP()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeAccountTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeAccountTEMPRepository>();


                IEnumerable<IncomeAccountsTreeAccountTEMP> iatat = iatatRepository.Get().ToArray();

                return iatat.ToArray();
            });
        }

        public IncomeAccountsTreeAccountTEMP[] GetIncomeAccountsTreeAccountTEMPBySearchVal(string search)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeAccountTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeAccountTEMPRepository>();

                IEnumerable<IncomeAccountsTreeAccountTEMP> iatat = iatatRepository.GetIncomeAccountsTreeAccountTEMPBySearchVal(search);

                return iatat.ToArray();
            });
        }

        #endregion

        #region IncomeAccountsTreeMisCodesTEMP

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeAccountsTreeMisCodesTEMP UpdateIncomeAccountsTreeMisCodesTEMP(IncomeAccountsTreeMisCodesTEMP iatat)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeMisCodesTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeMisCodesTEMPRepository>();

                IncomeAccountsTreeMisCodesTEMP updatedEntity = null;

                //if (iatat.ID == 0)
                //{

                //    updatedEntity = iatatRepository.Add(iatat);
                //}
                //else
                //    updatedEntity = iatatRepository.Update(iatat);


                iatat.ApprovalStatus = "AWAITINGAPPROVAL";
                if (iatat.ID == 0)
                {

                    updatedEntity = iatatRepository.Add(iatat);
                }
                else
                {
                    if (GetIncomeAccountsTreeMisCodesTEMP(iatat.ID).ApprovalStatus == "AWAITINGAPPROVAL")
                        updatedEntity = iatatRepository.Update(iatat);
                }
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeAccountsTreeMisCodesTEMP(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeMisCodesTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeMisCodesTEMPRepository>();

                //to ensure only awaitingaprroval item in the DB is updated
                if (GetIncomeAccountsTreeMisCodesTEMP(ID).ApprovalStatus == "AWAITINGAPPROVAL")
                    iatatRepository.Remove(ID);
            });
        }

        public IncomeAccountsTreeMisCodesTEMP GetIncomeAccountsTreeMisCodesTEMP(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeMisCodesTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeMisCodesTEMPRepository>();

                IncomeAccountsTreeMisCodesTEMP iatatEntity = iatatRepository.Get(ID);
                if (iatatEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeAccountsTreeMisCodesTEMP with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return iatatEntity;
            });
        }

        public IncomeAccountsTreeMisCodesTEMP[] GetAllIncomeAccountsTreeMisCodesTEMP()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeMisCodesTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeMisCodesTEMPRepository>();


                IEnumerable<IncomeAccountsTreeMisCodesTEMP> iatat = iatatRepository.Get().ToArray();

                return iatat.ToArray();
            });
        }

        public IncomeAccountsTreeMisCodesTEMP[] GetIncomeAccountsTreeMisCodesTEMPBySearchVal(string search)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountsTreeMisCodesTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountsTreeMisCodesTEMPRepository>();

                IEnumerable<IncomeAccountsTreeMisCodesTEMP> iatat = iatatRepository.GetIncomeAccountsTreeMisCodesTEMPBySearchVal(search);

                return iatat.ToArray();
            });
        }

        #endregion

        #region OneBankAO

        [OperationBehavior(TransactionScopeRequired = true)]
        public OneBankAO UpdateOneBankAO(OneBankAO onebank)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankAORepository onebankRepository = _DataRepositoryFactory.GetDataRepository<IOneBankAORepository>();

                OneBankAO updatedEntity = null;

                if (onebank.Id == 0)
                {
                    updatedEntity = onebankRepository.Add(onebank);
                }
                else
                    updatedEntity = onebankRepository.Update(onebank);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOneBankAO(int Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankAORepository onebankRepository = _DataRepositoryFactory.GetDataRepository<IOneBankAORepository>();

                    onebankRepository.Remove(Id);
            });
        }

        public OneBankAO GetOneBankAO(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankAORepository onebankRepository = _DataRepositoryFactory.GetDataRepository<IOneBankAORepository>();

                OneBankAO onebankEntity = onebankRepository.Get(Id);
                if (onebankEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Item with Id of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return onebankEntity;
            });
        }

        public OneBankAO[] GetAllOneBankAO()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankAORepository onebankRepository = _DataRepositoryFactory.GetDataRepository<IOneBankAORepository>();

                IEnumerable<OneBankAO> onebank = onebankRepository.Get().ToArray();

                return onebank.ToArray();
            });
        }

        public OneBankAO[] GetOneBankAOByParams(string SearchValue, int year, int period)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankAORepository onebankRepository = _DataRepositoryFactory.GetDataRepository<IOneBankAORepository>();

                IEnumerable<OneBankAO> onebank = onebankRepository.GetOneBankAOByParams(SearchValue, year, period);

                return onebank.ToArray();
            });
        }

        #endregion

        #region OneBankBranch

        [OperationBehavior(TransactionScopeRequired = true)]
        public OneBankBranch UpdateOneBankBranch(OneBankBranch onebank)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankBranchRepository onebankRepository = _DataRepositoryFactory.GetDataRepository<IOneBankBranchRepository>();

                OneBankBranch updatedEntity = null;

                if (onebank.Id == 0)
                {
                    updatedEntity = onebankRepository.Add(onebank);
                }
                else
                    updatedEntity = onebankRepository.Update(onebank);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOneBankBranch(int Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankBranchRepository onebankRepository = _DataRepositoryFactory.GetDataRepository<IOneBankBranchRepository>();

                onebankRepository.Remove(Id);
            });
        }

        public OneBankBranch GetOneBankBranch(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankBranchRepository onebankRepository = _DataRepositoryFactory.GetDataRepository<IOneBankBranchRepository>();

                OneBankBranch onebankEntity = onebankRepository.Get(Id);
                if (onebankEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Item with Id of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return onebankEntity;
            });
        }

        public OneBankBranch[] GetAllOneBankBranch()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankBranchRepository onebankRepository = _DataRepositoryFactory.GetDataRepository<IOneBankBranchRepository>();

                IEnumerable<OneBankBranch> onebank = onebankRepository.Get().ToArray();

                return onebank.ToArray();
            });
        }

        public OneBankBranch[] GetOneBankBranchByParams(string SearchValue, int year, int period)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankBranchRepository onebankRepository = _DataRepositoryFactory.GetDataRepository<IOneBankBranchRepository>();

                IEnumerable<OneBankBranch> onebank = onebankRepository.GetOneBankBranchByParams(SearchValue, year, period);

                return onebank.ToArray();
            });
        }

        #endregion

        #region OneBankRegionTD

        [OperationBehavior(TransactionScopeRequired = true)]
        public OneBankRegionTD UpdateOneBankRegionTD(OneBankRegionTD oneBankRegionTD)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankRegionTDRepository oneBankRegionTDRepository = _DataRepositoryFactory.GetDataRepository<IOneBankRegionTDRepository>();

                OneBankRegionTD updatedEntity = null;

                if (oneBankRegionTD.ID == 0)
                {

                    updatedEntity = oneBankRegionTDRepository.Add(oneBankRegionTD);
                }
                else
                    updatedEntity = oneBankRegionTDRepository.Update(oneBankRegionTD);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOneBankRegionTD(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankRegionTDRepository oneBankRegionTDRepository = _DataRepositoryFactory.GetDataRepository<IOneBankRegionTDRepository>();

                oneBankRegionTDRepository.Remove(ID);
            });
        }

        public OneBankRegionTD GetOneBankRegionTD(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankRegionTDRepository oneBankRegionTDRepository = _DataRepositoryFactory.GetDataRepository<IOneBankRegionTDRepository>();

                OneBankRegionTD oneBankRegionTDEntity = oneBankRegionTDRepository.Get(ID);
                if (oneBankRegionTDEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OneBankRegionTD with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return oneBankRegionTDEntity;
            });
        }

        public OneBankRegionTD[] GetAllOneBankRegionTD()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankRegionTDRepository oneBankRegionTDRepository = _DataRepositoryFactory.GetDataRepository<IOneBankRegionTDRepository>();


                IEnumerable<OneBankRegionTD> oneBankRegionTD = oneBankRegionTDRepository.Get().ToArray();

                return oneBankRegionTD.ToArray();
            });
        }

        #endregion

        #region OneBankTeamTable

        [OperationBehavior(TransactionScopeRequired = true)]
        public OneBankTeamTable UpdateOneBankTeamTable(OneBankTeamTable oneBankTeamTable)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankTeamTableRepository oneBankTeamTableRepository = _DataRepositoryFactory.GetDataRepository<IOneBankTeamTableRepository>();

                OneBankTeamTable updatedEntity = null;

                if (oneBankTeamTable.ID == 0)
                {

                    updatedEntity = oneBankTeamTableRepository.Add(oneBankTeamTable);
                }
                else
                    updatedEntity = oneBankTeamTableRepository.Update(oneBankTeamTable);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteOneBankTeamTable(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankTeamTableRepository oneBankTeamTableRepository = _DataRepositoryFactory.GetDataRepository<IOneBankTeamTableRepository>();

                oneBankTeamTableRepository.Remove(ID);
            });
        }

        public OneBankTeamTable GetOneBankTeamTable(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankTeamTableRepository oneBankTeamTableRepository = _DataRepositoryFactory.GetDataRepository<IOneBankTeamTableRepository>();

                OneBankTeamTable oneBankTeamTableEntity = oneBankTeamTableRepository.Get(ID);
                if (oneBankTeamTableEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("OneBankTeamTable with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return oneBankTeamTableEntity;
            });
        }

        public OneBankTeamTable[] GetAllOneBankTeamTable()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IOneBankTeamTableRepository oneBankTeamTableRepository = _DataRepositoryFactory.GetDataRepository<IOneBankTeamTableRepository>();


                IEnumerable<OneBankTeamTable> oneBankTeamTable = oneBankTeamTableRepository.Get().ToArray();

                return oneBankTeamTable.ToArray();
            });
        }

        #endregion

        #region MprInterestMapping

        [OperationBehavior(TransactionScopeRequired = true)]
        public MprInterestMapping UpdateMprInterestMapping(MprInterestMapping mprInterestMapping)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMprInterestMappingRepository mprInterestMappingRepository = _DataRepositoryFactory.GetDataRepository<IMprInterestMappingRepository>();

                MprInterestMapping updatedEntity = null;

                if (mprInterestMapping.ID == 0)
                {

                    updatedEntity = mprInterestMappingRepository.Add(mprInterestMapping);
                }
                else
                    updatedEntity = mprInterestMappingRepository.Update(mprInterestMapping);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMprInterestMapping(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMprInterestMappingRepository mprInterestMappingRepository = _DataRepositoryFactory.GetDataRepository<IMprInterestMappingRepository>();

                mprInterestMappingRepository.Remove(ID);
            });
        }

        public MprInterestMapping GetMprInterestMapping(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMprInterestMappingRepository mprInterestMappingRepository = _DataRepositoryFactory.GetDataRepository<IMprInterestMappingRepository>();

                MprInterestMapping mprInterestMappingEntity = mprInterestMappingRepository.Get(ID);
                if (mprInterestMappingEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MprInterestMapping with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return mprInterestMappingEntity;
            });
        }

        public MprInterestMapping[] GetAllMprInterestMapping()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMprInterestMappingRepository mprInterestMappingRepository = _DataRepositoryFactory.GetDataRepository<IMprInterestMappingRepository>();


                IEnumerable<MprInterestMapping> mprInterestMapping = mprInterestMappingRepository.Get().ToArray();

                return mprInterestMapping.ToArray();
            });
        }

        #endregion

        #region MISNewOthersTEMP

        [OperationBehavior(TransactionScopeRequired = true)]
        public MISNewOthersTEMP UpdateMISNewOthersTEMP(MISNewOthersTEMP iatat)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISNewOthersTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IMISNewOthersTEMPRepository>();

                MISNewOthersTEMP updatedEntity = null;

                //if (iatat.ID == 0)
                //{

                //    updatedEntity = iatatRepository.Add(iatat);
                //}
                //else
                //    updatedEntity = iatatRepository.Update(iatat);


                iatat.ApprovalStatus = "AWAITINGAPPROVAL";
                if (iatat.Id == 0)
                {

                    updatedEntity = iatatRepository.Add(iatat);
                }
                else
                {
                    if (GetMISNewOthersTEMP(iatat.Id).ApprovalStatus == "AWAITINGAPPROVAL")
                        updatedEntity = iatatRepository.Update(iatat);
                }
                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteMISNewOthersTEMP(int Id)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISNewOthersTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IMISNewOthersTEMPRepository>();

                //to ensure only awaitingaprroval item in the DB is updated
                if (GetMISNewOthersTEMP(Id).ApprovalStatus == "AWAITINGAPPROVAL")
                    iatatRepository.Remove(Id);
            });
        }

        public MISNewOthersTEMP GetMISNewOthersTEMP(int Id)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISNewOthersTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IMISNewOthersTEMPRepository>();

                MISNewOthersTEMP iatatEntity = iatatRepository.Get(Id);
                if (iatatEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("MISNewOthersTEMP with Id of {0} is not in database", Id));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return iatatEntity;
            });
        }

        public MISNewOthersTEMP[] GetAllMISNewOthersTEMP()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISNewOthersTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IMISNewOthersTEMPRepository>();


                IEnumerable<MISNewOthersTEMP> iatat = iatatRepository.Get().ToArray();

                return iatat.ToArray();
            });
        }

        public MISNewOthersTEMP[] GetMISNewOthersTEMPBySearchVal(string search)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IMISNewOthersTEMPRepository iatatRepository = _DataRepositoryFactory.GetDataRepository<IMISNewOthersTEMPRepository>();

                IEnumerable<MISNewOthersTEMP> iatat = iatatRepository.GetMISNewOthersTEMPBySearchVal(search);

                return iatat.ToArray();
            });
        }

        #endregion

        #region IncomeCustomerPoolRate

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeCustomerPoolRate UpdateIncomeCustomerPoolRate(IncomeCustomerPoolRate incomeCustomerPoolRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCustomerPoolRateRepository incomeCustomerPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCustomerPoolRateRepository>();

                IncomeCustomerPoolRate updatedEntity = null;
                
                if (incomeCustomerPoolRate.Id == 0)
                {
                    if(incomeCustomerPoolRateRepository.ValidateIncomeCustomerPoolRate(incomeCustomerPoolRate.CustomerNo, incomeCustomerPoolRate.Year) == null)
                    updatedEntity = incomeCustomerPoolRateRepository.Add(incomeCustomerPoolRate);
                }
                else
                    updatedEntity = incomeCustomerPoolRateRepository.Update(incomeCustomerPoolRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeCustomerPoolRate(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCustomerPoolRateRepository incomeCustomerPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCustomerPoolRateRepository>();

                incomeCustomerPoolRateRepository.Remove(ID);
            });
        }

        public IncomeCustomerPoolRate GetIncomeCustomerPoolRate(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCustomerPoolRateRepository incomeCustomerPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCustomerPoolRateRepository>();

                IncomeCustomerPoolRate incomeCustomerPoolRateEntity = incomeCustomerPoolRateRepository.Get(ID);
                if (incomeCustomerPoolRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeCustomerPoolRate with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeCustomerPoolRateEntity;
            });
        }

        public IncomeCustomerPoolRate[] GetAllIncomeCustomerPoolRate()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCustomerPoolRateRepository incomeCustomerPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCustomerPoolRateRepository>();

                IEnumerable<IncomeCustomerPoolRate> incomeCustomerPoolRate = incomeCustomerPoolRateRepository.Get().ToArray();

                return incomeCustomerPoolRate.ToArray();
            });
        }

        public IncomeCustomerPoolRate[] GetIncomeCustomerPoolRateUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeCustomerPoolRateRepository incomeCustomerPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeCustomerPoolRateRepository>();

                IEnumerable<IncomeCustomerPoolRate> incomeCustomerPoolRate = incomeCustomerPoolRateRepository.GetIncomeCustomerPoolRateBySearchValue(searchvalue);


                return incomeCustomerPoolRate.ToArray();
            });
        }

        #endregion

        #region IncomeAccountPoolRate

        [OperationBehavior(TransactionScopeRequired = true)]
        public IncomeAccountPoolRate UpdateIncomeAccountPoolRate(IncomeAccountPoolRate incomeAccountPoolRate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountPoolRateRepository incomeAccountPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountPoolRateRepository>();

                IncomeAccountPoolRate updatedEntity = null;

                if (incomeAccountPoolRate.Id == 0)
                {
                   // if (incomeCustomerPoolRateRepository.ValidateIncomeCustomerPoolRate(incomeCustomerPoolRate.CustomerNo, incomeCustomerPoolRate.Year) == null)
                        updatedEntity = incomeAccountPoolRateRepository.Add(incomeAccountPoolRate);
                }
                else
                    updatedEntity = incomeAccountPoolRateRepository.Update(incomeAccountPoolRate);

                return updatedEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void DeleteIncomeAccountPoolRate(int ID)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountPoolRateRepository incomeAccountPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountPoolRateRepository>();

                incomeAccountPoolRateRepository.Remove(ID);
            });
        }

        public IncomeAccountPoolRate GetIncomeAccountPoolRate(int ID)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountPoolRateRepository incomeAccountPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountPoolRateRepository>();

                IncomeAccountPoolRate incomeAccountPoolRateEntity = incomeAccountPoolRateRepository.Get(ID);
                if (incomeAccountPoolRateEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("IncomeAccountPoolRate with ID of {0} is not in database", ID));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return incomeAccountPoolRateEntity;
            });
        }

        public IncomeAccountPoolRate[] GetAllIncomeAccountPoolRate()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER, GROUP_SUPER_BUSINESS };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountPoolRateRepository incomeAccountPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountPoolRateRepository>();

                IEnumerable<IncomeAccountPoolRate> incomeAccountPoolRate = incomeAccountPoolRateRepository.Get().ToArray();

                return incomeAccountPoolRate.ToArray();
            });
        }

        public IncomeAccountPoolRate[] GetIncomeAccountPoolRateUsingSearchValue(string searchvalue)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var groupNames = new List<string>() { GROUP_ADMINISTRATOR, GROUP_USER };
                AllowAccessToOperation(SOLUTION_NAME, groupNames);

                IIncomeAccountPoolRateRepository incomeAccountPoolRateRepository = _DataRepositoryFactory.GetDataRepository<IIncomeAccountPoolRateRepository>();

                IEnumerable<IncomeAccountPoolRate> incomeAccountPoolRate = incomeAccountPoolRateRepository.GetIncomeAccountPoolRateBySearchValue(searchvalue);


                return incomeAccountPoolRate.ToArray();
            });
        }

        #endregion

    }
}
