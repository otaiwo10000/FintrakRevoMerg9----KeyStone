"use strict";

var commonModule = angular.module('fintrakCommon', ['ngRoute', 'ui.bootstrap']);

var App = angular.module('fintrak', ['ngRoute', 'ui.bootstrap', 'ui.router', 'oc.lazyLoad', 'ngResource',
    'fintrakCommon', 'colorpicker.module', 'ngCsvImport', 'ngLoadingSpinner', 'ks.ngScrollRepeat', 'treeGrid',
    'ngIdleTimer', 'ngSanitize', 'ngCsv', 'ngJsonExportExcel', 'ngTable', 'ngInputDate', 'ng-fusioncharts']);
//, 'moment-picker', 'textAngular'
App.config(function ($provide) {
    $provide.decorator("$exceptionHandler",
        ["$delegate",
            function ($delegate) {
                return function (exception, cause) {
                    exception.message = "Please contact the Help Desk! \n Message: " +
                        exception.message;
                    $delegate(exception, cause);
                    alert(exception.message);
                };
            }]);
});

//Http Intercpetor to check auth failures for xhr requests
App.config(['$httpProvider',
    function ($httpProvider) {
        $httpProvider.interceptors.push('httpErrorResponseInterceptor');
    }
]);
App.config(function ($stateProvider, $urlRouterProvider) {
    //
    // For any unmatched url, redirect to /state1
    //var rootUrl = '/ClientPortal/';
    var rootUrl = '';


    $urlRouterProvider.otherwise("/core-userprofile-list");
    //
    // Now set up the states
    $stateProvider

        .state('core-module-list', {
            url: "/core-module-list-view",
            templateUrl: rootUrl + 'app/core/views/module/module-list-view.html',
            controller: 'ModuleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-apps-list', {
            url: "/core-apps-list-view",
            templateUrl: rootUrl + 'app/core/views/module/apps-list-view.html',
            controller: 'AppListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-general-edit', {
            url: "/core-general-edit-view",
            templateUrl: rootUrl + 'app/core/views/configuration/general-edit-view.html',
            controller: 'GeneralEditController as vm'



        }).state('core-changepassword-edit', {
            url: "/core-changepassword-edit-view",
            templateUrl: rootUrl + 'app/core/views/account/changepassword-edit-view.html',
            controller: 'AccountChangePasswordController as vm'

        }).state('core-fiscalyear-list', {
            url: "/core-fiscalyear-list-view",
            templateUrl: rootUrl + 'app/core/views/configuration/fiscalyear-list-view.html',
            controller: 'FiscalYearListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.tableTools.min.js',
                            rootUrl + 'app/assets/js/plugins/datatable/dataTables.bootstrap.js']
                    });
                }]
            }
        }).state('core-fiscalyear-edit', {
            url: "/core-fiscalyear-edit-view/:fiscalyearId",
            templateUrl: rootUrl + 'app/core/views/configuration/fiscalyear-edit-view.html',
            controller: 'FiscalYearEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']

                    });
                }]
            }
        }).state('core-fiscalperiod-edit', {
            url: "/core-fiscalperiod-edit-view/:fiscalyearId?fiscalperiodId",
            templateUrl: rootUrl + 'app/core/views/configuration/fiscalperiod-edit-view.html',
            controller: 'FiscalPeriodEditController as vm'
        }).state('core-financialtype-list', {
            url: "/core-financialtype-list",
            templateUrl: rootUrl + 'app/core/views/configuration/financialtype-list-view.html',
            controller: 'FinancialTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-financialtype-edit', {
            url: "/core-financialtype-edit/:financialtypeId",
            templateUrl: rootUrl + 'app/core/views/configuration/financialtype-edit-view.html',
            controller: 'FinancialTypeEditController as vm'

        }).state('core-gldefinition-list', {
            url: "/core-gldefinition-list",
            templateUrl: rootUrl + 'app/core/views/configuration/gldefinition-list-view.html',
            controller: 'GLDefinitionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-gldefinition-edit', {
            url: "/core-gldefinition-edit/:gldefinitionId",
            templateUrl: rootUrl + 'app/core/views/configuration/gldefinition-edit-view.html',
            controller: 'GLDefinitionEditController as vm'

        }).state('core-chartofaccount-list', {
            url: "/core-chartofaccount-list",
            templateUrl: rootUrl + 'app/core/views/configuration/chartofaccount-list-view.html',
            controller: 'ChartOfAccountListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-chartofaccount-edit', {
            url: "/core-chartofaccount-edit/:chartofaccountId",
            templateUrl: rootUrl + 'app/core/views/configuration/chartofaccount-edit-view.html',
            controller: 'ChartOfAccountEditController as vm'
        }).state('core-currency-list', {
            url: "/core-currency-list-view",
            templateUrl: rootUrl + 'app/core/views/configuration/currency-list-view.html',
            controller: 'CurrencyListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-audittrail-list', {
            url: "/core-audittrail-list-view",
            templateUrl: rootUrl + 'app/core/views/configuration/audittrail-list-view.html',
            controller: 'AuditTrailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-currency-edit', {
            url: "/core-currency-edit-view/:currencyId",
            templateUrl: rootUrl + 'app/core/views/configuration/currency-edit-view.html',
            controller: 'CurrencyEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

            //
        }).state('core-reportstatus-list', {
            url: "/core-reportstatus-list-view",
            templateUrl: rootUrl + 'app/core/views/configuration/reportstatus-list-view.html',
            controller: 'ReportStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('core-reportstatus-edit', {
            url: "/core-reportstatus-edit-view/:statusId",
            templateUrl: rootUrl + 'app/core/views/configuration/reportstatus-edit-view.html',
            controller: 'ReportStatusEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
            //
        }).state('core-currencyrate-edit', {
            url: "/core-currencyrate-edit-view/:currencyId?currencyrateId",
            templateUrl: rootUrl + 'app/core/views/configuration/currencyrate-edit-view.html',
            controller: 'CurrencyRateEditController as vm'
        }).state('core-producttype-list', {
            url: "/core-producttype-list",
            templateUrl: rootUrl + 'app/core/views/configuration/producttype-list-view.html',
            controller: 'ProductTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-producttype-edit', {
            url: "/core-producttype-edit/:producttypeId",
            templateUrl: rootUrl + 'app/core/views/configuration/producttype-edit-view.html',
            controller: 'ProductTypeEditController as vm'
        }).state('core-product-list', {
            url: "/core-product-list-view",
            templateUrl: rootUrl + 'app/core/views/configuration/product-list-view.html',
            controller: 'ProductListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-product-edit', {
            url: "/core-product-edit-view/:productId?code?name",
            templateUrl: rootUrl + 'app/core/views/configuration/product-edit-view.html',
            controller: 'ProductEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-producttypemapping-edit', {
            url: "/core-producttypemapping-edit-view/:productId?productcode?producttypemappingId",
            templateUrl: rootUrl + 'app/core/views/configuration/producttypemapping-edit-view.html',
            controller: 'ProductTypeMappingEditController as vm'
        }).state('core-role-list', {
            url: "/core-role-list",
            templateUrl: rootUrl + 'app/core/views/account/role-list-view.html',
            controller: 'RoleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-role-edit', {
            url: "/core-role-edit-view/:roleId",
            templateUrl: rootUrl + 'app/core/views/account/role-edit-view.html',
            controller: 'RoleEditController as vm'
        }).state('core-menurole-list', {
            url: "/core-menurole-list",
            templateUrl: rootUrl + 'app/core/views/accessibility/menurole-list-view.html',
            controller: 'MenuRoleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-company-list', {
            url: "/core-company-list",
            templateUrl: rootUrl + 'app/core/views/configuration/company-list-view.html',
            controller: 'CompanyListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-company-edit', {
            url: "/core-company-edit-view/:companyId",
            templateUrl: rootUrl + 'app/core/views/configuration/company-edit-view.html',
            controller: 'CompanyEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-branch-edit', {
            url: "/core-branch-edit-view/:companyId?branchId",
            templateUrl: rootUrl + 'app/core/views/configuration/branch-edit-view.html',
            controller: 'BranchEditController as vm'
        }).state('core-companymodule-list', {
            url: "/core-companymodule-list",
            templateUrl: rootUrl + 'app/core/views/configuration/companymodule-list-view.html',
            controller: 'CompanyModuleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-companymodule-edit', {
            url: "/core-companymodule-edit-view/:companymoduleId",
            templateUrl: rootUrl + 'app/core/views/configuration/companymodule-edit-view.html',
            controller: 'CompanyModuleEditController as vm'
        }).state('core-usersetup-list', {
            url: "/core-usersetup-list",
            templateUrl: rootUrl + 'app/core/views/account/usersetup-list-view.html',
            controller: 'UserSetupListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-usersetup-edit', {
            url: "/core-usersetup-edit/:usersetupId",
            templateUrl: rootUrl + 'app/core/views/account/usersetup-edit-view.html',
            controller: 'UserSetupEditController as vm'
        }).state('core-usermanager-list', {
            url: "/core-usermanager-list",
            templateUrl: rootUrl + 'app/core/views/account/usermanager-list-view.html',
            controller: 'UserManagerListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-usermanager-edit', {
            url: "/core-usermanager-edit/:usersetupId",
            templateUrl: rootUrl + 'app/core/views/account/usermanager-edit-view.html',
            controller: 'UserManagerEditController as vm'
        }).state('core-userprofile-list', {
            url: "/core-userprofile-list",
            templateUrl: rootUrl + 'app/core/views/account/userprofile-list-view.html',
            controller: 'UserProfileListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('core-errortracker-list', {
            url: "/core-errortracker-list",
            templateUrl: rootUrl + 'app/core/views/configuration/errortracker-list-view.html',
            controller: 'ErrorTrackerListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('core-activity-list', {
            url: "/core-activity-list",
            templateUrl: rootUrl + 'app/core/views/configuration/activity-list-view.html',
            controller: 'ActivityListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-activity-edit', {
            url: "/core-activity-edit-view/:activityId",
            templateUrl: rootUrl + 'app/core/views/configuration/activity-edit-view.html',
            controller: 'ActivityEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-packagesetup-edit', {
            url: "/core-packagesetup-edit-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/packagesetup-edit-view.html',
            controller: 'PackageSetupEditController as vm'
        }).state('core-solutionrundate-list', {
            url: "/core-solutionrundate-list",
            templateUrl: rootUrl + 'app/extraction/views/extraction/solutionrundate-list-view.html',
            controller: 'SolutionRunDateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-solutionrundate-edit', {
            url: "/core-solutionrundate-edit/:solutionrundateId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/solutionrundate-edit-view.html',
            controller: 'SolutionRunDateEditController as vm'
        }).state('core-closedperiod-list', {
            url: "/core-closedperiod-list",
            templateUrl: rootUrl + 'app/extraction/views/extraction/closedperiod-list-view.html',
            controller: 'ClosedPeriodListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-closedperiod-edit', {
            url: "/core-closedperiod-edit/:closedperiodId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/closedperiod-edit-view.html',
            controller: 'ClosedPeriodEditController as vm'
        }).state('core-closedperiodtemplate-list', {
            url: "/core-closedperiodtemplate-list",
            templateUrl: rootUrl + 'app/extraction/views/extraction/closedperiodtemplate-list-view.html',
            controller: 'ClosedPeriodTemplateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-closedperiodtemplate-edit', {
            url: "/core-closedperiodtemplate-edit/:closedperiodtemplateId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/closedperiodtemplate-edit-view.html',
            controller: 'ClosedPeriodTemplateEditController as vm'
        }).state('core-extraction-list', {
            url: "/core-extraction-list-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/extraction-list-view.html',
            controller: 'ExtractionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-extraction-edit', {
            url: "/core-extraction-edit-view/:extractionId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/extraction-edit-view.html',
            controller: 'ExtractionEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-extractionrole-edit', {
            url: "/core-extractionrole-edit-view/:extractionId?extractionroleId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/extractionrole-edit-view.html',
            controller: 'ExtractionRoleEditController as vm'
        }).state('core-runextraction-list', {
            url: "/core-runextraction-list-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/runextraction-list-view.html',
            controller: 'RunExtractionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-process-list', {
            url: "/core-process-list-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/process-list-view.html',
            controller: 'ProcessListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-process-edit', {
            url: "/core-process-edit-view/:processId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/process-edit-view.html',
            controller: 'ProcessEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-processrole-edit', {
            url: "/core-processrole-edit-view/:processId?processroleId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/processrole-edit-view.html',
            controller: 'ProcessRoleEditController as vm'
        }).state('core-runprocess-list', {
            url: "/core-runprocess-list-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/runprocess-list-view.html',
            controller: 'RunProcessListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-upload-list', {
            url: "/core-upload-list-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/upload-list-view.html',
            controller: 'UploadListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-upload-edit', {
            url: "/core-upload-edit-view/:uploadId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/upload-edit-view.html',
            controller: 'UploadEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-uploadrole-edit', {
            url: "/core-uploadrole-edit-view/:uploadId?uploadroleId",
            templateUrl: rootUrl + 'app/extraction/views/extraction/uploadrole-edit-view.html',
            controller: 'UploadRoleEditController as vm'
        }).state('core-runupload-list', {
            url: "/core-runupload-list-view",
            templateUrl: rootUrl + 'app/extraction/views/extraction/runupload-list-view.html',
            controller: 'RunUploadListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('core-client-list', {
            url: "/core-client-list",
            templateUrl: rootUrl + 'app/core/views/configuration/client-list-view.html',
            controller: 'ClientListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-client-edit', {
            url: "/core-client-edit/:clientId",
            templateUrl: rootUrl + 'app/core/views/configuration/client-edit-view.html',
            controller: 'ClientEditController as vm'



        }).state('core-database-list', {
            url: "/core-database-list",
            templateUrl: rootUrl + 'app/core/views/configuration/database-list-view.html',
            controller: 'DatabaseListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-database-edit', {
            url: "/core-database-edit/:databaseId",
            templateUrl: rootUrl + 'app/core/views/configuration/database-edit-view.html',
            controller: 'DatabaseEditController as vm'



        }).state('core-defaultuser-list', {
            url: "/core-defaultuser-list",
            templateUrl: rootUrl + 'app/core/views/configuration/defaultuser-list-view.html',
            controller: 'DefaultuserListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-defaultuser-edit', {
            url: "/core-defaultuser-edit/:defaultuserId",
            templateUrl: rootUrl + 'app/core/views/configuration/defaultuser-edit-view.html',
            controller: 'DefaultuserEditController as vm'



        }).state('core-companysecurity-list', {
            url: "/core-companysecurity-list",
            templateUrl: rootUrl + 'app/core/views/configuration/companysecurity-list-view.html',
            controller: 'CompanySecurityListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-companysecurity-edit', {
            url: "/core-companysecurity-edit/:companysecurityId",
            templateUrl: rootUrl + 'app/core/views/configuration/companysecurity-edit-view.html',
            controller: 'CompanySecurityEditController as vm'


        }).state('core-usersession-list', {
            url: "/core-usersession-list",
            templateUrl: rootUrl + 'app/core/views/configuration/usersession-list-view.html',
            controller: 'UserSessionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-usersession-edit', {
            url: "/core-usersession-edit/:usersessionId",
            templateUrl: rootUrl + 'app/core/views/configuration/usersession-edit-view.html',
            controller: 'UserSessionEditController as vm'



        }).state('core-staffs-list', {
            url: "/core-staffs-list",
            templateUrl: rootUrl + 'app/core/views/configuration/staff-list-view.html',
            controller: 'StaffListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('core-staff-edit', {
            url: "/core-staff-edit/:staffId",
            templateUrl: rootUrl + 'app/core/views/configuration/staff-edit-view.html',
            controller: 'StaffEditController as vm'




        }).state('finstat-registry-list', {
            url: "/finstat-registry-list",
            templateUrl: rootUrl + 'app/finstat/views/registry-list-view.html',
            controller: 'RegistryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-registry-edit', {
            url: "/finstat-registry-edit/:registryId",
            templateUrl: rootUrl + 'app/finstat/views/registry-edit-view.html',
            controller: 'RegistryEditController as vm'


        }).state('finstat-revacctregistry-list', {
            url: "/finstat-revacctregistry-list",
            templateUrl: rootUrl + 'app/finstat/views/revacctregistry-list-view.html',
            controller: 'RevacctRegistryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-revacctregistry-edit', {
            url: "/finstat-revacctregistry-edit/:revenueId",
            templateUrl: rootUrl + 'app/finstat/views/revacctregistry-edit-view.html',
            controller: 'RevacctRegistryEditController as vm'


        }).state('finstat-qualitativenote-list', {
            url: "/finstat-qualitativenote-list",
            templateUrl: rootUrl + 'app/finstat/views/qualitativenote-list-view.html',
            controller: 'QualitativeNoteListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-qualitativenote-edit', {
            url: "/finstat-qualitativenote-edit/:qualitativeNoteId",
            templateUrl: rootUrl + 'app/finstat/views/qualitativenote-edit-view.html',
            controller: 'QualitativeNoteEditController as vm'


        }).state('finstat-budget-list', {
            url: "/finstat-budget-list",
            templateUrl: rootUrl + 'app/finstat/views/ifrsbudget-list-view.html',
            controller: 'IFRSBudgetListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-budget-edit', {
            url: "/finstat-budget-edit/:ifrsbudgetId",
            templateUrl: rootUrl + 'app/finstat/views/ifrsbudget-edit-view.html',
            controller: 'IFRSBudgetEditController as vm'

        }).state('finstat-derivedcaption-list', {
            url: "/finstat-derivedcaption-list",
            templateUrl: rootUrl + 'app/finstat/views/derivedcaption-list-view.html',
            controller: 'DerivedCaptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-derivedcaption-edit', {
            url: "/finstat-derivedcaption-edit/:derivedcaptionId",
            templateUrl: rootUrl + 'app/finstat/views/derivedcaption-edit-view.html',
            controller: 'DerivedCaptionEditController as vm'
        }).state('finstat-glmapping-list', {
            url: "/finstat-glmapping-list",
            templateUrl: rootUrl + 'app/finstat/views/glmapping-list-view.html',
            controller: 'GLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-glmapping-edit', {
            url: "/finstat-glmapping-edit/:glmappingId",
            templateUrl: rootUrl + 'app/finstat/views/glmapping-edit-view.html',
            controller: 'GLMappingEditController as vm'
        }).state('finstat-unmappedgl-list', {
            url: "/finstat-unmappedgl-list",
            templateUrl: rootUrl + 'app/finstat/views/unmappedgl-list-view.html',
            controller: 'UnMappedGLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-unmappedgl-edit', {
            url: "/finstat-unmappedgl-edit/:glcode",
            templateUrl: rootUrl + 'app/finstat/views/unmappedgl-edit-view.html',
            controller: 'UnMappedGLEditController as vm'
        }).state('finstat-gltype-list', {
            url: "/finstat-gltype-list",
            templateUrl: rootUrl + 'app/finstat/views/gltype-list-view.html',
            controller: 'GLTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-gltype-edit', {
            url: "/finstat-gltype-edit/:gltypeId",
            templateUrl: rootUrl + 'app/finstat/views/gltype-edit-view.html',
            controller: 'GLTypeEditController as vm'
        }).state('finstat-instrumenttype-list', {
            url: "/finstat-instrumenttype-list",
            templateUrl: rootUrl + 'app/finstat/views/instrumenttype-list-view.html',
            controller: 'InstrumentTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-instrumenttype-edit', {
            url: "/finstat-instrumenttype-edit/:instrumenttypeId",
            templateUrl: rootUrl + 'app/finstat/views/instrumenttype-edit-view.html',
            controller: 'InstrumentTypeEditController as vm'
        }).state('finstat-instrumenttypeglmap-list', {
            url: "/finstat-instrumenttypeglmap-list",
            templateUrl: rootUrl + 'app/finstat/views/instrumenttypeglmap-list-view.html',
            controller: 'InstrumentTypeGLMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-instrumenttypeglmap-edit', {
            url: "/finstat-instrumenttypeglmap-edit/:instrumenttypeglmapId",
            templateUrl: rootUrl + 'app/finstat/views/instrumenttypeglmap-edit-view.html',
            controller: 'InstrumentTypeGLMapEditController as vm'
        }).state('finstat-autopostingtemplate-list', {
            url: "/finstat-autopostingtemplate-list",
            templateUrl: rootUrl + 'app/finstat/views/autopostingtemplate-list-view.html',
            controller: 'AutoPostingTemplateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-autopostingtemplate-edit', {
            url: "/finstat-autopostingtemplate-edit/:autopostingtemplateId",
            templateUrl: rootUrl + 'app/finstat/views/autopostingtemplate-edit-view.html',
            controller: 'AutoPostingTemplateEditController as vm'
        }).state('finstat-trialbalance-list', {
            url: "/finstat-trialbalance-list",
            templateUrl: rootUrl + 'app/finstat/views/trialbalance-list-view.html',
            controller: 'TrialBalanceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('finstat-trialbalanceconsolidated-list', {
            url: "/finstat-trialbalanceconsolidated-list",
            templateUrl: rootUrl + 'app/finstat/views/trialbalanceconsolidated-list-view.html',
            controller: 'ConsolidatedTrialBalanceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('finstat-adjustment-list', {
            url: "/finstat-adjustment-list",
            templateUrl: rootUrl + 'app/finstat/views/adjustment-list-view.html',
            controller: 'AdjustmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js'
                        ]
                    });
                }]
            }
        }).state('finstat-adjustment-edit', {
            url: "/finstat-adjustment-edit/:gladjustmentId?adjustmentType?reportType",
            templateUrl: rootUrl + 'app/finstat/views/adjustment-edit-view.html',
            controller: 'AdjustmentEditController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js'
                        ]
                    });
                }]
            }


        }).state('ifrs-reportpackviewer-list', {
            url: "/ifrs-reportpackviewer-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/ifrsreportpackviewer-list-view.html',
            controller: 'IFRSReportPackViewerListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js'
                        ]
                    });
                }]
            }

        }).state('finstat-transactiondetail-list', {
            url: "/finstat-transactiondetail-list",
            templateUrl: rootUrl + 'app/finstat/views/transactiondetail-list-view.html',
            controller: 'TransactionDetailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-postingdetail-list', {
            url: "/finstat-postingdetail-list",
            templateUrl: rootUrl + 'app/finstat/views/postingdetail-list-view.html',
            controller: 'PostingDetailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('finstat-revenueglmapping-list', {
            url: "/finstat-revenueglmapping-list",
            templateUrl: rootUrl + 'app/finstat/views/revenueglmapping-list-view.html',
            controller: 'RevenueGLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('finstat-revenueglmapping-edit', {
            url: "/finstat-revenueglmapping-edit/:glmappingId",
            templateUrl: rootUrl + 'app/finstat/views/revenueglmapping-edit-view.html',
            controller: 'RevenueGLMappingEditController as vm'


        }).state('ifrsloan-loansetup-list', {
            url: "/ifrsloan-loansetup-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/loansetup-list-view.html',
            controller: 'LoanSetupListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-loansetup-edit', {
            url: "/ifrsloan-loansetup-edit/:loansetupId",
            templateUrl: rootUrl + 'app/ifrsloan/views/loansetup-edit-view.html',
            controller: 'LoanSetupEditController as vm'
        }).state('ifrsloan-scheduletype-list', {
            url: "/ifrsloan-scheduletype-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/scheduletype-list-view.html',
            controller: 'ScheduleTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-scheduletype-edit', {
            url: "/ifrsloan-scheduletype-edit/:scheduletypeId",
            templateUrl: rootUrl + 'app/ifrsloan/views/scheduletype-edit-view.html',
            controller: 'ScheduleTypeEditController as vm'
        }).state('ifrsloan-product-list', {
            url: "/ifrsloan-product-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/product-list-view.html',
            controller: 'IFRSProductListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-product-edit', {
            url: "/ifrsloan-product-edit/:productId?code",
            templateUrl: rootUrl + 'app/ifrsloan/views/product-edit-view.html',
            controller: 'IFRSProductEditController as vm'
        }).state('ifrsloan-creditriskrating-list', {
            url: "/ifrsloan-creditriskrating-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/creditriskrating-list-view.html',
            controller: 'CreditRiskRatingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-creditriskrating-edit', {
            url: "/ifrsloan-creditriskrating-edit/:creditriskratingId",
            templateUrl: rootUrl + 'app/ifrsloan/views/creditriskrating-edit-view.html',
            controller: 'CreditRiskRatingEditController as vm'
        }).state('ifrsloan-collateralcategory-list', {
            url: "/ifrsloan-collateralcategory-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateralcategory-list-view.html',
            controller: 'CollateralCategoryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-collateralcategory-edit', {
            url: "/ifrsloan-collateralcategory-edit/:collateralcategoryId",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateralcategory-edit-view.html',
            controller: 'CollateralCategoryEditController as vm'
        }).state('ifrsloan-collateraltype-edit', {
            url: "/ifrsloan-collateraltype-edit/:collateralcategoryId?categorycode?collateraltypeId",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateraltype-edit-view.html',
            controller: 'CollateralTypeEditController as vm'
        }).state('ifrsloan-collateralinformation-list', {
            url: "/ifrsloan-collateralinformation-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateralinformation-list-view.html',
            controller: 'CollateralInformationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-collateralinformation-edit', {
            url: "/ifrsloan-collateralinformation-edit/:collateralinformationId",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateralinformation-edit-view.html',
            controller: 'CollateralInformationEditController as vm'
        }).state('ifrsloan-collateralrealizationperiod-list', {
            url: "/ifrsloan-collateralrealizationperiod-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateralrealizationperiod-list-view.html',
            controller: 'CollateralRealizationPeriodListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-collateralrealizationperiod-edit', {
            url: "/ifrsloan-collateralrealizationperiod-edit/:collateralrealizationperiodId",
            templateUrl: rootUrl + 'app/ifrsloan/views/collateralrealizationperiod-edit-view.html',
            controller: 'CollateralRealizationPeriodEditController as vm'
        }).state('ifrsloan-watchlistedloan-list', {
            url: "/ifrsloan-watchlistedloan-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/watchlistedloan-list-view.html',
            controller: 'WatchListedLoanListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-watchlistedloan-edit', {
            url: "/ifrsloan-watchlistedloan-edit/:watchlistedloanId",
            templateUrl: rootUrl + 'app/ifrsloan/views/watchlistedloan-edit-view.html',
            controller: 'WatchListedLoanEditController as vm'

        }).state('ifrsloan-individualschedule-list', {
            url: "/ifrsloan-individualschedule-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/individualschedule-list-view.html',
            controller: 'IndividualScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-individualschedule-edit', {
            url: "/ifrsloan-individualschedule-edit/:individualscheduleId",
            templateUrl: rootUrl + 'app/ifrsloan/views/individualschedule-edit-view.html',
            controller: 'IndividualScheduleEditController as vm'

        }).state('ifrsloan-individualimpairment-list', {
            url: "/ifrsloan-individualimpairment-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/individualimpairment-list-view.html',
            controller: 'IndividualImpairmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-individualimpairment-edit', {
            url: "/ifrsloan-individualimpairment-edit/:individualimpairmentId",
            templateUrl: rootUrl + 'app/ifrsloan/views/individualimpairment-edit-view.html',
            controller: 'IndividualImpairmentEditController as vm'

        }).state('ifrsloan-impairmentoverride-list', {
            url: "/ifrsloan-impairmentoverride-list",
            templateUrl: rootUrl + 'app/ifrsloan/views/impairmentoverride-list-view.html',
            controller: 'ImpairmentOverrideListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsloan-impairmentoverride-edit', {
            url: "/ifrsloan-impairmentoverride-edit/:impairmentoverrideId",
            templateUrl: rootUrl + 'app/ifrsloan/views/impairmentoverride-edit-view.html',
            controller: 'ImpairmentOverrideEditController as vm'
        }).state('ifrsfi-fairvaluebasismap-list', {
            url: "/ifrsfi-fairvaluebasismap-list",
            templateUrl: rootUrl + 'app/ifrsfi/views/fairvaluebasismap-list-view.html',
            controller: 'FairValueBasisMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsfi-fairvaluebasismap-edit', {
            url: "/ifrsfi-fairvaluebasismap-edit/:fairvaluebasismapId",
            templateUrl: rootUrl + 'app/ifrsfi/views/fairvaluebasismap-edit-view.html',
            controller: 'FairValueBasisMapEditController as vm'
        }).state('ifrsfi-fairvaluebasisexemption-list', {
            url: "/ifrsfi-fairvaluebasisexemption-list",
            templateUrl: rootUrl + 'app/ifrsfi/views/fairvaluebasisexemption-list-view.html',
            controller: 'FairValueBasisExemptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrsfi-fairvaluebasisexemption-edit', {
            url: "/ifrsfi-fairvaluebasisexemption-edit/:fairvaluebasisexemptionId",
            templateUrl: rootUrl + 'app/ifrsfi/views/fairvaluebasisexemption-edit-view.html',
            controller: 'FairValueBasisExemptionEditController as vm'
            //DataView

        }).state('ifrs-bondconsolidateddata-list', {
            url: "/ifrs-bondconsolidateddata-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/bondconsolidateddata-list-view.html',
            controller: 'BondConsolidatedDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-bondconsolidatedothers-list', {
            url: "/ifrs-bondconsolidatedothers-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/bondconsolidateddataothers-list-view.html',
            controller: 'BondConsolidatedDataOthersListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }



        }).state('ifrs-tbillconsolidateddata-list', {
            url: "/ifrs-tbillconsolidateddata-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/tbillconsolidateddata-list-view.html',
            controller: 'TbillConsolidatedDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-loanconsolidateddata-list', {
            url: "/ifrs-loanconsolidateddata-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/loanconsolidateddata-list-view.html',
            controller: 'LoanConsolidatedDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loanconsolidateddatafsdh-list', {
            url: "/ifrs-loanconsolidateddatafsdh-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/loanconsolidateddatafsdh-list-view.html',
            controller: 'LoanConsolidatedDataFSDHListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-bondperiodicschedule-list', {
            url: "/ifrs-bondperiodicschedule-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/bondperiodicschedule-list-view.html',
            controller: 'BondPeriodicScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-bondamortization-list', {
            url: "/ifrs-bondamortization-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/bondcomputation-list-view.html',
            controller: 'BondComputationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-zerocouponbond-list', {
            url: "/ifrs-zerocouponbond-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/bondcomputationresultzero-list-view.html',
            controller: 'BondComputationResultZeroListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-loanperiodicschedule-list', {
            url: "/ifrs-loanperiodicschedule-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/loanperiodicschedule-list-view.html',
            controller: 'LoanPeriodicScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-borrowingperiodicschedule-list', {
            url: "/ifrs-borrowingperiodicschedule-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/borrowingperiodicschedule-list-view.html',
            controller: 'BorrowingPeriodicScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-unmappedproduct-list', {
            url: "/ifrs-unmappedproduct-list",

            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/unmappedifrsproduct-list-view.html',
            controller: 'UnMappedIFRSProductListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loandailyschedule-list', {
            url: "/ifrs-loandailyschedule-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/loanschedule-list-view.html',
            controller: 'LoanScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-borrowingdailyschedule-list', {
            url: "/ifrs-borrowingdailyschedule-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/borrowingschedule-list-view.html',
            controller: 'BorrowingScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loanimpairmentresult-list', {
            url: "/ifrs-loanimpairmentresult-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/loansimpairmentresult-list-view.html',
            controller: 'LoansImpairmentResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-treasurybillscomputation-list', {
            url: "/ifrs-treasurybillscomputation-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/tbillscomputationresult-list-view.html',
            controller: 'TBillsComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-equitystock-list', {
            url: "/ifrs-equitystock-list",
            templateUrl: rootUrl + 'app/IFRSDataView/views/equitystockcomputationresult-list-view.html',
            controller: 'EquityStockComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-bonddata-list', {
            url: "/ifrs-bonddata-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/bonddata-list-view.html',
            controller: 'IFRSBondListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-bonddata-edit', {
            url: "/ifrs-bonddata-edit/:bondId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/bonddata-edit-view.html',
            controller: 'IFRSBondEditController as vm'


        }).state('ifrs-offbalancesheetexposure-list', {
            url: "/ifrs-offbalancesheetexposure-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/offbalancesheetexposure-list-view.html',
            controller: 'OffBalanceSheetExposureListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-offbalancesheetexposure-edit', {
            url: "/ifrs-offbalancesheetexposure-edit/:ObeId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/offbalancesheetexposure-edit-view.html',
            controller: 'OffBalanceSheetExposureEditController as vm'


        }).state('ifrs-tbills-list', {
            url: "/ifrs-tbills-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/tbilldata-list-view.html',
            controller: 'IFRSTbillListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-tbills-edit', {
            url: "/ifrs-tbills-edit/:tbillId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/tbilldata-edit-view.html',
            controller: 'IFRSTbillEditController as vm'



        }).state('ifrs-loandetail-list', {
            url: "/ifrs-loandetail-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loandetail-list-view.html',
            controller: 'LoanDetailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loandetail-edit', {
            url: "/ifrs-loandetail-edit/:loanDetailId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loandetail-edit-view.html',
            controller: 'LoanDetailEditController as vm'

        }).state('ifrs-loanprydata-list', {
            url: "/ifrs-loanprydata-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loanpry-list-view.html',
            controller: 'LoanPryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-ifrscustomer-edit', {
            url: "/ifrs-ifrscustomer-edit/:ifrsCustomerId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/ifrscustomer-edit-view.html',
            controller: 'IfrsCustomerEditController as vm'


        }).state('ifrs-ifrscustomer-list', {
            url: "/ifrs-ifrscustomer-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/ifrscustomer-list-view.html',
            controller: 'IfrsCustomerListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-ifrscustomeraccount-edit', {
            url: "/ifrs-ifrscustomeraccount-edit/:ifrsCustomerAccountId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/ifrscustomeraccount-edit-view.html',
            controller: 'IfrsCustomerAccountEditController as vm'


        }).state('ifrs-ifrscustomeraccount-list', {
            url: "/ifrs-ifrscustomeraccount-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/ifrscustomeraccount-list-view.html',
            controller: 'IfrsCustomerAccountListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loanprydata-edit', {
            url: "/ifrs-loanprydata-edit/:pryId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loanpry-edit-view.html',
            controller: 'LoanPryEditController as vm'



        }).state('ifrs-borrowingdata-list', {
            url: "/ifrs-borrowingdata-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/borrowing-list-view.html',
            controller: 'BorrowingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-borrowingdata-edit', {
            url: "/ifrs-borrowingdata-edit/:borrowingId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/borrowing-edit-view.html',
            controller: 'BorrowingEditController as vm'




        }).state('ifrs-loanprymoratoriumdata-list', {
            url: "/ifrs-loanprymoratoriumdata-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loanprymoratorium-list-view.html',
            controller: 'LoanPryMoratoriumListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-loanprymoratoriumdata-edit', {
            url: "/ifrs-loanprymoratoriumdata-edit/:loanPryMoratoriumId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loanprymoratorium-edit-view.html',
            controller: 'LoanPryMoratoriumEditController as vm'


        }).state('ifrs-integralfee-list', {
            url: "/ifrs-integralfee-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/integralfee-list-view.html',
            controller: 'IntegralFeeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs-integralfee-edit', {
            url: "/ifrs-integralfee-edit/:integralFeeId",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/integralfee-edit-view.html',
            controller: 'IntegralFeeEditController as vm'

        }).state('ifrs-placement-list', {
            url: "/ifrs-placement-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/placement-list-view.html',
            controller: 'PlacementListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-placement-edit', {
            url: "/ifrs-placement-edit/:Placement_Id",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/placement-edit-view.html',
            controller: 'PlacementEditController as vm'

        }).state('ifrs-loaninterestrate-list', {
            url: "/ifrs-loaninterestrate-list",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loaninterestrate-list-view.html',
            controller: 'LoanInterestRateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs-loaninterestrate-edit', {
            url: "/ifrs-loaninterestrate-edit/:LoanInterestRate_Id",
            templateUrl: rootUrl + 'app/ifrs_extracted_data/views/loaninterestrate-edit-view.html',
            controller: 'LoanInterestRateEditController as vm'



        }).state('mpr-teamdefinition-list', {
            url: "/mpr-teamdefinition-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teamdefinition-list-view.html',
            controller: 'TeamDefinitionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-teamdefinition-edit', {
            url: "/mpr-teamdefinition-edit/:teamdefinitionId",
            templateUrl: rootUrl + 'app/mpr_core/views/teamdefinition-edit-view.html',
            controller: 'TeamDefinitionEditController as vm'
        }).state('mpr-teamclassificationtype-list', {
            url: "/mpr-teamclassificationtype-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teamclassificationtype-list-view.html',
            controller: 'TeamClassificationTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('mpr-captionmapping-list', {
            url: "/mpr-captionmapping-list",
            templateUrl: rootUrl + 'app/mpr_core/views/captionmapping-list-view.html',
            controller: 'CaptionMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-captionmapping-edit', {
            url: "/mpr-captionmapping-edit/:captionmappingId",
            templateUrl: rootUrl + 'app/mpr_core/views/captionmapping-edit-view.html',
            controller: 'CaptionMappingEditController as vm'

        }).state('mpr-bsinotherinformation-list', {
            url: "/mpr-bsinotherinformation-list",
            templateUrl: rootUrl + 'app/mpr_core/views/bsinotherinformation-list-view.html',
            controller: 'BSINOtherInformationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-bsinotherinformation-edit', {
            url: "/mpr-bsinotherinformation-edit/:bsinotherinformationId",
            templateUrl: rootUrl + 'app/mpr_core/views/bsinotherinformation-edit-view.html',
            controller: 'BSINOtherInformationEditController as vm'

        }).state('mpr-bsinotherinformationtotalline-list', {
            url: "/mpr-bsinotherinformationtotalline-list",
            templateUrl: rootUrl + 'app/mpr_core/views/bsinotherinformationtotalline-list-view.html',
            controller: 'BSINOtherInformationTotalLineListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-bsinotherinformationtotalline-edit', {
            url: "/mpr-bsinotherinformationtotalline-edit/:bsinotherinformationtotallineId",
            templateUrl: rootUrl + 'app/mpr_core/views/bsinotherinformationtotalline-edit-view.html',
            controller: 'BSINOtherInformationTotalLineEditController as vm'

        }).state('mpr-abcratio-list', {
            url: "/mpr-abcratio-list",
            templateUrl: rootUrl + 'app/mpr_core/views/abcratio-list-view.html',
            controller: 'AbcRatioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-abcratio-edit', {
            url: "/mpr-abcratio-edit/:abcratioId",
            templateUrl: rootUrl + 'app/mpr_core/views/abcratio-edit-view.html',
            controller: 'AbcRatioEditController as vm'

        }).state('mpr-sbu-list', {
            url: "/mpr-sbu-list",
            templateUrl: rootUrl + 'app/mpr_core/views/sbu-list-view.html',
            controller: 'SbuListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-sbu-edit', {
            url: "/mpr-sbu-edit/:sbuId",
            templateUrl: rootUrl + 'app/mpr_core/views/sbu-edit-view.html',
            controller: 'SbuEditController as vm'

        }).state('mpr-sbutype-list', {
            url: "/mpr-sbutype-list",
            templateUrl: rootUrl + 'app/mpr_core/views/sbutype-list-view.html',
            controller: 'SbuTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-sbutype-edit', {
            url: "/mpr-sbutype-edit/:sbutypeId",
            templateUrl: rootUrl + 'app/mpr_core/views/sbutype-edit-view.html',
            controller: 'SbuTypeEditController as vm'

        }).state('mpr-servicese-list', {
            url: "/mpr-servicese-list",
            templateUrl: rootUrl + 'app/mpr_core/views/servicese-list-view.html',
            controller: 'ServiceseListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-servicese-edit', {
            url: "/mpr-servicese-edit/:servicesId",
            templateUrl: rootUrl + 'app/mpr_core/views/servicese-edit-view.html',
            controller: 'ServiceseEditController as vm'

        }).state('mpr-ratios-list', {
            url: "/mpr-ratios-list",
            templateUrl: rootUrl + 'app/mpr_core/views/ratios-list-view.html',
            controller: 'RatiosListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-ratios-edit', {
            url: "/mpr-ratios-edit/:ratiosId",
            templateUrl: rootUrl + 'app/mpr_core/views/ratios-edit-view.html',
            controller: 'RatiosEditController as vm'

        }).state('mpr-ratiocaptionmapping-list', {
            url: "/mpr-ratiocaptionmapping-list",
            templateUrl: rootUrl + 'app/mpr_core/views/ratiocaptionmapping-list-view.html',
            controller: 'RatioCaptionMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-ratiocaptionmapping-edit', {
            url: "/mpr-ratiocaptionmapping-edit/:ratiocaptionmappingId",
            templateUrl: rootUrl + 'app/mpr_core/views/ratiocaptionmapping-edit-view.html',
            controller: 'RatioCaptionMappingEditController as vm'

        }).state('mpr-teamclassificationtype-edit', {
            url: "/mpr-teamclassificationtype-edit/:teamclassificationtypeId",
            templateUrl: rootUrl + 'app/mpr_core/views/teamclassificationtype-edit-view.html',
            controller: 'TeamClassificationTypeEditController as vm'
        }).state('mpr-teamclassification-list', {
            url: "/mpr-teamclassification-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teamclassification-list-view.html',
            controller: 'TeamClassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-teamclassification-edit', {
            url: "/mpr-teamclassification-edit/:teamclassificationId",
            templateUrl: rootUrl + 'app/mpr_core/views/teamclassification-edit-view.html',
            controller: 'TeamClassificationEditController as vm'
        }).state('mpr-team-list', {
            url: "/mpr-team-list",
            templateUrl: rootUrl + 'app/mpr_core/views/team-list-view.html',
            controller: 'TeamListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-team-edit', {
            url: "/mpr-team-edit/:teamId",
            templateUrl: rootUrl + 'app/mpr_core/views/team-edit-view.html',
            controller: 'TeamEditController as vm'
        }).state('mpr-usermis-list', {
            url: "/mpr-usermis-list",
            templateUrl: rootUrl + 'app/mpr_core/views/usermis-list-view.html',
            controller: 'UserMISListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-usermis-edit', {
            url: "/mpr-usermis-edit/:usermisId",
            templateUrl: rootUrl + 'app/mpr_core/views/usermis-edit-view.html',
            controller: 'UserMISEditController as vm'
        }).state('mpr-accounttransferprice-list', {
            url: "/mpr-accounttransferprice-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/accounttransferprice-list-view.html',
            controller: 'AccountTransferPriceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-accounttransferprice-edit', {
            url: "/mpr-accounttransferprice-edit/:accounttransferpriceId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/accounttransferprice-edit-view.html',
            controller: 'AccountTransferPriceEditController as vm'
        }).state('mpr-generaltransferprice-list', {
            url: "/mpr-generaltransferprice-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/generaltransferprice-list-view.html',
            controller: 'GeneralTransferPriceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-generaltransferprice-edit', {
            url: "/mpr-generaltransferprice-edit/:generaltransferpriceId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/generaltransferprice-edit-view.html',
            controller: 'GeneralTransferPriceEditController as vm'
        }).state('mpr-teamclassificationmap-edit', {
            url: "/mpr-teamclassificationmap-edit/:teamId?miscode?definitioncode?teamclassificationmapId",
            templateUrl: rootUrl + 'app/mpr_core/views/teamclassificationmap-edit-view.html',
            controller: 'TeamClassificationMapEditController as vm'
        }).state('mpr-accountofficerdetail-list', {
            url: "/mpr-accountofficerdetail-list",
            templateUrl: rootUrl + 'app/mpr_core/views/accountofficerdetail-list-view.html',
            controller: 'AccountOfficerDetailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-accountofficerdetail-edit', {
            url: "/mpr-accountofficerdetail-edit/:accountofficerdetailId",
            templateUrl: rootUrl + 'app/mpr_core/views/accountofficerdetail-edit-view.html',
            controller: 'AccountOfficerDetailEditController as vm'
        }).state('mpr-branchdefaultmis-list', {
            url: "/mpr-branchdefaultmis-list",
            templateUrl: rootUrl + 'app/mpr_core/views/branchdefaultmis-list-view.html',
            controller: 'BranchDefaultMISListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-branchdefaultmis-edit', {
            url: "/mpr-branchdefaultmis-edit/:branchdefaultmisId",
            templateUrl: rootUrl + 'app/mpr_core/views/branchdefaultmis-edit-view.html',
            controller: 'BranchDefaultMISEditController as vm'
        }).state('mpr-misreplacement-list', {
            url: "/mpr-misreplacement-list",
            templateUrl: rootUrl + 'app/mpr_core/views/misreplacement-list-view.html',
            controller: 'MISReplacementListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-misreplacement-edit', {
            url: "/mpr-misreplacement-edit/:misreplacementId",
            templateUrl: rootUrl + 'app/mpr_core/views/misreplacement-edit-view.html',
            controller: 'MISReplacementEditController as vm'

        }).state('mpr-bsexemption-list', {
            url: "/mpr-bsexemption-list",
            templateUrl: rootUrl + 'app/mpr_core/views/bsexemption-list-view.html',
            controller: 'BSExemptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-bsexemption-edit', {
            url: "/mpr-bsexemption-edit/:bsexemptionId",
            templateUrl: rootUrl + 'app/mpr_core/views/bsexemption-edit-view.html',
            controller: 'BSExemptionEditController as vm'



        }).state('mpr-memoaccountmap-list', {
            url: "/mpr-memoaccountmap-list",
            templateUrl: rootUrl + 'app/mpr_core/views/memoaccountmap-list-view.html',
            controller: 'MemoAccountMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-memoaccountmap-edit', {
            url: "/mpr-memoaccountmap-edit/:memoaccountmapId",
            templateUrl: rootUrl + 'app/mpr_core/views/memoaccountmap-edit-view.html',
            controller: 'MemoAccountMapEditController as vm'

        }).state('mpr-memounit-list', {
            url: "/mpr-memounit-list",
            templateUrl: rootUrl + 'app/mpr_core/views/memounit-list-view.html',
            controller: 'MemoUnitListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-memounit-edit', {
            url: "/mpr-memounit-edit/:memounitId",
            templateUrl: rootUrl + 'app/mpr_core/views/memounit-edit-view.html',
            controller: 'MemoUnitEditController as vm'

        }).state('mpr-memoglmap-list', {
            url: "/mpr-memoglmap-list",
            templateUrl: rootUrl + 'app/mpr_core/views/memoglmap-list-view.html',
            controller: 'MemoGLMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-memoglmap-edit', {
            url: "/mpr-memoglmap-edit/:memounitId",
            templateUrl: rootUrl + 'app/mpr_core/views/memoglmap-edit-view.html',
            controller: 'MemoGLMapEditController as vm'

        }).state('mpr-memoproductmap-list', {
            url: "/mpr-memoproductmap-list",
            templateUrl: rootUrl + 'app/mpr_core/views/memoproductmap-list-view.html',
            controller: 'MemoProductMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-memoproductmap-edit', {
            url: "/mpr-memoproductmap-edit/:memoproductmapId",
            templateUrl: rootUrl + 'app/mpr_core/views/memoproductmap-edit-view.html',
            controller: 'MemoProductMapEditController as vm'


        }).state('mpr-custaccount-list', {
            url: "/mpr-custaccount-list",
            templateUrl: rootUrl + 'app/mpr_core/views/custaccount-list-view.html',
            controller: 'CustAccountListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-bscaption-list', {
            url: "/mpr-bscaption-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/bscaption-list-view.html',
            controller: 'BSCaptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-bscaption-edit', {
            url: "/mpr-bscaption-edit/:bscaptionId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/bscaption-edit-view.html',
            controller: 'BSCaptionEditController as vm'
        }).state('mpr-mprproduct-list', {
            url: "/mpr-mprproduct-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/mprproduct-list-view.html',
            controller: 'MPRProductListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-mprproduct-edit', {
            url: "/mpr-mprproduct-edit/:mprproductId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/mprproduct-edit-view.html',
            controller: 'MPRProductEditController as vm'
        }).state('mpr-bsglmapping-list', {
            url: "/mpr-bsglmapping-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/bsglmapping-list-view.html',
            controller: 'BSGLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-bsglmapping-edit', {
            url: "/mpr-bsglmapping-edit/:bsglmappingId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/bsglmapping-edit-view.html',
            controller: 'BSGLMappingEditController as vm'
        }).state('mpr-nonproductmap-list', {
            url: "/mpr-nonproductmap-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/nonproductmap-list-view.html',
            controller: 'NonProductMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-nonproductmap-edit', {
            url: "/mpr-nonproductmap-edit/:nonproductmapId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/nonproductmap-edit-view.html',
            controller: 'NonProductMapEditController as vm'
        }).state('mpr-nonproductrate-list', {
            url: "/mpr-nonproductrate-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/nonproductrate-list-view.html',
            controller: 'NonProductRateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-nonproductrate-edit', {
            url: "/mpr-nonproductrate-edit/:nonproductrateId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/nonproductrate-edit-view.html',
            controller: 'NonProductRateEditController as vm'
        }).state('mpr-productmis-list', {
            url: "/mpr-productmis-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/productmis-list-view.html',
            controller: 'ProductMISListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-productmis-edit', {
            url: "/mpr-productmis-edit/:productmisId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/productmis-edit-view.html',
            controller: 'ProductMISEditController as vm'
        }).state('mpr-balancesheetthreshold-list', {
            url: "/mpr-balancesheetthreshold-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/balancesheetthreshold-list-view.html',
            controller: 'BalanceSheetThresholdListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-balancesheetthreshold-edit', {
            url: "/mpr-balancesheetthreshold-edit/:balancesheetthresholdId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/balancesheetthreshold-edit-view.html',
            controller: 'BalanceSheetThresholdEditController as vm'
        }).state('mpr-balancesheetbudget-list', {
            url: "/mpr-balancesheetbudget-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/balancesheetbudget-list-view.html',
            controller: 'BalanceSheetBudgetListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-balancesheetbudget-edit', {
            url: "/mpr-balancesheetbudget-edit/:budgetId?budgettype",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/balancesheetbudget-edit-view.html',
            controller: 'BalanceSheetBudgetEditController as vm'
        }).state('mpr-plbudget-list', {
            url: "/mpr-plbudget-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/plbudget-list-view.html',
            controller: 'PLBudgetListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-plbudget-edit', {
            url: "/mpr-plbudget-edit/:budgetId?budgettype",
            templateUrl: rootUrl + 'app/mpr_pl/views/plbudget-edit-view.html',
            controller: 'PLBudgetEditController as vm'

        }).state('mpr-processdata-list', {
            url: "/mpr-processdata-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/processdata-list-view.html',
            controller: 'ProcessDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-processdata-edit', {
            url: "/mpr-processdata-edit/:processdataId",
            templateUrl: rootUrl + 'app/mpr_pl/views/processdata-edit-view.html',
            controller: 'ProcessDataEditController as vm'



        }).state('mpr-managementtree-list', {
            url: "/mpr-managementtree-list",
            templateUrl: rootUrl + 'app/mpr_core/views/managementtree-list-view.html',
            controller: 'ManagementTreeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-managementtree-edit', {
            url: "/mpr-managementtree-edit/:managementtreeId",
            templateUrl: rootUrl + 'app/mpr_core/views/managementtree-edit-view.html',
            controller: 'ManagementTreeEditController as vm'
        }).state('mpr-accountmis-list', {
            url: "/mpr-accountmis-list",
            templateUrl: rootUrl + 'app/mpr_core/views/accountmis-list-view.html',
            controller: 'AccountMISListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-accountmis-edit', {
            url: "/mpr-accountmis-edit/:accountmisId",
            templateUrl: rootUrl + 'app/mpr_core/views/accountmis-edit-view.html',
            controller: 'AccountMISEditController as vm'

        }).state('mpr-mprsetup-edit', {
            url: "/mpr-mprsetup-edit/:accountmisId",
            templateUrl: rootUrl + 'app/mpr_core/views/mprsetup-edit-view.html',
            controller: 'MPRSetupEditController as vm'
        }).state('mpr-transferprice-list', {
            url: "/mpr-transferprice-list",
            templateUrl: rootUrl + 'app/mpr_core/views/transferprice-list-view.html',
            controller: 'TransferPriceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-transferprice-edit', {
            url: "/mpr-transferprice-edit/:transferpriceId",
            templateUrl: rootUrl + 'app/mpr_core/views/transferprice-edit-view.html',
            controller: 'TransferPriceEditController as vm'




        }).state('mpr-mprbalancesheet-list', {
            url: "/mpr-mprbalancesheet-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/mprbalancesheet-list-view.html',
            controller: 'MPRBalanceSheetListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-mprbalancesheet-edit', {
            url: "/mpr-mprbalancesheet-edit/:balancesheetId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/mprbalancesheet-edit-view.html',
            controller: 'MPRBalanceSheetEditController as vm'



        }).state('mpr-mprbalancesheetadjustment-list', {
            url: "/mpr-mprbalancesheetadjustment-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/mprbalancesheetadjustment-list-view.html',
            controller: 'MPRBalanceSheetAdjustmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-mprbalancesheetadjustment-edit', {
            url: "/mpr-mprbalancesheetadjustment-edit/:mprbalancesheetadjustmentId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/mprbalancesheetadjustment-edit-view.html',
            controller: 'MPRBalanceSheetAdjustmentEditController as vm'
        }).state('mpr-unmappedproduct-list', {
            url: "/mpr-unmappedproduct-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/unmappedproduct-list-view.html',
            controller: 'UnMappedProductListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-plcaption-list', {
            url: "/mpr-plcaption-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/plcaption-list-view.html',
            controller: 'PLCaptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-plcaption-edit', {
            url: "/mpr-plcaption-edit/:plcaptionId",
            templateUrl: rootUrl + 'app/mpr_pl/views/plcaption-edit-view.html',
            controller: 'PLCaptionEditController as vm'
        }).state('mpr-mprglmapping-list', {
            url: "/mpr-mprglmapping-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/mprglmapping-list-view.html',
            controller: 'MPRGLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-mprglmapping-edit', {
            url: "/mpr-mprglmapping-edit/:mprglmappingId",
            templateUrl: rootUrl + 'app/mpr_pl/views/mprglmapping-edit-view.html',
            controller: 'MPRGLMappingEditController as vm'
        }).state('mpr-unmappedgl-list', {
            url: "/mpr-unmappedgl-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/unmappedgl-list-view.html',
            controller: 'UnMappedPLGLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-glreclassification-list', {
            url: "/mpr-glreclassification-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/glreclassification-list-view.html',
            controller: 'GLReclassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-glreclassification-edit', {
            url: "/mpr-glreclassification-edit/:glreclassificationId",
            templateUrl: rootUrl + 'app/mpr_pl/views/glreclassification-edit-view.html',
            controller: 'GLReclassificationEditController as vm'
        }).state('mpr-glexception-list', {
            url: "/mpr-glexception-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/glexception-list-view.html',
            controller: 'GLExceptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-glexception-edit', {
            url: "/mpr-glexception-edit/:glexceptionId",
            templateUrl: rootUrl + 'app/mpr_pl/views/glexception-edit-view.html',
            controller: 'GLExceptionEditController as vm'
        }).state('mpr-glmis-list', {
            url: "/mpr-glmis-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/glmis-list-view.html',
            controller: 'GLMISListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-glmis-edit', {
            url: "/mpr-glmis-edit/:glmisId",
            templateUrl: rootUrl + 'app/mpr_pl/views/glmis-edit-view.html',
            controller: 'GLMISEditController as vm'



        }).state('mpr-revenue-list', {
            url: "/mpr-revenue-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/revenue-list-view.html',
            controller: 'RevenueListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-revenue-edit', {
            url: "/mpr-revenue-edit/:revenueId",
            templateUrl: rootUrl + 'app/mpr_pl/views/revenue-edit-view.html',
            controller: 'RevenueEditController as vm'



        }).state('mpr-mprcommfee-list', {
            url: "/mpr-mprcommfee-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/mprcommfee-list-view.html',
            controller: 'MPRCommFeeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-mprcommfee-edit', {
            url: "/mpr-mprcommfee-edit/:CommFee_Id",
            templateUrl: rootUrl + 'app/mpr_pl/views/mprcommfee-edit-view.html',
            controller: 'MPRCommFeeEditController as vm'


        }).state('mpr-plincomereportadjustment-list', {
            url: "/mpr-plincomereportadjustment-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/plincomereportadjustment-list-view.html',
            controller: 'PLIncomeReportAdjustmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-plincomereportadjustment-edit', {
            url: "/mpr-plincomereportadjustment-edit/:plincomereportadjustmentId",
            templateUrl: rootUrl + 'app/mpr_pl/views/plincomereportadjustment-edit-view.html',
            controller: 'PLIncomeReportAdjustmentEditController as vm'

        }).state('mpr-costcentredefinition-list', {
            url: "/mpr-costcentredefinition-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/costcentredefinition-list-view.html',
            controller: 'CostCentreDefinitionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-costcentredefinition-edit', {
            url: "/mpr-costcentredefinition-edit/:ccdefinitionId",
            templateUrl: rootUrl + 'app/mpr_opex/views/costcentredefinition-edit-view.html',
            controller: 'CostCentreDefinitionEditController as vm'

        }).state('mpr-costcentre-list', {
            url: "/mpr-costcentre-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/costcentre-list-view.html',
            controller: 'CostCentreListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-costcentre-edit', {
            url: "/mpr-costcentre-edit/:costcentreId",
            templateUrl: rootUrl + 'app/mpr_opex/views/costcentre-edit-view.html',
            controller: 'CostCentreEditController as vm'

        }).state('mpr-expensebasis-list', {
            url: "/mpr-expensebasis-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/expensebasis-list-view.html',
            controller: 'ExpenseBasisListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-expensebasis-edit', {
            url: "/mpr-expensebasis-edit/:expensebasisId",
            templateUrl: rootUrl + 'app/mpr_opex/views/expensebasis-edit-view.html',
            controller: 'ExpenseBasisEditController as vm'

        }).state('mpr-expensemapping-list', {
            url: "/mpr-expensemapping-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/expensemapping-list-view.html',
            controller: 'ExpenseMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-expensemapping-edit', {
            url: "/mpr-expensemapping-edit/:expensemappingId",
            templateUrl: rootUrl + 'app/mpr_opex/views/expensemapping-edit-view.html',
            controller: 'ExpenseMappingEditController as vm'

        }).state('mpr-expenseproductmapping-list', {
            url: "/mpr-expenseproductmapping-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/expenseproductmapping-list-view.html',
            controller: 'ExpenseProductMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-expenseproductmapping-edit', {
            url: "/mpr-expenseproductmapping-edit/:expenseproductId",
            templateUrl: rootUrl + 'app/mpr_opex/views/expenseproductmapping-edit-view.html',
            controller: 'ExpenseProductMappingEditController as vm'

        }).state('mpr-expenseglmapping-list', {
            url: "/mpr-expenseglmapping-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/expenseglmapping-list-view.html',
            controller: 'ExpenseGLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-expenseglmapping-edit', {
            url: "/mpr-expenseglmapping-edit/:expenseglId",
            templateUrl: rootUrl + 'app/mpr_opex/views/expenseglmapping-edit-view.html',
            controller: 'ExpenseGLMappingEditController as vm'

        }).state('mpr-expenserawbasis-list', {
            url: "/mpr-expenserawbasis-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/expenserawbasis-list-view.html',
            controller: 'ExpenseRawBasisListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-expenserawbasis-edit', {
            url: "/mpr-expenserawbasis-edit/:expenserawbasisId",
            templateUrl: rootUrl + 'app/mpr_opex/views/expenserawbasis-edit-view.html',
            controller: 'ExpenseRawBasisEditController as vm'

        }).state('mpr-opexrawexpense-list', {
            url: "/mpr-opexrawexpense-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexrawexpense-list-view.html',
            controller: 'OpexRawExpenseListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexrawexpense-edit', {
            url: "/mpr-opexrawexpense-edit/:expenserawbasisId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexrawexpense-edit-view.html',
            controller: 'OpexRawExpenseEditController as vm'

        }).state('mpr-staffcost-list', {
            url: "/mpr-staffcost-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/staffcost-list-view.html',
            controller: 'StaffCostListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-staffcost-edit', {
            url: "/mpr-staffcost-edit/:staffcostId",
            templateUrl: rootUrl + 'app/mpr_opex/views/staffcost-edit-view.html',
            controller: 'StaffCostEditController as vm'

        }).state('mpr-opexmanagementtree-list', {
            url: "/mpr-opexmanagementtree-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmanagementtree-list-view.html',
            controller: 'OpexManagementTreeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexmanagementtree-edit', {
            url: "/mpr-opexmanagementtree-edit/:opexmgtTreeId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmanagementtree-edit-view.html',
            controller: 'OpexManagementTreeEditController as vm'

        }).state('mpr-activitybase-list', {
            url: "/mpr-activitybase-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/activitybase-list-view.html',
            controller: 'ActivityBaseListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-activitybase-edit', {
            url: "/mpr-activitybase-edit/:activitybaseId",
            templateUrl: rootUrl + 'app/mpr_opex/views/activitybase-edit-view.html',
            controller: 'ActivityBaseEditController as vm'

        }).state('mpr-activitybaseratio-list', {
            url: "/mpr-activitybaseratio-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/activitybaseratio-list-view.html',
            controller: 'ActivityBaseRatioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-activitybaseratio-edit', {
            url: "/mpr-activitybaseratio-edit/:activitybaseratioId",
            templateUrl: rootUrl + 'app/mpr_opex/views/activitybaseratio-edit-view.html',
            controller: 'ActivityBaseRatioEditController as vm'


        }).state('mpr-fixedassetsharingratio-list', {
            url: "/mpr-fixedassetsharingratio-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/fixedassetsharingratio-list-view.html',
            controller: 'FixedAssetSharingRatioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-fixedassetsharingratio-edit', {
            url: "/mpr-fixedassetsharingratio-edit/:fixedAssetSharingRatioId",
            templateUrl: rootUrl + 'app/mpr_opex/views/fixedassetsharingratio-edit-view.html',
            controller: 'FixedAssetSharingRatioEditController as vm'



        }).state('mpr-incomecentralvaultschedule-list', {
            url: "/mpr-incomecentralvaultschedule-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomecentralvaultschedule-list-view.html',
            controller: 'IncomeCentralVaultScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-incomecentralvaultschedule-edit', {
            url: "/mpr-incomecentralvaultschedule-edit/:incomeCentralVaultScheduleId",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomecentralvaultschedule-edit-view.html',
            controller: 'IncomeCentralVaultScheduleEditController as vm'
            /////hhhhhhhhhhhhhhhhhh/////
        }).state('mpr-incomecashcentrecode-list', {
            url: "/mpr-incomecashcentrecode-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/incomecashcentrecode-list-view.html',
            controller: 'IncomeCashCentreCodeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-incomecashcentrecode-edit', {
            url: "/mpr-incomecashcentrecode-edit/:incomecashcentrecodeId",
            templateUrl: rootUrl + 'app/mpr_opex/views/incomecashcentrecode-edit-view.html',
            controller: 'IncomeCashCentreCodeEditController as vm'


        }).state('mpr-incomeneaglsbu-list', {
            url: "/mpr-incomeneaglsbu-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/incomeneaglsbu-list-view.html',
            controller: 'IncomeNEAGLSBUListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-incomeneaglsbu-edit', {
            url: "/mpr-incomeneaglsbu-edit/:incomeneaglsbuId",
            templateUrl: rootUrl + 'app/mpr_opex/views/incomeneaglsbu-edit-view.html',
            controller: 'IncomeNEAGLSBUEditController as vm'

            //}).state('mpr-categorytransferprice-list', {
            //    url: "/mpr-categorytransferprice-list",
            //    templateUrl: rootUrl + 'app/mpr_opex/views/categorytransferprice-list-view.html',
            //    controller: 'CategoryTransferPriceListController as vm',
            //    resolve: {
            //        loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
            //            return $ocLazyLoad.load({
            //                files: [
            //                    rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
            //                    rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
            //                    rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
            //            });
            //        }]
            //    }
            //}).state('mpr-categorytransferprice-edit', {
            //    url: "/mpr-categorytransferprice-edit/:categorytransferpriceId",
            //    templateUrl: rootUrl + 'app/mpr_opex/views/categorytransferprice-edit-view.html',
            //    controller: 'CategoryTransferPriceEditController as vm'


        }).state('mpr-neabranchsbushares-list', {
            url: "/mpr-neabranchsbushares-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/neabranchsbushares-list-view.html',
            controller: 'NEABranchSBUSharesListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-neabranchsbushares-edit', {
            url: "/mpr-neabranchsbushares-edit/:neabranchsbusharesId",
            templateUrl: rootUrl + 'app/mpr_opex/views/neabranchsbushares-edit-view.html',
            controller: 'NEABranchSBUSharesEditController as vm'


        }).state('mpr-opexabcexemption-list', {
            url: "/mpr-opexabcexemption-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexabcexemption-list-view.html',
            controller: 'OpexAbcExemptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexabcexemption-edit', {
            url: "/mpr-opexabcexemption-edit/:opexabcexemptionId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexabcexemption-edit-view.html',
            controller: 'OpexAbcExemptionEditController as vm'


        }).state('mpr-neabranchsharingratio-list', {
            url: "/mpr-neabranchsharingratio-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/neabranchsharingratio-list-view.html',
            controller: 'NEABranchSharingRatioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-neabranchsharingratio-edit', {
            url: "/mpr-neabranchsharingratio-edit/:neabranchsharingratioId",
            templateUrl: rootUrl + 'app/mpr_opex/views/neabranchsharingratio-edit-view.html',
            controller: 'NEABranchSharingRatioEditController as vm'


        }).state('mpr-neasharingratio-list', {
            url: "/mpr-neasharingratio-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/neasharingratio-list-view.html',
            controller: 'NEASharingRatioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-neasharingratio-edit', {
            url: "/mpr-neasharingratio-edit/:neasharingratioId",
            templateUrl: rootUrl + 'app/mpr_opex/views/neasharingratio-edit-view.html',
            controller: 'NEASharingRatioEditController as vm'


        }).state('mpr-neasharingratiofcy-list', {
            url: "/mpr-neasharingratiofcy-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/neasharingratiofcy-list-view.html',
            controller: 'NEASharingRatioFcyListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-neasharingratiofcy-edit', {
            url: "/mpr-neasharingratiofcy-edit/:neasharingratiofcyId",
            templateUrl: rootUrl + 'app/mpr_opex/views/neasharingratiofcy-edit-view.html',
            controller: 'NEASharingRatioFcyEditController as vm'


        }).state('mpr-opexbranchmapping-list', {
            url: "/mpr-opexbranchmapping-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexbranchmapping-list-view.html',
            controller: 'OpexBranchMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexbranchmapping-edit', {
            url: "/mpr-opexbranchmapping-edit/:opexbranchmappingId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexbranchmapping-edit-view.html',
            controller: 'OpexBranchMappingEditController as vm'


        }).state('mpr-lowcostremap-list', {
            url: "/mpr-lowcostremap-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/lowcostremap-list-view.html',
            controller: 'LowCostRemapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-lowcostremap-edit', {
            url: "/mpr-lowcostremap-edit/:lowcostremapId",
            templateUrl: rootUrl + 'app/mpr_opex/views/lowcostremap-edit-view.html',
            controller: 'LowCostRemapEditController as vm'



        }).state('mpr-opexmisreplacement-list', {
            url: "/mpr-opexmisreplacement-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmisreplacement-list-view.html',
            controller: 'OpexMISReplacementListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexmisreplacement-edit', {
            url: "/mpr-opexmisreplacement-edit/:opexmisreplacementId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmisreplacement-edit-view.html',
            controller: 'OpexMISReplacementEditController as vm'
        }).state('mpr-opexbusinessrule-list', {
            url: "/mpr-opexbusinessrule-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexbusinessrule-list-view.html',
            controller: 'OpexBusinessRuleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexbusinessrule-edit', {
            url: "/mpr-opexbusinessrule-edit/:opexbusinessruleId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexbusinessrule-edit-view.html',
            controller: 'OpexBusinessRuleEditController as vm'
        }).state('opex-opexglmapping-list', {
            url: "/opex-opexglmapping-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexglmapping-list-view.html',
            controller: 'OpexGLMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('opex-opexglmapping-edit', {
            url: "/opex-opexglmapping-edit/:glmappingId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexglmapping-edit-view.html',
            controller: 'OpexGLMappingEditController as vm'
        }).state('opex-unmappedgl-list', {
            url: "/opex-unmappedgl-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/unmappedgl-list-view.html',
            controller: 'UnMappedOpexGLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }



        }).state('mpr-opexreport-list', {
            url: "/mpr-opexreport-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexreport-list-view.html',
            controller: 'OpexReportListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-opexchecklist-list', {
            url: "/mpr-opexchecklist-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/checklist-list-view.html',
            controller: 'CheckListListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexglbasis-list', {
            url: "/mpr-opexglbasis-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexglbasis-list-view.html',
            controller: 'OpexGLBasisListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexglbasis-edit', {
            url: "/mpr-opexglbasis-edit/:opexglbasisId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexglbasis-edit-view.html',
            controller: 'OpexGLBasisEditController as vm'

        }).state('mpr-opexbasismapping-list', {
            url: "/mpr-opexbasismapping-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexbasismapping-list-view.html',
            controller: 'OpexBasisMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-opexbasismapping-edit', {
            url: "/mpr-opexbasismapping-edit/:opexbasismappingId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexbasismapping-edit-view.html',
            controller: 'OpexBasisMappingEditController as vm'



        }).state('mpr-hoexemptionmiscode-list', {
            url: "/mpr-hoexemptionmiscode-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/hoexemptionmiscode-list-view.html',
            controller: 'HoExemptionMISCodeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('mpr-hoexemptionmiscode-edit', {
            url: "/mpr-hoexemptionmiscode-edit/:id",
            templateUrl: rootUrl + 'app/mpr_opex/views/hoexemptionmiscode-edit-view.html',
            controller: 'HoExemptionMISCodeEditController as vm'


        }).state('mpr-messagingsubscription-edit', {
            url: "/mpr-messagingsubscription-edit/:messagingSubscriptionId",
            templateUrl: rootUrl + 'app/mpr_core/views/messagingsubscription-edit-view.html',
            controller: 'MessagingSubscriptionEditController as vm'
        })
        //CDQM
        .state('cdqm-address-list', {
            url: "/cdqm-address-list",
            templateUrl: rootUrl + 'app/cdqm/views/address-list-view.html',
            controller: 'AddressListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-address-edit', {
            url: "/cdqm-address-edit/:addressId",
            templateUrl: rootUrl + 'app/cdqm/views/address-edit-view.html',
            controller: 'AddressEditController as vm'
        }).state('cdqm-country-list', {
            url: "/cdqm-country-list",
            templateUrl: rootUrl + 'app/cdqm/views/country-list-view.html',
            controller: 'CDQMCountryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-country-edit', {
            url: "/cdqm-country-edit/:countryId",
            templateUrl: rootUrl + 'app/cdqm/views/country-edit-view.html',
            controller: 'CDQMCountryEditController as vm'
        }).state('cdqm-gendergroup-list', {
            url: "/cdqm-gendergroup-list",
            templateUrl: rootUrl + 'app/cdqm/views/gendergroup-list-view.html',
            controller: 'GenderGroupListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-gendergroup-edit', {
            url: "/cdqm-gendergroup-edit/:gendergroupId",
            templateUrl: rootUrl + 'app/cdqm/views/gendergroup-edit-view.html',
            controller: 'GenderGroupEditController as vm'
        }).state('cdqm-merchant-list', {
            url: "/cdqm-merchant-list",
            templateUrl: rootUrl + 'app/cdqm/views/merchant-list-view.html',
            controller: 'MerchantListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-merchant-edit', {
            url: "/cdqm-merchant-edit/:merchantId",
            templateUrl: rootUrl + 'app/cdqm/views/merchant-edit-view.html',
            controller: 'MerchantEditController as vm'
        }).state('cdqm-title-list', {
            url: "/cdqm-title-list",
            templateUrl: rootUrl + 'app/cdqm/views/title-list-view.html',
            controller: 'TitleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-title-edit', {
            url: "/cdqm-title-edit/:titleId",
            templateUrl: rootUrl + 'app/cdqm/views/title-edit-view.html',
            controller: 'TitleEditController as vm'
        }).state('cdqm-etlconfiguration-list', {
            url: "/cdqm-etlconfiguration-list",
            templateUrl: rootUrl + 'app/cdqm/views/etlconfiguration-list-view.html',
            controller: 'ETLConfigurationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-etlconfiguration-edit', {
            url: "/cdqm-etlconfiguration-edit/:etlconfigurationId",
            templateUrl: rootUrl + 'app/cdqm/views/etlconfiguration-edit-view.html',
            controller: 'ETLConfigurationEditController as vm'
        }).state('cdqm-product-list', {
            url: "/cdqm-product-list",
            templateUrl: rootUrl + 'app/cdqm/views/product-list-view.html',
            controller: 'CDQMProductListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-product-edit', {
            url: "/cdqm-product-edit/:productId",
            templateUrl: rootUrl + 'app/cdqm/views/product-edit-view.html',
            controller: 'CDQMProductEditController as vm'
        }).state('cdqm-customermis-list', {
            url: "/cdqm-customermis-list",
            templateUrl: rootUrl + 'app/cdqm/views/customermis-list-view.html',
            controller: 'CustomerMISListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('cdqm-customermis-edit', {
            url: "/cdqm-customermis-edit/:customermisId",
            templateUrl: rootUrl + 'app/cdqm/views/customermis-edit-view.html',
            controller: 'CustomerMISEditController as vm'
        }).state('cdqm-customercheck-list', {
            url: "/cdqm-customercheck-list",
            templateUrl: rootUrl + 'app/cdqm/views/customercheck-list-view.html',
            controller: 'CustomerCheckListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


            //IFRS9



            //Begin Victor Update

        })

        .state('ifrs9-eurobondspread-list', {
            url: "/ifrs9-eurobondspread-list",
            templateUrl: rootUrl + 'app/IFRS9/views/eurobondspread-list-view.html',
            controller: 'EuroBondSpreadListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-eurobondspread-edit', {
            url: "/ifrs9-eurobondspread-edit/:eurobondspreadId",
            templateUrl: rootUrl + 'app/IFRS9/views/eurobondspread-edit-view.html',
            controller: 'EuroBondSpreadEditController as vm'
        }).state('ifrs9-placementcomputationresult-list', {
            url: "/ifrs9-placementcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/placementcomputationresult-list-view.html',
            controller: 'PlacementComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-lgdcomputationresult-list', {
            url: "/ifrs9-lgdcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/lgdcomputationresult-list-view.html',
            controller: 'LgdComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-localbondspread-list', {
            url: "/ifrs9-localbondspread-list",
            templateUrl: rootUrl + 'app/IFRS9/views/localbondspread-list-view.html',
            controller: 'LocalBondSpreadListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-localbondspread-edit', {
            url: "/ifrs9-localbondspread-edit/:localbondspreadId",
            templateUrl: rootUrl + 'app/IFRS9/views/localbondspread-edit-view.html',
            controller: 'LocalBondSpreadEditController as vm'
        }).state('ifrs9-marginalpddistribution-list', {
            url: "/ifrs9-marginalpddistribution-list",
            templateUrl: rootUrl + 'app/IFRS9/views/marginalpddistribution-list-view.html',
            controller: 'MarginalPDDistributionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-bondmarginalpddistribution-list', {
            url: "/ifrs9-bondmarginalpddistribution-list",
            templateUrl: rootUrl + 'app/IFRS9/views/bondmarginalpddistribution-list-view.html',
            controller: 'BondMarginalPDDistributionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-marginalpddistributionplacement-list', {
            url: "/ifrs9-marginalpddistributionplacement-list",
            templateUrl: rootUrl + 'app/IFRS9/views/marginalpddistributionplacement-list-view.html',
            controller: 'MarginalPDDistributionPlacementListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-eclcomputationresult-list', {
            url: "/ifrs9-eclcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/eclcomputationresult-list-view.html',
            controller: 'EclComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-bondeclcomputationresult-list', {
            url: "/ifrs9-bondeclcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/bondeclcomputationresult-list-view.html',
            controller: 'BondEclComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-placementeclcomputationresult-list', {
            url: "/ifrs9-placementeclcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/placementeclcomputationresult-list-view.html',
            controller: 'PlacementEclComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-lcbgeclcomputationresult-list', {
            url: "/ifrs9-lcbgeclcomputationresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/lcbgeclcomputationresult-list-view.html',
            controller: 'LcBgEclComputationResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        })




        //End Victor Update


        .state('ifrs9-externalrating-list', {
            url: "/ifrs9-externalrating-list",
            templateUrl: rootUrl + 'app/IFRS9/views/externalrating-list-view.html',
            controller: 'ExternalRatingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-externalrating-edit', {
            url: "/ifrs9-externalrating-edit/:externalratingId",
            templateUrl: rootUrl + 'app/IFRS9/views/externalrating-edit-view.html',
            controller: 'ExternalRatingEditController as vm'

        }).state('ifrs9-historicalsectorrating-list', {
            url: "/ifrs9-historicalsectorrating-list",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalsectorrating-list-view.html',
            controller: 'HistoricalSectorRatingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-historicalsectorrating-edit', {
            url: "/ifrs9-historicalsectorrating-edit/:historicalsectorratingId",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalsectorrating-edit-view.html',
            controller: 'HistoricalSectorRatingEditController as vm'



        }).state('ifrs9-internalratingbased-list', {
            url: "/ifrs9-internalratingbased-list",
            templateUrl: rootUrl + 'app/IFRS9/views/internalratingbased-list-view.html',
            controller: 'InternalRatingBasedListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-internalratingbased-edit', {
            url: "/ifrs9-internalratingbased-edit/:internalratingbasedId",
            templateUrl: rootUrl + 'app/IFRS9/views/internalratingbased-edit-view.html',
            controller: 'InternalRatingBasedEditController as vm'

        }).state('ifrs9-macroeconomic-list', {
            url: "/ifrs9-macroeconomic-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomic-list-view.html',
            controller: 'MacroEconomicListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-macroeconomic-edit', {
            url: "/ifrs9-macroeconomic-edit/:macroeconomicId",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomic-edit-view.html',
            controller: 'MacroEconomicEditController as vm'



        }).state('ifrs9-computedforcastedpdlgd-list', {
            url: "/ifrs9-computedforcastedpdlgd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/computedforcastedpdlgd-list-view.html',
            controller: 'ComputedForcastedPDLGDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-computedforcastedpdlgdforeign-list', {
            url: "/ifrs9-computedforcastedpdlgdforeign-list",
            templateUrl: rootUrl + 'app/IFRS9/views/computedforcastedpdlgdforeign-list-view.html',
            controller: 'ComputedForcastedPDLGDForeignListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-macroeconomicsnpl-list', {
            url: "/ifrs9-macroeconomicsnpl-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomicsnpl-list-view.html',
            controller: 'MacroEconomicsNPLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-macroeconomicsnpl-edit', {
            url: "/ifrs9-macroeconomicsnpl-edit/:macroeconomicnplId",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomicsnpl-edit-view.html',
            controller: 'MacroEconomicsNPLEditController as vm'


        }).state('ifrs9-monthlydiscountfactor-list', {
            url: "/ifrs9-monthlydiscountfactor-list",
            templateUrl: rootUrl + 'app/IFRS9/views/monthlydiscountfactor-list-view.html',
            controller: 'MonthlyDiscountFactorListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-monthlydiscountfactorplacement-list', {
            url: "/ifrs9-monthlydiscountfactorplacement-list",
            templateUrl: rootUrl + 'app/IFRS9/views/monthlydiscountfactorplacement-list-view.html',
            controller: 'MonthlyDiscountFactorPlacementListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-monthlydiscountfactorbond-list', {
            url: "/ifrs9-monthlydiscountfactorbond-list",
            templateUrl: rootUrl + 'app/IFRS9/views/monthlydiscountfactorbond-list-view.html',
            controller: 'MonthlyDiscountFactorBondListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-ratingmapping-list', {
            url: "/ifrs9-ratingmapping-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ratingmapping-list-view.html',
            controller: 'RatingMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-ratingmapping-edit', {
            url: "/ifrs9-ratingmapping-edit/:ratingmappingId",
            templateUrl: rootUrl + 'app/IFRS9/views/ratingmapping-edit-view.html',
            controller: 'RatingMappingEditController as vm'


        }).state('ifrs9-transition-list', {
            url: "/ifrs9-transition-list",
            templateUrl: rootUrl + 'app/IFRS9/views/transition-list-view.html',
            controller: 'TransitionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-transition-edit', {
            url: "/ifrs9-transition-edit/:transitionId",
            templateUrl: rootUrl + 'app/IFRS9/views/transition-edit-view.html',
            controller: 'TransitionEditController as vm'

        }).state('ifrs9-sector-list', {
            url: "/ifrs9-sector-list",
            templateUrl: rootUrl + 'app/IFRS9/views/sector-list-view.html',
            controller: 'SectorListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-sector-edit', {
            url: "/ifrs9-sector-edit/:sectorId",
            templateUrl: rootUrl + 'app/IFRS9/views/sector-edit-view.html',
            controller: 'SectorEditController as vm'


        }).state('ifrs9-historicalclassification-list', {
            url: "/ifrs9-historicalclassification-list",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalclassification-list-view.html',
            controller: 'HistoricalClassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-historicalclassification-edit', {
            url: "/ifrs9-historicalclassification-edit/:historicalclassificationId",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalclassification-edit-view.html',
            controller: 'HistoricalClassificationEditController as vm'


        }).state('ifrs9-macroeconomichistorical-list', {
            url: "/ifrs9-macroeconomichistorical-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomichistorical-list-view.html',
            controller: 'MacroEconomicHistoricalListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-macroeconomichistorical-edit', {
            url: "/ifrs9-macroeconomichistorical-edit/:macroeconomichistoricalId",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomichistorical-edit-view.html',
            controller: 'MacroEconomicHistoricalEditController as vm'


        }).state('ifrs9-setup-list', {
            url: "/ifrs9-setup-list",
            templateUrl: rootUrl + 'app/IFRS9/views/setup-list-view.html',
            controller: 'SetUpListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-setup-edit', {
            url: "/ifrs9-setup-edit/:setupId",
            templateUrl: rootUrl + 'app/IFRS9/views/setup-edit-view.html',
            controller: 'SetUpEditController as vm'


        }).state('ifrs9-notchdifference-list', {
            url: "/ifrs9-notchdifference-list",
            templateUrl: rootUrl + 'app/IFRS9/views/notchdifference-list-view.html',
            controller: 'NotchDifferenceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-notchdifference-edit', {
            url: "/ifrs9-notchdifference-edit/:notchdifferenceId",
            templateUrl: rootUrl + 'app/IFRS9/views/notchdifference-edit-view.html',
            controller: 'NotchDifferenceEditController as vm'

        }).state('ifrs9-historicalsectorialpd-list', {
            url: "/ifrs9-historicalsectorialpd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalsectorialpd-list-view.html',
            controller: 'HistoricalSectorialPDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-historicalsectoriallgd-edit', {
            url: "/ifrs9-historicalsectoriallgd-edit/:historicalsectoriallgdId",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalsectoriallgd-edit-view.html',
            controller: 'HistoricalSectorialLGDEditController as vm'

        }).state('ifrs9-historicalsectoriallgd-list', {
            url: "/ifrs9-historicalsectoriallgd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalsectoriallgd-list-view.html',
            controller: 'HistoricalSectorialLGDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-historicalsectorialpd-edit', {
            url: "/ifrs9-historicalsectorialpd-edit/:historicalsectorialpdId",
            templateUrl: rootUrl + 'app/IFRS9/views/historicalsectorialpd-edit-view.html',
            controller: 'HistoricalSectorialPDEditController as vm'



        }).state('ifrs9-sectorialregressedpd-list', {
            url: "/ifrs9-sectorialregressedpd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/sectorialregressedpd-list-view.html',
            controller: 'SectorialRegressedPDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-sectorialregressedlgd-list', {
            url: "/ifrs9-sectorialregressedlgd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/sectorialregressedlgd-list-view.html',
            controller: 'SectorialRegressedLGDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

            //}).state('ifrs9-computedforcastedpdlgd-list', {
            //    url: "/ifrs9-computedforcastedpdlgd-list",
            //    templateUrl: rootUrl + 'app/IFRS9/views/computedforcastedpdlgd-list-view.html',
            //    controller: 'ComputedForcastedPDLGDListController as vm',
            //    resolve: {
            //        loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
            //            return $ocLazyLoad.load({
            //                files: [
            //                      rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
            //                       rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
            //                       rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
            //            });
            //        }]
            //    }

        }).state('ifrs9-macroeconomicvariable-list', {
            url: "/ifrs9-macroeconomicvariable-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomicvariable-list-view.html',
            controller: 'MacroEconomicVariableListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-macroeconomicvariable-edit', {
            url: "/ifrs9-macroeconomicvariable-edit/:macroeconomicvariableId",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomicvariable-edit-view.html',
            controller: 'MacroEconomicVariableEditController as vm'


        }).state('ifrs9-sectorvariablemapping-list', {
            url: "/ifrs9-sectorvariablemapping-list",
            templateUrl: rootUrl + 'app/IFRS9/views/sectorvariablemapping-list-view.html',
            controller: 'SectorVariableMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-sectorvariablemapping-edit', {
            url: "/ifrs9-sectorvariablemapping-edit/:sectorvariablemappingId",
            templateUrl: rootUrl + 'app/IFRS9/views/sectorvariablemapping-edit-view.html',
            controller: 'SectorVariableMappingEditController as vm'

        }).state('ifrs9-dashboard-list', {
            url: "/ifrs9-dashboard-list-view",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrs9-dashboard-list-view.html',
            controller: 'IFRS9DashboardController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-pitpd-list', {
            url: "/ifrs9-pitpd-list",
            templateUrl: rootUrl + 'app/IFRS9/views/pitpd-list-view.html',
            controller: 'PiTPDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-loanbucketdistribution-list', {
            url: "/ifrs9-loanbucketdistribution-list",
            templateUrl: rootUrl + 'app/IFRS9/views/loanbucketdistribution-list-view.html',
            controller: 'LoanBucketDistributionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-pitformular-list', {
            url: "/ifrs9-pitformular-list",
            templateUrl: rootUrl + 'app/IFRS9/views/pitformular-list-view.html',
            controller: 'PitFormularListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-macroeconomicvdisplay-list', {
            url: "/ifrs9-macroeconomicvdisplay-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macroeconomicvdisplay-list-view.html',
            controller: 'MacroeconomicVDisplayListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-lifetimepdclassification-list', {
            url: "/ifrs9-lifetimepdclassification-list",
            templateUrl: rootUrl + 'app/IFRS9/views/lifetimepdclassification-list-view.html',
            controller: 'LifeTimePDClassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-performingloan-list', {
            url: "/ifrs9-performingloan-list",
            templateUrl: rootUrl + 'app/IFRS9/views/performingloan-list-view.html',
            controller: 'PerformingLoanListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-underperformingloan-list', {
            url: "/ifrs9-underperformingloan-list",
            templateUrl: rootUrl + 'app/IFRS9/views/underperformingloan-list-view.html',
            controller: 'UnderPerformingLoanListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-nonperformingloan-list', {
            url: "/ifrs9-nonperformingloan-list",
            templateUrl: rootUrl + 'app/IFRS9/views/nonperformingloan-list-view.html',
            controller: 'NonPerformingLoanListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-summaryreport-list', {
            url: "/ifrs9-summaryreport-list",
            templateUrl: rootUrl + 'app/IFRS9/views/summaryreport-list-view.html',
            controller: 'SummaryReportController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-eclcalculationmodel-list', {
            url: "/ifrs9-eclcalculationmodel-list",
            templateUrl: rootUrl + 'app/IFRS9/views/eclcalculationmodel-list-view.html',
            controller: 'EclCalculationModelListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-ifrsequityunqouted-list', {
            url: "/ifrs9-ifrsequityunqouted-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsequityunqouted-list-view.html',
            controller: 'IfrsEquityUnqoutedListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-ifrsequityunqouted-edit', {
            url: "/ifrs9-ifrsequityunqouted-edit/:ifrsequityunqoutedId",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsequityunqouted-edit-view.html',
            controller: 'IfrsEquityUnqoutedEditController as vm'

        }).state('ifrs9-ifrsstocksmapping-list', {
            url: "/ifrs9-ifrsstocksmapping-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsstocksmapping-list-view.html',
            controller: 'IfrsStocksMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-ifrsstocksmapping-edit', {
            url: "/ifrs9-ifrsstocksmapping-edit/:ifrsstocksmappingId",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsstocksmapping-edit-view.html',
            controller: 'IfrsStocksMappingEditController as vm'


        }).state('ifrs9-ifrsstocksprimarydata-list', {
            url: "/ifrs9-ifrsstocksprimarydata-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsstocksprimarydata-list-view.html',
            controller: 'IfrsStocksPrimaryDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-ifrsstocksprimarydata-edit', {
            url: "/ifrs9-ifrsstocksprimarydata-edit/:ifrsstocksprimarydataId",
            templateUrl: rootUrl + 'app/IFRS9/views/ifrsstocksprimarydata-edit-view.html',
            controller: 'IfrsStocksPrimaryDataEditController as vm'

        }).state('ifrs9-reconciliation-list', {
            url: "/ifrs9-reconciliation-list",
            templateUrl: rootUrl + 'app/IFRS9/views/reconciliation-list-view.html',
            controller: 'ReconciliationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-forecastedmacroeconimcssensitivity-list', {
            url: "/ifrs9-forecastedmacroeconimcssensitivity-list",
            templateUrl: rootUrl + 'app/IFRS9/views/forecastedmacroeconimcssensitivity-list-view.html',
            controller: 'ForecastedMacroeconimcsSensitivityListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-forecastedmacroeconimcsscenario-list', {
            url: "/ifrs9-forecastedmacroeconimcsscenario-list",
            templateUrl: rootUrl + 'app/IFRS9/views/forecastedmacroeconimcsscenario-list-view.html',
            controller: 'ForecastedMacroeconimcsScenarioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-fairvaluationmodel-list', {
            url: "/ifrs9-fairvaluationmodel-list",
            templateUrl: rootUrl + 'app/IFRS9/views/fairvaluationmodel-list-view.html',
            controller: 'FairValuationModelListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-loanspreadscenario-list', {
            url: "/ifrs9-loanspreadscenario-list",
            templateUrl: rootUrl + 'app/IFRS9/views/loanspreadscenario-list-view.html',
            controller: 'LoanSpreadScenarioListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-loanspreadsensitivity-list', {
            url: "/ifrs9-loanspreadsensitivity-list",
            templateUrl: rootUrl + 'app/IFRS9/views/loanspreadsensitivity-list-view.html',
            controller: 'LoanSpreadSensitivityListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-unquotedequityfairvalueresult-list', {
            url: "/ifrs9-unquotedequityfairvalueresult-list",
            templateUrl: rootUrl + 'app/IFRS9/views/unquotedequityfairvalueresult-list-view.html',
            controller: 'UnquotedEquityFairvalueResultListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-markovmatrix-list', {
            url: "/ifrs9-markovmatrix-list",
            templateUrl: rootUrl + 'app/IFRS9/views/markovmatrix-list-view.html',
            controller: 'MarkovMatrixListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-pitpdcomparism-list', {
            url: "/ifrs9-pitpdcomparism-list",
            templateUrl: rootUrl + 'app/IFRS9/views/pitpdcomparism-list-view.html',
            controller: 'PiTPDComparismListController as vm'

        }).state('ifrs9-eclcomparism-list', {
            url: "/ifrs9-eclcomparism-list",
            templateUrl: rootUrl + 'app/IFRS9/views/eclcomparism-list-view.html',
            controller: 'ECLComparismListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('ifrs9-ccfmodelling-list', {
            url: "/ifrs9-ccfmodelling-list",
            templateUrl: rootUrl + 'app/IFRS9/views/ccfmodelling-list-view.html',
            controller: 'CCFModellingListController as vm'

        }).state('ifrs9-foreigneadexchangerate-list', {
            url: "/ifrs9-foreigneadexchangerate-list",
            templateUrl: rootUrl + 'app/IFRS9/views/foreigneadexchangerate-list-view .html',
            controller: 'ForeignEADExchangeRateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('ifrs9-probabilityweighted-list', {
            url: "/ifrs9-probabilityweighted-list",
            templateUrl: rootUrl + 'app/IFRS9/views/probabilityweighted-list-view.html',
            controller: 'ProbabilityWeightedListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-probabilityweighted-edit', {
            url: "/ifrs9-probabilityweighted-edit/:ProbabilityWeighted_Id",
            templateUrl: rootUrl + 'app/IFRS9/views/probabilityweighted-edit-view.html',
            controller: 'ProbabilityWeightedEditController as vm'


        }).state('ifrs9-macrovariableestimate-list', {
            url: "/ifrs9-macrovariableestimate-list",
            templateUrl: rootUrl + 'app/IFRS9/views/macrovariableestimate-list-view.html',
            controller: 'MacrovariableEstimateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-macrovariableestimate-edit', {
            url: "/ifrs9-macrovariableestimate-edit/:MacrovariableEstimate_Id",
            templateUrl: rootUrl + 'app/IFRS9/views/macrovariableestimate-edit-view.html',
            controller: 'MacrovariableEstimateEditController as vm'


        }).state('ifrs9-sectormapping-list', {
            url: "/ifrs9-sectormapping-list",
            templateUrl: rootUrl + 'app/IFRS9/views/sectormapping-list-view.html',
            controller: 'SectorMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('ifrs9-sectormapping-edit', {
            url: "/ifrs9-sectormapping-edit/:SectorMapping_Id",
            templateUrl: rootUrl + 'app/IFRS9/views/sectormapping-edit-view.html',
            controller: 'SectorMappingEditController as vm'


        }).state('ifrs9-investmentothersecl-list', {
            url: "/ifrs9-investmentothersecl-list",
            templateUrl: rootUrl + 'app/IFRS9/views/investmentothersecl-list-view.html',
            controller: 'InvestmentOthersECLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


            //IFRS9 END


            //Scorecard states
        }).state('scd-dashboard-graph', {
            url: "/scd-dashboard-graph",
            templateUrl: rootUrl + 'app/scorecard/views/scd-dashboard-graph-view.html',
            controller: 'ScorecardDashboardController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-analytic-graph', {
            url: "/scd-analytic-graph",
            templateUrl: rootUrl + 'app/scorecard/views/scd-analytic-graph-view.html',
            controller: 'ScorecardAnalyticController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-dataentry-list', {
            url: "/scd-dataentry-list",
            templateUrl: rootUrl + 'app/scorecard/views/dataentry-list-view.html',
            controller: 'KPIDataEntryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-dataentry-edit', {
            url: "/scd-dataentry-edit/:dataentryId",
            templateUrl: rootUrl + 'app/scorecard/views/dataentry-edit-view.html',
            controller: 'KPIDataEntryEditController as vm'
        }).state('scd-setup-list', {
            url: "/scd-setup-list",
            templateUrl: rootUrl + 'app/scorecard/views/setup-edit-view.html',
            controller: 'SCDSetupEditController as vm'
        }).state('scd-teamclassification-list', {
            url: "/scd-teamclassification-list",
            templateUrl: rootUrl + 'app/scorecard/views/teamclassification-list-view.html',
            controller: 'SCDTeamClassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-teamclassification-edit', {
            url: "/scd-teamclassification-edit/:teamclassificationId",
            templateUrl: rootUrl + 'app/scorecard/views/teamclassification-edit-view.html',
            controller: 'SCDTeamClassificationEditController as vm'
        }).state('scd-teammapping-list', {
            url: "/scd-teammapping-list",
            templateUrl: rootUrl + 'app/scorecard/views/teammap-list-view.html',
            controller: 'TeamMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-teammap-edit', {
            url: "/scd-teammap-edit/:teammapId",
            templateUrl: rootUrl + 'app/scorecard/views/teammap-edit-view.html',
            controller: 'TeamMapEditController as vm'
        }).state('scd-category-list', {
            url: "/scd-category-list",
            templateUrl: rootUrl + 'app/scorecard/views/category-list-view.html',
            controller: 'CategoryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colVis.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/datatable/exts/dataTables.colReorder.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-category-edit', {
            url: "/scd-category-edit/:categoryId",
            templateUrl: rootUrl + 'app/scorecard/views/category-edit-view.html',
            controller: 'CategoryEditController as vm'
        }).state('scd-metric-list', {
            url: "/scd-metric-list",
            templateUrl: rootUrl + 'app/scorecard/views/metric-list-view.html',
            controller: 'KPIMetricListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-metric-edit', {
            url: "/scd-metric-edit/:metricId",
            templateUrl: rootUrl + 'app/scorecard/views/metric-edit-view.html',
            controller: 'KPIMetricEditController as vm'
        }).state('scd-classification-list', {
            url: "/scd-classification-list",
            templateUrl: rootUrl + 'app/scorecard/views/kpiclassification-list-view.html',
            controller: 'KPIClassificationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-kpiclassification-edit', {
            url: "/scd-kpiclassification-edit/:classificationId",
            templateUrl: rootUrl + 'app/scorecard/views/kpiclassification-edit-view.html',
            controller: 'KPIClassificationEditController as vm'
        }).state('scd-participant-list', {
            url: "/scd-participant-list",
            templateUrl: rootUrl + 'app/scorecard/views/participant-list-view.html',
            controller: 'KPIParticipantListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-participant-edit', {
            url: "/scd-participant-edit/:participantId",
            templateUrl: rootUrl + 'app/scorecard/views/participant-edit-view.html',
            controller: 'KPIParticipantEditController as vm'
        }).state('scd-threshold-list', {
            url: "/scd-threshold-list",
            templateUrl: rootUrl + 'app/scorecard/views/threshold-list-view.html',
            controller: 'KPIThresholdListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-threshold-edit', {
            url: "/scd-threshold-edit/:thresholdId",
            templateUrl: rootUrl + 'app/scorecard/views/threshold-edit-view.html',
            controller: 'KPIThresholdEditController as vm'
        }).state('scd-actualdata-list', {
            url: "/scd-actualdata-list",
            templateUrl: rootUrl + 'app/scorecard/views/actualdata-list-view.html',
            controller: 'SCDActualDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-actualdata-edit', {
            url: "/scd-actualdata-edit/:actualdataId",
            templateUrl: rootUrl + 'app/scorecard/views/actualdata-edit-view.html',
            controller: 'SCDActualDataEditController as vm'
        }).state('scd-actualmapping-list', {
            url: "/scd-actualmapping-list",
            templateUrl: rootUrl + 'app/scorecard/views/actualmapping-list-view.html',
            controller: 'KPIActualMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-actualmapping-edit', {
            url: "/scd-actualmapping-edit/:actualmappingId",
            templateUrl: rootUrl + 'app/scorecard/views/actualmapping-edit-view.html',
            controller: 'KPIActualMappingEditController as vm'
        }).state('scd-targetdata-list', {
            url: "/scd-targetdata-list",
            templateUrl: rootUrl + 'app/scorecard/views/targetdata-list-view.html',
            controller: 'SCDTargetDataListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-targetdata-edit', {
            url: "/scd-targetdata-edit/:targetdataId",
            templateUrl: rootUrl + 'app/scorecard/views/targetdata-edit-view.html',
            controller: 'SCDTargetDataEditController as vm'
        }).state('scd-targetmapping-list', {
            url: "/scd-targetmapping-list",
            templateUrl: rootUrl + 'app/scorecard/views/targetmapping-list-view.html',
            controller: 'KPITargetMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-targetmapping-edit', {
            url: "/scd-targetmapping-edit/:targetmappingId",
            templateUrl: rootUrl + 'app/scorecard/views/targetmapping-edit-view.html',
            controller: 'KPITargetMappingEditController as vm'
        }).state('scd-topperformingkpi-report', {
            url: "/scd-topperformingkpi-report",
            templateUrl: rootUrl + 'app/scorecard/views/topperformingkpi-report-view.html',
            controller: 'TopPerformingKPIController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-worstperformingkpi-report', {
            url: "/scd-worstperformingkpi-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-worstperformingkpi-report-view.html',
            controller: 'WorstPerformingKPIController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-multiplekpi-report', {
            url: "/scd-multiplekpi-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-multiplekpi-report-view.html',
            controller: 'MultipleKPIController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-kpiperformance-report', {
            url: "/scd-kpiperformance-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-kpiperformance-report-view.html',
            controller: 'KPIPerformanceController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-userkpi-report', {
            url: "/scd-userkpi-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-userkpi-report-view.html',
            controller: 'UserKPIController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-dataentrybykpi-report', {
            url: "/scd-dataentrybykpi-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-dataentrybykpi-report-view.html',
            controller: 'DataEntryByKPIController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-dataentrybyuser-report', {
            url: "/scd-dataentrybyuser-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-dataentrybyuser-report-view.html',
            controller: 'DataEntryByUserController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('scd-alldataentry-report', {
            url: "/scd-alldataentry-report",
            templateUrl: rootUrl + 'app/scorecard/views/scd-alldataentry-report-view.html',
            controller: 'AllDataEntryController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('budget-operation-list', {
            url: "/budget-operation-list",
            templateUrl: rootUrl + 'app/budget/views/operation-list-view.html',
            controller: 'OperationListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('budget-operation-edit', {
            url: "/budget-operation-edit/:operationId",
            templateUrl: rootUrl + 'app/budget/views/operation-edit-view.html',
            controller: 'OperationEditController as vm'
        }).state('budget-operationreview-edit', {
            url: "/budget-operationreview-edit/:operationId?operationcode?operationreviewId",
            templateUrl: rootUrl + 'app/budget/views/operationreview-edit-view.html',
            controller: 'OperationReviewEditController as vm'
        }).state('budget-budgetinglevel-list', {
            url: "/budget-budgetinglevel-list",
            templateUrl: rootUrl + 'app/budget/views/budgetinglevel-list-view.html',
            controller: 'BudgetingLevelListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('budget-budgetinglevel-edit', {
            url: "/budget-budgetinglevel-edit/:budgetingLevelId",
            templateUrl: rootUrl + 'app/budget/views/budgetinglevel-edit-view.html',
            controller: 'BudgetingLevelEditController as vm'
        }).state('budget-policylevel-list', {
            url: "/budget-policylevel-list",
            templateUrl: rootUrl + 'app/budget/views/policylevel-list-view.html',
            controller: 'PolicyLevelListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('budget-policylevel-edit', {
            url: "/budget-policylevel-edit/:policyLevelId",
            templateUrl: rootUrl + 'app/budget/views/policylevel-edit-view.html',
            controller: 'PolicyLevelEditController as vm'
        }).state('budget-modificationlevel-list', {
            url: "/budget-modificationlevel-list",
            templateUrl: rootUrl + 'app/budget/views/modificationlevel-list-view.html',
            controller: 'ModificationLevelListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        }).state('budget-modificationlevel-edit', {
            url: "/budget-modificationlevel-edit/:modificationLevelId",
            templateUrl: rootUrl + 'app/budget/views/modificationlevel-edit-view.html',
            controller: 'ModificationLevelEditController as vm'
        }).state('mpr-crbdata-list', {
            url: "/mpr-crbdata-list",
            templateUrl: rootUrl + 'app/mpr_core/views/crbdata-list-view.html',
            controller: 'CRBListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-ai-list', {
            url: "/mpr-ai-list",
            templateUrl: rootUrl + 'app/mpr_core/views/accountinterest-list-view.html',
            controller: 'AIListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-pi-list', {
            url: "/mpr-pi-list",
            templateUrl: rootUrl + 'app/mpr_core/views/productinterest-list-view.html',
            controller: 'PIListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-ts-edit', {
            url: "/mpr-ts-edit/:Team_StructureId",
            templateUrl: rootUrl + 'app/mpr_core/views/teamstructure-edit-view.html',
            controller: 'TSEditController as vm'

        }).state('mpr-ts-list', {
            url: "/mpr-ts-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teamstructure-list-view.html',
            controller: 'TSListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-scorecardmetrics-edit', {
            url: "/mpr-scorecardmetrics-edit/:MetricId",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardmetrics-edit-view.html',
            controller: 'SCMEditController as vm'

        }).state('mpr-scorecardmetrics-list', {
            url: "/mpr-scorecardmetrics-list",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardmetrics-list-view.html',
            controller: 'SCMListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            //rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            //rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            //rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']

                            rootUrl + 'app/assets/newdatatable_files/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/newdatatable_files/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/newdatatable_files/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-scorecardweight-edit', {
            url: "/mpr-scorecardweight-edit/:WeightId",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardweight-edit-view.html',
            controller: 'SCWEditController as vm'

        }).state('mpr-scorecardweight-list', {
            url: "/mpr-scorecardweight-list",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardweight-list-view.html',
            controller: 'SCWListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('mpr-scorecardmapping-edit', {
            url: "/mpr-scorecardmapping-edit/:MappingId",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardmapping-edit-view.html',
            controller: 'SCMappingEditController as vm'

        }).state('mpr-scorecardmapping-list', {
            url: "/mpr-scorecardmapping-list",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardmapping-list-view.html',
            controller: 'SCMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-mistransferprice-edit', {
            url: "/mpr-mistransferprice-edit/:mistransferpriceId",
            templateUrl: rootUrl + 'app/mpr_core/views/mistransferprice-edit-view.html',
            controller: 'MISTransferPriceEditController as vm'

        }).state('mpr-mistransferprice-list', {
            url: "/mpr-mistransferprice-list",
            templateUrl: rootUrl + 'app/mpr_core/views/mistransferprice-list-view.html',
            controller: 'MISTransferPriceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-categorytransferprice-edit', {
            url: "/mpr-categorytransferprice-edit/:CategoryTransferPriceId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/categorytransferprice-edit-view.html',
            controller: 'CategoryTransferPriceEditController as vm'

        }).state('mpr-categorytransferprice-list', {
            url: "/mpr-categorytransferprice-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/categorytransferprice-list-view.html',
            controller: 'CategoryTransferPriceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-customertransferprice-edit', {
            url: "/mpr-customertransferprice-edit/:customertransferpriceId",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/customertransferprice-edit-view.html',
            controller: 'CustomerTransferPriceEditController as vm'

        }).state('mpr-customertransferprice-list', {
            url: "/mpr-customertransferprice-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/customertransferprice-list-view.html',
            controller: 'CustomerTransferPriceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-acquirermapping-list', {
            url: "/mpr-acquirermapping-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/acquirermapping-list-view.html',
            controller: 'AcquirerMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-acquirersharing-list', {
            url: "/mpr-acquirersharing-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/acquirersharing-list-view.html',
            controller: 'AcquirerSharingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-teamsector-edit', {
            url: "/mpr-teamsector-edit/:Mpr_Team_Sector_ID",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/teamsector-edit-view.html',
            controller: 'TeamSectorEditController as vm'

        }).state('mpr-teamsector-list', {
            url: "/mpr-teamsector-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/teamsector-list-view.html',
            controller: 'TeamSectorListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-teamsegment-edit', {
            url: "/mpr-teamsegment-edit/:Mpr_Team_Segment_ID",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/teamsegment-edit-view.html',
            controller: 'TeamSegmentEditController as vm'

        }).state('mpr-teamsegment-list', {
            url: "/mpr-teamsegment-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/teamsegment-list-view.html',
            controller: 'TeamSegmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-producttransferprice-edit', {
            url: "/mpr-producttransferprice-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/producttransferprice-edit-view.html',
            controller: 'ProductTransferPriceEditController as vm'

        }).state('mpr-producttransferprice-list', {
            url: "/mpr-producttransferprice-list",
            templateUrl: rootUrl + 'app/mpr_core/views/producttransferprice-list-view.html',
            controller: 'ProductTransferPriceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-ctp-list', {
            url: "/mpr-ctp-list",
            templateUrl: rootUrl + 'app/mpr_core/views/captiontransferprice-list-view.html',
            controller: 'CTPListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-assettype-list', {
            url: "/mpr-assettype-list",
            templateUrl: rootUrl + 'app/mpr_core/views/assettype-list-view.html',
            controller: 'AssetTypeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-customermis-list', {
            url: "/mpr-customermis-list",
            templateUrl: rootUrl + 'app/mpr_core/views/customermis-list-view.html',
            controller: 'CustomermisListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-ppr-list', {
            url: "/mpr-ppr-list",
            templateUrl: rootUrl + 'app/mpr_core/views/ppr-list-view.html',
            controller: 'PPRListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-riskadjustedcharge-list', {
            url: "/mpr-riskadjustedcharge-list",
            templateUrl: rootUrl + 'app/mpr_core/views/riskadjustedcharge-list-view.html',
            controller: 'RACListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-publicsector-list', {
            url: "/mpr-publicsector-list",
            templateUrl: rootUrl + 'app/mpr_core/views/publicsector-list-view.html',
            controller: 'PSListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-corporateadjustment-list', {
            url: "/mpr-corporateadjustment-list",
            templateUrl: rootUrl + 'app/mpr_core/views/corporateadjustment-list-view.html',
            controller: 'CAdjustmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomecommfeelinecaption-edit', {
            url: "/mpr-incomecommfeelinecaption-edit/:ICFLcaptionId",
            templateUrl: rootUrl + 'app/mpr_income/views/incomecommfeelinecaption-edit-view.html',
            controller: 'IncomeCommFeeLineCaptionEditController as vm'

        }).state('mpr-incomecommfeelinecaption-list', {
            url: "/mpr-incomecommfeelinecaption-list",
            templateUrl: rootUrl + 'app/mpr_income/views/incomecommfeelinecaption-list-view.html',
            controller: 'IncomeCommFeeLineCaptionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeproductstableunit-edit', {
            url: "/mpr-incomeproductstableunit-edit/:iptUnitId",
            templateUrl: rootUrl + 'app/mpr_income/views/incomeproductstableunit-edit-view.html',
            controller: 'IncomeProductsTableUnitEditController as vm'

        }).state('mpr-incomeproductstableunit-list', {
            url: "/mpr-incomeproductstableunit-list",
            templateUrl: rootUrl + 'app/mpr_income/views/incomeproductstableunit-list-view.html',
            controller: 'IncomeProductsTableUnitListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeproductstabletreasury-edit', {
            url: "/mpr-incomeproductstabletreasury-edit/:iptTreasuryId",
            templateUrl: rootUrl + 'app/mpr_income/views/incomeproductstabletreasury-edit-view.html',
            controller: 'IncomeProductsTableTreasuryEditController as vm'

        }).state('mpr-incomeproductstabletreasury-list', {
            url: "/mpr-incomeproductstabletreasury-list",
            templateUrl: rootUrl + 'app/mpr_income/views/incomeproductstabletreasury-list-view.html',
            controller: 'IncomeProductsTableTreasuryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeNEAmapping-edit', {
            url: "/mpr-incomeNEAmapping-edit/:incomeNEAmappingId",
            templateUrl: rootUrl + 'app/mpr_income/views/incomeNEAmapping-edit-view.html',
            controller: 'IncomeNEAMappingEditController as vm'

        }).state('mpr-incomeNEAmapping-list', {
            url: "/mpr-incomeNEAmapping-list",
            templateUrl: rootUrl + 'app/mpr_income/views/incomeNEAmapping-list-view.html',
            controller: 'IncomeNEAMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-teamstructure_kbl-edit', {
            url: "/mpr-teamstructure_kbl-edit/:teambankid/:teamgroupid",
            templateUrl: rootUrl + 'app/mpr_core/views/teamstructure_kbl-edit-view.html',
            controller: 'TeamStructureKBLEditController as vm'

        }).state('mpr-teamstructure_kbl-list', {
            url: "/mpr-teamstructure_kbl-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teamstructure_kbl-list-view.html',
            controller: 'TeamStructureKBLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-usermis_kbl-edit', {
            url: "/mpr-usermis_kbl-edit/:usermisId",
            templateUrl: rootUrl + 'app/mpr_core/views/usermis_kbl-edit-view.html',
            controller: 'UserMIS_KBLEditController as vm'
        }).state('mpr-usermis_kbl-list', {
            url: "/mpr-usermis_kbl-list",
            templateUrl: rootUrl + 'app/mpr_core/views/usermis_kbl-list-view.html',
            controller: 'UserMIS_KBLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('core-usersetup_kbl-edit', {
            url: "/core-usersetup_kbl-edit/:usersetupId",
            templateUrl: rootUrl + 'app/core/views/account/usersetup_kbl-edit-view.html',
            controller: 'UserSetup_KBLEditController as vm'
        }).state('core-usersetup_kbl-list', {
            url: "/core-usersetup_kbl-list",
            templateUrl: rootUrl + 'app/core/views/account/usersetup_kbl-list-view.html',
            controller: 'UserSetup_KBLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-scorecardmetricsKBL-edit', {
            url: "/mpr-scorecardmetricsKBL-edit/:MetricID",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardmetricsKBL-edit-view.html',
            controller: 'ScoreCardMetricsKBLEditController as vm'
        }).state('mpr-scorecardmetricsKBL-list', {
            url: "/mpr-scorecardmetricsKBL-list",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardmetricsKBL-list-view.html',
            controller: 'ScoreCardMetricsKBLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-scorecardKPItypesKBL-edit', {
            url: "/mpr-scorecardKPItypesKBL-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardKPItypesKBL-edit-view.html',
            controller: 'ScoreCardKPITypesKBLEditController as vm'
        }).state('mpr-scorecardKPItypesKBL-list', {
            url: "/mpr-scorecardKPItypesKBL-list",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardKPItypesKBL-list-view.html',
            controller: 'ScoreCardKPITypesKBLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-scorecardsetmetrictargetKBL-edit', {
            url: "/mpr-scorecardsetmetrictargetKBL-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardsetmetrictargetKBL-edit-view.html',
            controller: 'ScoreCardSetMetricTargetKBLEditController as vm'
        }).state('mpr-scorecardsetmetrictargetKBL-list', {
            url: "/mpr-scorecardsetmetrictargetKBL-list",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardsetmetrictargetKBL-list-view.html',
            controller: 'ScoreCardSetMetricTargetKBLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-scorecardMISmappingKBL-edit', {
            url: "/mpr-scorecardMISmappingKBL-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardMISmappingKBL-edit-view.html',
            controller: 'ScoreCardMISMappingKBLEditController as vm'
        }).state('mpr-scorecardMISmappingKBL-list', {
            url: "/mpr-scorecardMISmappingKBL-list",
            templateUrl: rootUrl + 'app/mpr_core/views/scorecardMISmappingKBL-list-view.html',
            controller: 'ScoreCardMISMappingKBLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomecashvaultschedule-edit', {
            url: "/mpr-incomecashvaultschedule-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomecashvaultschedule-edit-view.html',
            controller: 'IncomeCashVaultScheduleEditController as vm'
        }).state('mpr-incomecashvaultschedule-list', {
            url: "/mpr-incomecashvaultschedule-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomecashvaultschedule-list-view.html',
            controller: 'IncomeCashVaultScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-accountnumbercustomername-list', {
            url: "/mpr-accountnumbercustomername-list",
            templateUrl: rootUrl + 'app/mpr_core/views/accountnumbercustomername-list-view.html',
            controller: 'AccountNumberCustomerNameListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-slaryschedule-edit', {
            url: "/mpr-slaryschedule-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/slaryschedule-edit-view.html',
            controller: 'SlaryScheduleEditController as vm'
        }).state('mpr-slaryschedule-list', {
            url: "/mpr-slaryschedule-list",
            templateUrl: rootUrl + 'app/mpr_core/views/slaryschedule-list-view.html',
            controller: 'SlaryScheduleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-opexglbasis2-edit', {
            url: "/mpr-opexglbasis2-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexglbasis2-edit-view.html',
            controller: 'OpexGLBasis2EditController as vm'
        }).state('mpr-opexglbasis2-list', {
            url: "/mpr-opexglbasis2-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexglbasis2-list-view.html',
            controller: 'OpexGLBasis2ListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeotherbreakdown-edit', {
            url: "/mpr-incomeotherbreakdown-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeotherbreakdown-edit-view.html',
            controller: 'IncomeOtherBreakdownEditController as vm'
        }).state('mpr-incomeotherbreakdown-list', {
            url: "/mpr-incomeotherbreakdown-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeotherbreakdown-list-view.html',

            controller: 'IncomeOtherBreakdownListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-downloadbasefintrakfinalmanual-edit', {
            url: "/mpr-downloadbasefintrakfinalmanual-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/downloadbasefintrakfinalmanual-edit-view.html',
            controller: 'DownloadBaseFintrakFinalManualEditController as vm'
        }).state('mpr-downloadbasefintrakfinalmanual-list', {
            url: "/mpr-downloadbasefintrakfinalmanual-list",
            templateUrl: rootUrl + 'app/mpr_core/views/downloadbasefintrakfinalmanual-list-view.html',
            controller: 'DownloadBaseFintrakFinalManualListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-closeperiod2-list', {
            url: "/mpr-closeperiod2-list",
            templateUrl: rootUrl + 'app/core/views/configuration/closeperiod2-list-view.html',
            controller: 'ClosePeriod2Controller as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-mprreportstatus-edit', {
            url: "/mpr-mprreportstatus-edit/:MPRReportStatusId",
            templateUrl: rootUrl + 'app/mpr_core/views/mprreportstatus-edit-view.html',
            controller: 'MPRReportStatusEditController as vm'
        }).state('mpr-mprreportstatus-list', {
            url: "/mpr-mprreportstatus-list",
            templateUrl: rootUrl + 'app/mpr_core/views/mprreportstatus-list-view.html',

            controller: 'MPRReportStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-finstatmapping-edit', {
            url: "/mpr-finstatmapping-edit/:finstatmappingId",
            templateUrl: rootUrl + 'app/mpr_core/views/finstatmapping-edit-view.html',
            controller: 'FinstatMappingEditController as vm'
        }).state('mpr-finstatmapping-list', {
            url: "/mpr-finstatmapping-list",
            templateUrl: rootUrl + 'app/mpr_core/views/finstatmapping-list-view.html',

            controller: 'FinstatMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomesetup-edit', {
            url: "/mpr-incomesetup-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomesetup-edit-view.html',
            controller: 'IncomeSetupEditController as vm'
        }).state('mpr-incomesetup-list', {
            url: "/mpr-incomesetup-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomesetup-list-view.html',

            controller: 'IncomeSetupListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-teambank_kbl-edit', {
            url: "/mpr-teambank_kbl-edit/:teambankid",
            templateUrl: rootUrl + 'app/mpr_core/views/teambank_kbl-edit-view.html',
            controller: 'TeamBankKBLEditController as vm'
        }).state('mpr-teambank_kbl-list', {
            url: "/mpr-teambank_kbl-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teambank_kbl-list-view.html',

            controller: 'TeamBankKBLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-teamgroup_kbl-edit', {
            url: "/mpr-teamgroup_kbl-edit/:teamgroupid",
            templateUrl: rootUrl + 'app/mpr_core/views/teamgroup_kbl-edit-view.html',
            controller: 'TeamGroupKBLEditController as vm'
        }).state('mpr-teamgroup_kbl-list', {
            url: "/mpr-teamgroup_kbl-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teamgroup_kbl-list-view.html',

            controller: 'TeamGroupKBLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountstreemiscodes-edit', {
            url: "/mpr-incomeaccountstreemiscodes-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountstreemiscodes-edit-view.html',
            controller: 'IncomeAccountsTreeMisCodesEditController as vm'
        }).state('mpr-incomeaccountstreemiscodes-list', {
            url: "/mpr-incomeaccountstreemiscodes-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountstreemiscodes-list-view.html',

            controller: 'IncomeAccountsTreeMisCodesListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountstreeaccount-edit', {
            url: "/mpr-incomeaccountstreeaccount-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountstreeaccount-edit-view.html',
            controller: 'IncomeAccountsTreeAccountEditController as vm'
        }).state('mpr-incomeaccountstreeaccount-list', {
            url: "/mpr-incomeaccountstreeaccount-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountstreeaccount-list-view.html',

            controller: 'IncomeAccountsTreeAccountListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomepoolratesbu-edit', {
            url: "/mpr-incomepoolratesbu-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomepoolratesbu-edit-view.html',
            controller: 'IncomePoolRateSbuEditController as vm'
        }).state('mpr-incomepoolratesbu-list', {
            url: "/mpr-incomepoolratesbu-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomepoolratesbu-list-view.html',

            controller: 'IncomePoolRateSbuListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomepoolratesbuyear-edit', {
            url: "/mpr-incomepoolratesbuyear-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomepoolratesbuyear-edit-view.html',
            controller: 'IncomePoolRateSbuYearEditController as vm'
        }).state('mpr-incomepoolratesbuyear-list', {
            url: "/mpr-incomepoolratesbuyear-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomepoolratesbuyear-list-view.html',

            controller: 'IncomePoolRateSbuYearListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountsfintrak-edit', {
            url: "/mpr-incomeaccountsfintrak-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountsfintrak-edit-view.html',
            controller: 'IncomeAccountsFintrakEditController as vm'
        }).state('mpr-incomeaccountsfintrak-list', {
            url: "/mpr-incomeaccountsfintrak-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountsfintrak-list-view.html',

            controller: 'IncomeAccountsFintrakListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('core-incomecrrsector-edit', {
            url: "/core-incomecrrsector-edit/:ID",
            templateUrl: rootUrl + 'app/core/views/configuration/incomecrrsector-edit-view.html',
            controller: 'IncomeCRRSectorEditController as vm'
        }).state('core-incomecrrsector-list', {
            url: "/core-incomecrrsector-list",
            templateUrl: rootUrl + 'app/core/views/configuration/incomecrrsector-list-view.html',

            controller: 'IncomeCRRSectorListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('core-incomemonths-edit', {
            url: "/core-incomemonths-edit/:ID",
            templateUrl: rootUrl + 'app/core/views/configuration/incomemonths-edit-view.html',
            controller: 'IncomeMonthsEditController as vm'
        }).state('core-incomemonths-list', {
            url: "/core-incomemonths-list",
            templateUrl: rootUrl + 'app/core/views/configuration/incomemonths-list-view.html',

            controller: 'IncomeMonthsListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountsnpl-edit', {
            url: "/mpr-incomeaccountsnpl-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountsnpl-edit-view.html',
            controller: 'IncomeAccountsNplEditController as vm'
        }).state('mpr-incomeaccountsnpl-list', {
            url: "/mpr-incomeaccountsnpl-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountsnpl-list-view.html',

            controller: 'IncomeAccountsNplListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomecommfeemis-edit', {
            url: "/mpr-incomecommfeemis-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomecommfeemis-edit-view.html',
            controller: 'IncomeCommFeeMisEditController as vm'
        }).state('mpr-incomecommfeemis-list', {
            url: "/mpr-incomecommfeemis-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomecommfeemis-list-view.html',

            controller: 'IncomeCommFeeMisListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomemiscodes-edit', {
            url: "/mpr-incomemiscodes-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomemiscodes-edit-view.html',
            controller: 'IncomeMisCodesEditController as vm'
        }).state('mpr-incomemiscodes-list', {
            url: "/mpr-incomemiscodes-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomemiscodes-list-view.html',

            controller: 'IncomeMisCodesListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-opexstaffcostdetail-edit', {
            url: "/mpr-opexstaffcostdetail-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexstaffcostdetail-edit-view.html',
            controller: 'OpexStaffcostDetailEditController as vm'
        }).state('mpr-opexstaffcostdetail-list', {
            url: "/mpr-opexstaffcostdetail-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexstaffcostdetail-list-view.html',
            controller: 'OpexStaffcostDetailListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-opextimeallocationmpr-edit', {
            url: "/mpr-opextimeallocationmpr-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_opex/views/opextimeallocationmpr-edit-view.html',
            controller: 'OpexTimeAllocationMPREditController as vm'
        }).state('mpr-opextimeallocationmpr-list', {
            url: "/mpr-opextimeallocationmpr-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opextimeallocationmpr-list-view.html',
            controller: 'OpexTimeAllocationMPRListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeproductshare-edit', {
            url: "/mpr-incomeproductshare-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_opex/views/incomeproductshare-edit-view.html',
            controller: 'IncomeProductShareEditController as vm'
        }).state('mpr-incomeproductshare-list', {
            url: "/mpr-incomeproductshare-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/incomeproductshare-list-view.html',
            controller: 'IncomeProductShareListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-opexmaintenance-edit', {
            url: "/mpr-opexmaintenance-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmaintenance-edit-view.html',
            controller: 'OpexMaintenanceEditController as vm'
        }).state('mpr-opexmaintenance-list', {
            url: "/mpr-opexmaintenance-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmaintenance-list-view.html',
            controller: 'OpexMaintenanceListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-opexmemounits-edit', {
            url: "/mpr-opexmemounits-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmemounits-edit-view.html',
            controller: 'OpexMemounitsEditController as vm'
        }).state('mpr-opexmemounits-list', {
            url: "/mpr-opexmemounits-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmemounits-list-view.html',
            controller: 'OpexMemounitsListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-opexmemounitplmap-edit', {
            url: "/mpr-opexmemounitplmap-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmemounitplmap-edit-view.html',
            controller: 'OpexMemounitPlmapEditController as vm'
        }).state('mpr-opexmemounitplmap-list', {
            url: "/mpr-opexmemounitplmap-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexmemounitplmap-list-view.html',
            controller: 'OpexMemounitPlmapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-opexsbubasecost-edit', {
            url: "/mpr-opexsbubasecost-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexsbubasecost-edit-view.html',
            controller: 'OpexSBUBaseCostEditController as vm'
        }).state('mpr-opexsbubasecost-list', {
            url: "/mpr-opexsbubasecost-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexsbubasecost-list-view.html',
            controller: 'OpexSBUBaseCostListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('mpr-incomeadjustmentcommfeessearch-edit', {
            url: "/mpr-incomeadjustmentcommfeessearch-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentcommfeessearch-edit-view.html',
            controller: 'IncomeAdjustmentCommFeesSearchEditController as vm'
        }).state('mpr-incomeadjustmentcommfeessearch-list', {
            url: "/mpr-incomeadjustmentcommfeessearch-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentcommfeessearch-list-view.html',
            controller: 'IncomeAdjustmentCommFeesSearchListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeadjustmentcommfeesearchmanual-edit', {
            url: "/mpr-incomeadjustmentcommfeesearchmanual-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentcommfeesearchmanual-edit-view.html',
            controller: 'IncomeAdjustmentCommFeeSearchManualEditController as vm'
        }).state('mpr-incomeadjustmentcommfeesearchmanual-list', {
            url: "/mpr-incomeadjustmentcommfeesearchmanual-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentcommfeesearchmanual-list-view.html',
            controller: 'IncomeAdjustmentCommFeeSearchManualListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeadjustmentvolsummary-list', {
            url: "/mpr-incomeadjustmentvolsummary-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentvolsummary-list-view.html',
            controller: 'IncomeAdjustmentVolSummaryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeadjustmentgetSBUsummary-list', {
            url: "/mpr-incomeadjustmentgetSBUsummary-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentgetSBUsummary-list-view.html',
            controller: 'IncomeAdjustmentGetSBUSummaryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-plincomesummary-list', {
            url: "/mpr-plincomesummary-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/plincomesummary-list-view.html',
            controller: 'ProfitLossIncomeSummaryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeadjustmentvolcaptionsummary-list', {
            url: "/mpr-incomeadjustmentvolcaptionsummary-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentvolcaptionsummary-list-view.html',
            controller: 'IncomeAdjustmentVolCaptionSummaryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-opexrawexpensesfixed-edit', {
            url: "/mpr-opexrawexpensesfixed-edit/:opexrawexpensesfixedId",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexrawexpensesfixed-edit-view.html',
            controller: 'OpexRawExpensesFixedEditController as vm'
        }).state('mpr-opexrawexpensesfixed-list', {
            url: "/mpr-opexrawexpensesfixed-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexrawexpensesfixed-list-view.html',
            controller: 'OpexRawExpensesFixedListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeadjustmentcommfeeSBUcaptionsummary-list', {
            url: "/mpr-incomeadjustmentcommfeeSBUcaptionsummary-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentcommfeeSBUcaptionsummary-list-view.html',
            controller: 'IncomeAdjustmentCommFeeSBUCaptionSummaryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeadjustmentcommfeecaptionsummary-list', {
            url: "/mpr-incomeadjustmentcommfeecaptionsummary-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentcommfeecaptionsummary-list-view.html',
            controller: 'IncomeAdjustmentCommFeeCaptionSummaryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeadjustmentvolprodsummary-list', {
            url: "/mpr-incomeadjustmentvolprodsummary-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentvolprodsummary-list-view.html',
            controller: 'IncomeAdjustmentVolProdSummaryListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-downloadbasefintrakfinalmemomanual-list', {
            url: "/mpr-downloadbasefintrakfinalmemomanual-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/downloadbasefintrakfinalmemomanual-list-view.html',
            controller: 'DownLoadBaseFintrakFinalMemoManualListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeproductmiscode-list', {
            url: "/mpr-incomeproductmiscode-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomeproductmiscode-list-view.html',
            controller: 'IncomeProductMISCodeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomebranches-edit', {
            url: "/mpr-incomebranches-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomebranches-edit-view.html',
            controller: 'IncomeBranchesEditController as vm'
        }).state('mpr-incomebranches-list', {
            url: "/mpr-incomebranches-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomebranches-list-view.html',
            controller: 'IncomeBranchesListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomecurrency-edit', {
            url: "/mpr-incomecurrency-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomecurrency-edit-view.html',
            controller: 'IncomeCurrencyEditController as vm'
        }).state('mpr-incomecurrency-list', {
            url: "/mpr-incomecurrency-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomecurrency-list-view.html',

            controller: 'IncomeCurrencyListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomememorep-edit', {
            url: "/mpr-incomememorep-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomememorep-edit-view.html',
            controller: 'IncomeMemorepEditController as vm'
        }).state('mpr-incomememorep-list', {
            url: "/mpr-incomememorep-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomememorep-list-view.html',

            controller: 'IncomeMemorepListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-opexupdate-edit', {
            url: "/mpr-opexupdate-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexupdate-edit-view.html',
            controller: 'OpexUpdateEditController as vm'
        }).state('mpr-opexupdate-list', {
            url: "/mpr-opexupdate-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexupdate-list-view.html',
            controller: 'OpexUpdateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomesplitpoolsrate-edit', {
            url: "/mpr-incomesplitpoolsrate-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomesplitpoolsrate-edit-view.html',
            controller: 'IncomeSplitPoolsRateEditController as vm'
        }).state('mpr-incomesplitpoolsrate-list', {
            url: "/mpr-incomesplitpoolsrate-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomesplitpoolsrate-list-view.html',

            controller: 'IncomeSplitPoolsRateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountsunit-edit', {
            url: "/mpr-incomeaccountsunit-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountsunit-edit-view.html',
            controller: 'IncomeAccountsUnitEditController as vm'
        }).state('mpr-incomeaccountsunit-list', {
            url: "/mpr-incomeaccountsunit-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountsunit-list-view.html',

            controller: 'IncomeAccountsUnitListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeadjustmentvolsummaryforcommfee-list', {
            url: "/mpr-incomeadjustmentvolsummaryforcommfee-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentvolsummaryforcommfee-list-view.html',
            controller: 'IncomeAdjustmentVolSummaryForCommFeeListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('opex-opexglmap-edit', {
            url: "/opex-opexglmap-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexglmap-edit-view.html',
            controller: 'OpexGLMapEditController as vm'
        }).state('opex-opexglmap-list', {
            url: "/opex-opexglmap-list",
            templateUrl: rootUrl + 'app/mpr_opex/views/opexglmap-list-view.html',

            controller: 'OpexGLMapListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeadjustmentvolumesearch-edit', {
            url: "/mpr-incomeadjustmentvolumesearch-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentvolumesearch-edit-view.html',
            controller: 'IncomeAdjustmentVolumeSearchEditController as vm'
        }).state('mpr-incomeadjustmentvolumesearch-list', {
            url: "/mpr-incomeadjustmentvolumesearch-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeadjustmentvolumesearch-list-view.html',

            controller: 'IncomeAdjustmentVolumeSearchListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }      

        }).state('mpr-incomeproductstablealt-edit', {
            url: "/mpr-incomeproductstablealt-edit/:ProductId",
            templateUrl: rootUrl + 'app/mpr_income/views/incomeproductstablealt-edit-view.html',
            controller: 'IncomeProductsTableALTEditController as vm'

        }).state('mpr-incomeproductstablealt-list', {
            url: "/mpr-incomeproductstablealt-list",
            templateUrl: rootUrl + 'app/mpr_income/views/incomeproductstablealt-list-view.html',
            controller: 'IncomeProductsTableALTListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }


        }).state('mpr-sqladminextraction-list', {
            url: "/mpr-sqladminextraction-list",
            templateUrl: rootUrl + 'app/extraction/views/extraction/sqladminextraction-list-view.html',
            controller: 'SQLAdminExtractionListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-sqladminprocess-list', {
            url: "/mpr-sqladminprocess-list",
            templateUrl: rootUrl + 'app/extraction/views/extraction/sqladminprocess-list-view.html',
            controller: 'SQLAdminProcessListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-sqladmin-list', {
            url: "/mpr-sqladmin-list",
            templateUrl: rootUrl + 'app/extraction/views/extraction/sqladmin-list-view.html',
            controller: 'SQLAdminListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('core-onboardinguser-list', {
            url: "/core-onboardinguser-list",
            templateUrl: rootUrl + 'app/core/views/configuration/onboardinguser-list-view.html',
            controller: 'OnBoardingUserListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-teamstructureALL-edit', {
            url: "/mpr-teamstructureALL-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_core/views/teamstructureALL-edit-view.html',
            controller: 'TeamStructureALLEditController as vm'
        }).state('mpr-teamstructureALL-list', {
            url: "/mpr-teamstructureALL-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teamstructureALL-list-view.html',

            controller: 'TeamStructureALLListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomefintrakaccountssegment-edit', {
            url: "/mpr-incomefintrakaccountssegment-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_core/views/incomefintrakaccountssegment-edit-view.html',
            controller: 'IncomeFintrakAccountsSegmentEditController as vm'
        }).state('mpr-incomefintrakaccountssegment-list', {
            url: "/mpr-incomefintrakaccountssegment-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomefintrakaccountssegment-list-view.html',

            controller: 'IncomeFintrakAccountsSegmentListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-teamstructuremapping_kbl-edit', {
            url: "/mpr-teamstructuremapping_kbl-edit",
            templateUrl: rootUrl + 'app/mpr_core/views/teamstructuremapping_kbl-edit-view.html',
            controller: 'TeamStructureMappingKBLEditController as vm'

        }).state('core-usersetup_WMB-edit', {
            url: "/core-usersetup_WMB-edit",
            templateUrl: rootUrl + 'app/core/views/account/usersetup_WMB-edit-view.html',
            controller: 'UserSetupWMBEditController as vm'

        }).state('mpr-incomesplitpoolsratesandbasis-edit', {
            url: "/mpr-incomesplitpoolsratesandbasis-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomesplitpoolsratesandbasis-edit-view.html',
            controller: 'IncomeSplitPoolsRatesAndBasisEditController as vm'
        }).state('mpr-incomesplitpoolsratesandbasis-list', {
            url: "/mpr-incomesplitpoolsratesandbasis-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomesplitpoolsratesandbasis-list-view.html',

            controller: 'IncomeSplitPoolsRatesAndBasisListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeretailproductoverrideTEMP-edit', {
            url: "/mpr-incomeretailproductoverrideTEMP-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomeretailproductoverrideTEMP-edit-view.html',
            controller: 'IncomeRetailProductOverrideTEMPEditController as vm'
        }).state('mpr-incomeretailproductoverrideTEMP-list', {
            url: "/mpr-incomeretailproductoverrideTEMP-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomeretailproductoverrideTEMP-list-view.html',

            controller: 'IncomeRetailProductOverrideTEMPListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountMISoverrideTEMP-edit', {
            url: "/mpr-incomeaccountMISoverrideTEMP-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomeaccountMISoverrideTEMP-edit-view.html',
            controller: 'IncomeAccountMISOverrideTEMPEditController as vm'
        }).state('mpr-incomeaccountMISoverrideTEMP-list', {
            url: "/mpr-incomeaccountMISoverrideTEMP-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomeaccountMISoverrideTEMP-list-view.html',
            controller: 'IncomeAccountMISOverrideTEMPListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeretailproductoverrideTEMPstatus-list', {
            url: "/mpr-incomeretailproductoverrideTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomeretailproductoverrideTEMPstatus-list-view.html',
            controller: 'IncomeRetailProductOverrideTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountMISoverrideTEMPstatus-list', {
            url: "/mpr-incomeaccountMISoverrideTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomeaccountMISoverrideTEMPstatus-list-view.html',
            controller: 'IncomeAccountMISOverrideTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomecommfeebusinessrule-edit', {
            url: "/mpr-incomecommfeebusinessrule-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomecommfeebusinessrule-edit-view.html',
            controller: 'IncomeCommFeeBusinessRuleEditController as vm'
        }).state('mpr-incomecommfeebusinessrule-list', {
            url: "/mpr-incomecommfeebusinessrule-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomecommfeebusinessrule-list-view.html',
            controller: 'IncomeCommFeeBusinessRuleListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomecustomerratingoverrideTEMP-edit', {
            url: "/mpr-incomecustomerratingoverrideTEMP-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomecustomerratingoverrideTEMP-edit-view.html',
            controller: 'IncomeCustomerRatingOverrideTEMPEditController as vm'
        }).state('mpr-incomecustomerratingoverrideTEMP-list', {
            url: "/mpr-incomecustomerratingoverrideTEMP-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomecustomerratingoverrideTEMP-list-view.html',
            controller: 'IncomeCustomerRatingOverrideTEMPListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountslisting-list', {
            url: "/mpr-incomeaccountslisting-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomeaccountslisting-list-view.html',
            controller: 'IncomeAccountsListingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-ftpriskratings-edit', {
            url: "/mpr-ftpriskrattings-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/ftpriskratings-edit-view.html',
            controller: 'FTPRiskRatingsEditController as vm'
        }).state('mpr-ftpriskratings-list', {
            url: "/mpr-ftpriskratings-list",
            templateUrl: rootUrl + 'app/mpr_core/views/ftpriskratings-list-view.html',

            controller: 'FTPRiskRatingsListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomecaptionpoolrate-edit', {
            url: "/mpr-incomecaptionpoolrate-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomecaptionpoolrate-edit-view.html',
            controller: 'IncomeCaptionPoolRateEditController as vm'
        }).state('mpr-incomecaptionpoolrate-list', {
            url: "/mpr-incomecaptionpoolrate-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomecaptionpoolrate-list-view.html',

            controller: 'IncomeCaptionPoolRateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomecustomerratingoverrideTEMPstatus-list', {
            url: "/mpr-incomecustomerratingoverrideTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomecustomerratingoverrideTEMPstatus-list-view.html',
            controller: 'IncomeCustomerRatingOverrideTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-teamTEMPstatus-list', {
            url: "/mpr-teamTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teamTEMPstatus-list-view.html',
            controller: 'TeamTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeotherbreakdownTEMPstatus-list', {
            url: "/mpr-incomeotherbreakdownTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeotherbreakdownTEMPstatus-list-view.html',
            controller: 'IncomeOtherBreakdownTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomenewdetailsTEMPstatus-list', {
            url: "/mpr-incomenewdetailsTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomenewdetailsTEMPstatus-list-view.html',
            controller: 'IncomeNewDetailsTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountstreemiscodesTEMP-edit', {
            url: "/mpr-incomeaccountstreemiscodesTEMP-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountstreemiscodesTEMP-edit-view.html',
            controller: 'IncomeAccountsTreeMisCodesTEMPEditController as vm'
        }).state('mpr-incomeaccountstreemiscodesTEMP-list', {
            url: "/mpr-incomeaccountstreemiscodesTEMP-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountstreemiscodesTEMP-list-view.html',
            controller: 'IncomeAccountsTreeMisCodesTEMPListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountstreeaccountTEMP-edit', {
            url: "/mpr-incomeaccountstreeaccountTEMP-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountstreeaccountTEMP-edit-view.html',
            controller: 'IncomeAccountsTreeAccountTEMPEditController as vm'
        }).state('mpr-incomeaccountstreeaccountTEMP-list', {
            url: "/mpr-incomeaccountstreeaccountTEMP-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountstreeaccountTEMP-list-view.html',

            controller: 'IncomeAccountsTreeAccountTEMPListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountstreeMISCodesTEMPstatus-edit', {
            url: "/mpr-incomeaccountstreeMISCodesTEMPstatus-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountstreeMISCodesTEMPstatus-edit-view.html',
            controller: 'IncomeAccountsTreeMISCodesTEMPstatusEditController as vm'
        }).state('mpr-incomeaccountstreeMISCodesTEMPstatus-list', {
            url: "/mpr-incomeaccountstreeMISCodesTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountstreeMISCodesTEMPstatus-list-view.html',
            controller: 'IncomeAccountsTreeMISCodesTEMPstatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountstreeaccountTEMPstatus-edit', {
            url: "/mpr-incomeaccountstreeaccountTEMPstatus-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountstreeaccountTEMPstatus-edit-view.html',
            controller: 'IncomeAccountsTreeAccountTEMPStatusEditController as vm'
        }).state('mpr-incomeaccountstreeaccountTEMPstatus-list', {
            url: "/mpr-incomeaccountstreeaccountTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountstreeaccountTEMPstatus-list-view.html',

            controller: 'IncomeAccountsTreeAccountTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-onebankao-edit', {
            url: "/mpr-onebankao-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_core/views/onebankao-edit-view.html',
            controller: 'OneBankAOEditController as vm'
        }).state('mpr-onebankao-list', {
            url: "/mpr-onebankao-list",
            templateUrl: rootUrl + 'app/mpr_core/views/onebankao-list-view.html',

            controller: 'OneBankAOListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-onebankbranch-edit', {
            url: "/mpr-onebankbranch-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_core/views/onebankbranch-edit-view.html',
            controller: 'OneBankBranchEditController as vm'
        }).state('mpr-onebankbranch-list', {
            url: "/mpr-onebankbranch-list",
            templateUrl: rootUrl + 'app/mpr_core/views/onebankbranch-list-view.html',

            controller: 'OneBankBranchListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-onebankregiontd-edit', {
            url: "/mpr-onebankregiontd-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/onebankregiontd-edit-view.html',
            controller: 'OneBankRegionTDEditController as vm'
        }).state('mpr-onebankregiontd-list', {
            url: "/mpr-onebankregiontd-list",
            templateUrl: rootUrl + 'app/mpr_core/views/onebankregiontd-list-view.html',

            controller: 'OneBankRegionTDListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-onebankteamtable-edit', {
            url: "/mpr-onebankteamtable-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/onebankteamtable-edit-view.html',
            controller: 'OneBankTeamTableEditController as vm'
        }).state('mpr-onebankteamtable-list', {
            url: "/mpr-onebankteamtable-list",
            templateUrl: rootUrl + 'app/mpr_core/views/onebankteamtable-list-view.html',

            controller: 'OneBankTeamTableListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('incomenewdetailsTEMPstatus-list', {
            url: "/incomenewdetailsTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomenewdetailsTEMPstatus-list-view.html',

            controller: 'IncomeNewDetailsTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('incomeotherbreakdownTEMPstatus-list', {
            url: "/incomeotherbreakdownTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/incomeotherbreakdownTEMPstatus-list-view.html',

            controller: 'IncomeOtherBreakdownTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('downloadbasefintrakfinalmanualTEMPstatus-list', {
            url: "/downloadbasefintrakfinalmanualTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/downloadbasefintrakfinalmanualTEMPstatus-list-view.html',

            controller: 'DownLoadBaseFintrakFinalManualTEMPstatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-volumeanalysisrundates-edit', {
            url: "/mpr-volumeanalysisrundates-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/volumeanalysisrundates-edit-view.html',
            controller: 'VolumeAnalysisRundatesEditController as vm'
        }).state('mpr-volumeanalysisrundates-list', {
            url: "/mpr-volumeanalysisrundates-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/volumeanalysisrundates-list-view.html',

            controller: 'VolumeAnalysisRundatesListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

    }).state('mpr-mprinterestmapping-edit', {
            url: "/mpr-mprinterestmapping-edit/:ID",
            templateUrl: rootUrl + 'app/mpr_core/views/mprinterestmapping-edit-view.html',
            controller: 'MprInterestMappingEditController as vm'
        }).state('mpr-mprinterestmapping-list', {
            url: "/mpr-mprinterestmapping-list",
            templateUrl: rootUrl + 'app/mpr_core/views/mprinterestmapping-list-view.html',

            controller: 'MprInterestMappingListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

      
        }).state('mpr-misnewTEMPstatus-list', {
            url: "/mpr-misnewTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_core/views/misnewTEMPstatus-list-view.html',
            controller: 'MISNewTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-misnewothersTEMPstatus-list', {
            url: "/mpr-misnewothersTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_core/views/misnewothersTEMPstatus-list-view.html',
            controller: 'MISNewOthersTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-misnewothersTEMP-edit', {
            url: "/mpr-misnewothersTEMP-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_core/views/misnewothersTEMP-edit-view.html',
            controller: 'MISNewOthersTEMPEditController as vm'
        }).state('mpr-misnewothersTEMP-list', {
            url: "/mpr-misnewothersTEMP-list",
            templateUrl: rootUrl + 'app/mpr_core/views/misnewothersTEMP-list-view.html',
            controller: 'MISNewOthersTEMPListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountslistingTEMPstatus-list', {
            url: "/mpr-incomeaccountslistingTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomeaccountslistingTEMPstatus-list-view.html',
            controller: 'IncomeAccountsListingTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-dimaudit-list', {
            url: "/mpr-dimaudit-list",
            templateUrl: rootUrl + 'app/mpr_core/views/dimaudit-list-view.html',
            controller: 'DimAuditListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountmisoverride-list', {
            url: "/mpr-incomeaccountmisoverride-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomeaccountmisoverride-list-view.html',
            controller: 'IncomeAccountMisOverrideListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomecustomerratingoverride-list', {
            url: "/mpr-incomecustomerratingoverride-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomecustomerratingoverride-list-view.html',
            controller: 'IncomeCustomerRatingOverrideListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-teamaccessread-list', {
            url: "/mpr-teamaccessread-list",
            templateUrl: rootUrl + 'app/mpr_core/views/teamaccessread-list-view.html',
            controller: 'TeamAccessReadListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeretailproductoverride-list', {
            url: "/mpr-incomeretailproductoverride-list",
            templateUrl: rootUrl + 'app/mpr_balancesheet/views/incomeretailproductoverride-list-view.html',
            controller: 'IncomeRetailProductOverrideListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-misnew-list', {
            url: "/mpr-misnew-list",
            templateUrl: rootUrl + 'app/mpr_core/views/misnew-list-view.html',
            controller: 'MisNewListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

    

        }).state('mpr-pluploadeddataTEMPstatus-list', {
            url: "/mpr-pluploadeddataTEMPstatus-list",
            templateUrl: rootUrl + 'app/mpr_pl/views/pluploadeddataTEMPstatus-list-view.html',
            controller: 'PLUploadedDataTEMPStatusListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-misnewothers-list', {
            url: "/mpr-misnewothers-list",
            templateUrl: rootUrl + 'app/mpr_core/views/misnewothers-list-view.html',
            controller: 'MisNewOtherListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-pls-list', {
            url: "/mpr-pls-list",
            templateUrl: rootUrl + 'app/mpr_core/views/pls-list-view.html',
            controller: 'PLsListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        
        }).state('mpr-uploadcsvfile-edit', {
            url: "/mpr-uploadcsvfile-edit",
            templateUrl: rootUrl + 'app/mpr_core/views/uploadcsvfile-edit-view.html',
            controller: 'UpLoadCSVFileEditController as vm'

        }).state('mpr-incomecustomerpoolrate-edit', {
            url: "/mpr-incomecustomerpoolrate-edit/:Id",
            templateUrl: rootUrl + 'app/mpr_core/views/incomecustomerpoolrate-edit-view.html',
            controller: 'IncomeCustomerPoolRateEditController as vm'
        }).state('mpr-incomecustomerpoolrate-list', {
            url: "/mpr-incomecustomerpoolrate-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomecustomerpoolrate-list-view.html',

            controller: 'IncomeCustomerPoolRateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeaccountpoolrate-list', {
            url: "/mpr-incomeaccountrpoolrate-list",
            templateUrl: rootUrl + 'app/mpr_core/views/incomeaccountpoolrate-list-view.html',

            controller: 'IncomeAccountPoolRateListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }

        }).state('mpr-incomeproductstable-edit', {
            url: "/mpr-incomeproductstable-edit/:ProductId",
            templateUrl: rootUrl + 'app/mpr_income/views/incomeproductstable-edit-view.html',
            controller: 'IncomeProductsTableEditController as vm'

        }).state('mpr-incomeproductstable-list', {
            url: "/mpr-incomeproductstable-list",
            templateUrl: rootUrl + 'app/mpr_income/views/incomeproductstable-list-view.html',
            controller: 'IncomeProductsTableListController as vm',
            resolve: {
                loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        files: [
                            rootUrl + 'app/assets/js/plugins/dataTable/jquery.dataTables.min.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/dataTables.bootstrap.js',
                            rootUrl + 'app/assets/js/plugins/dataTable/exts/dataTables.tableTools.min.js']
                    });
                }]
            }
        });
});

App.controller('AppController', function ($scope, $rootScope, $routeParams, $location, $http, viewModelHelper) {

    $scope.userProfile = {};

    $scope.showMessage = function (title, message, type, includeDialog) {
        if (type === 'success') {
            toastr.success(message, title);
        } else if (type === 'error') {
            toastr.error(message, title);
        } else if (type === 'warning') {
            toastr.warning(message, title);
        } else if (type === 'info') {
            toastr.info(message, title);
        }

        if (includeDialog)
            alert(message);
    };

    var loadProfile = function () {
        viewModelHelper.apiGet('api/account/getuserprofile', null,
            function (result) {
                $scope.userProfile = result.data;
            },
            function (result) {
                toastr.error('Fail to load user data', 'Fintrak');
            }, null);
    };

    loadProfile();
});

// Services attached to the commonModule will be available to all other Angular modules.
commonModule.factory('viewModelHelper', function ($http, $q) {
    return Fintrak.viewModelHelper($http, $q);
});

commonModule.factory('validator', function () {
    return valJs.validator();
});

(function (se) {
    var viewModelHelper = function ($http, $q) {

        var self = this;

        self.modelIsValid = true;
        self.modelErrors = [];
        self.isLoading = false;

        self.apiGet = function (uri, data, success, failure, always) {
            self.isLoading = true;
            self.modelIsValid = true;
            $http.get(Fintrak.rootPath + uri, data)
                .then(function (result) {
                    success(result);
                    if (always !== null)
                        always();
                    self.isLoading = false;
                }, function (result) {
                    if (failure === null) {
                        if (result.status !== 400)
                            self.modelErrors = [result.status + ':' + result.statusText + ' - ' + result.data.Message];
                        else
                            self.modelErrors = [result.data.Message];
                        self.modelIsValid = false;
                    }
                    else
                        failure(result);
                    if (always !== null)
                        always();
                    self.isLoading = false;
                });
        };

        self.apiPost = function (uri, data, success, failure, always) {
            self.isLoading = true;
            self.modelIsValid = true;
            $http.post(Fintrak.rootPath + uri, data)
                .then(function (result) {
                    success(result);
                    if (always !== null)
                        always();
                    self.isLoading = false;
                }, function (result) {
                    if (failure === null) {
                        if (result.status !== 400)
                            self.modelErrors = [result.status + ':' + result.statusText + ' - ' + result.data.Message];
                        else
                            self.modelErrors = [result.data.Message];
                        self.modelIsValid = false;
                    }
                    else
                        failure(result);
                    if (always !== null)
                        always();
                    self.isLoading = false;
                });
        };

        return this;
    };
    se.viewModelHelper = viewModelHelper;
}(window.Fintrak));

(function (se) {
    var mustEqual = function (value, other) {
        return value === other;
    }
    se.mustEqual = mustEqual;
}(window.Fintrak));

// ***************** validation *****************

window.valJs = {};

(function (val) {
    var validator = function () {

        var self = this;

        self.PropertyRule = function (propertyName, rules) {
            var self = this;
            self.PropertyName = propertyName;
            self.Rules = rules;
        };

        self.ValidateModel = function (model, allPropertyRules) {
            var errors = [];
            var props = Object.keys(model);
            for (var i = 0; i < props.length; i++) {
                var prop = props[i];
                for (var j = 0; j < allPropertyRules.length; j++) {
                    var propertyRule = allPropertyRules[j];
                    if (prop === propertyRule.PropertyName) {
                        var propertyRules = propertyRule.Rules;

                        var propertyRuleProps = Object.keys(propertyRules);
                        for (var k = 0; k < propertyRuleProps.length; k++) {
                            var propertyRuleProp = propertyRuleProps[k];
                            if (propertyRuleProp !== 'custom') {
                                var rule = rules[propertyRuleProp];
                                var params = null;
                                if (propertyRules[propertyRuleProp].hasOwnProperty('params'))
                                    params = propertyRules[propertyRuleProp].params;
                                var validationResult = rule.validator(model[prop], params);
                                if (!validationResult) {
                                    errors.push(getMessage(prop, propertyRuleProp, rule.message));
                                }
                            }
                            else {
                                var validator = propertyRules.custom.validator;
                                var value = null;
                                if (propertyRules.custom.hasOwnProperty('params')) {
                                    value = propertyRules.custom.params;
                                }
                                var result = validator(model[prop], value());
                                if (result !== true) {
                                    errors.push(getMessage(prop, propertyRules.custom, 'Invalid value.'));
                                }
                            }
                        }
                    }
                }
            }

            model['errors'] = errors;
            model['isValid'] = (errors.length === 0);
        };

        var getMessage = function (prop, rule, defaultMessage) {
            var message = '';
            if (rule.hasOwnProperty('message'))
                message = rule.message;
            else
                message = prop + ': ' + defaultMessage;
            return message;
        };

        var rules = [];

        var setupRules = function () {

            rules['required'] = {
                validator: function (value, params) {
                    return !(value.toString().trim() === '');
                },
                message: 'Value is required 2.'
            };
            rules['notZero'] = {
                validator: function (value, params) {
                    return !(value === 0);
                },
                message: 'Value is must be greater than zero.'
            };
            rules['mostBePercentage'] = {
                validator: function (value, params) {
                    return !(value < 0);
                },
                message: 'Value must be greater than or equal zero.'
            };
            rules['mustBeDate'] = {
                validator: function (value, params) {
                    return (isDate(value));
                },
                message: 'Value is must be a valid date.'
            };
            rules['mustBeNumeric'] = {
                validator: function (value, params) {
                    return (isNumber(value));
                },
                message: 'Value is must be a valid number.'
            };
            rules['minLength'] = {
                validator: function (value, params) {
                    return !(value.toString().trim().length < params);
                },
                message: 'Value does not meet minimum length.'
            };
            rules['pattern'] = {
                validator: function (value, params) {
                    var regExp = new RegExp(params);
                    return !(regExp.exec(value.toString().trim()) === null);
                },
                message: 'Value must match regular expression.'
            };
        };

        function isDate(sDate) {
            if (sDate === null)
                return false;

            var scratch = new Date(sDate);
            if (scratch.toString() === 'NaN' || scratch.toString() === 'Invalid Date') {
                return false;
            } else {
                return true;
            }
        }

        function isNumber(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }

        setupRules();

        return this;
    };
    val.validator = validator;
}(window.valJs));

App.controller('NoneController', function ($scope, $routeParams) {

});

App.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

App.directive('kpiFormular', ['$rootScope', function ($rootScope) {
    return {
        link: function (scope, element, attrs) {
            $rootScope.$on('updateKPIFormular', function (e, val) {
                var domElement = element[0];

                if (document.selection) {
                    domElement.focus();
                    var sel = document.selection.createRange();
                    sel.text = val;
                    domElement.focus();
                } else if (domElement.selectionStart || domElement.selectionStart === 0) {
                    var startPos = domElement.selectionStart;
                    var endPos = domElement.selectionEnd;
                    var scrollTop = domElement.scrollTop;
                    domElement.value = domElement.value.substring(0, startPos) + val + domElement.value.substring(endPos, domElement.value.length);
                    domElement.focus();
                    domElement.selectionStart = startPos + val.length;
                    domElement.selectionEnd = startPos + val.length;
                    domElement.scrollTop = scrollTop;
                } else {
                    domElement.value += val;
                    domElement.focus();
                }

            });
        }
    };
}]);

App.directive('scoreFormular', ['$rootScope', function ($rootScope) {
    return {
        link: function (scope, element, attrs) {
            $rootScope.$on('updateScoreFormular', function (e, val) {
                var domElement = element[0];

                if (document.selection) {
                    domElement.focus();
                    var sel = document.selection.createRange();
                    sel.text = val;
                    domElement.focus();
                } else if (domElement.selectionStart || domElement.selectionStart === 0) {
                    var startPos = domElement.selectionStart;
                    var endPos = domElement.selectionEnd;
                    var scrollTop = domElement.scrollTop;
                    domElement.value = domElement.value.substring(0, startPos) + val + domElement.value.substring(endPos, domElement.value.length);
                    domElement.focus();
                    domElement.selectionStart = startPos + val.length;
                    domElement.selectionEnd = startPos + val.length;
                    domElement.scrollTop = scrollTop;
                } else {
                    domElement.value += val;
                    domElement.focus();
                }

            });
        }
    };
}]);

App.factory('httpErrorResponseInterceptor', ['$q', '$location',
    function ($q, $location) {
        return {
            response: function (responseData) {
                return responseData;
            },
            responseError: function error(response) {
                switch (response.status) {
                    case 401:
                        $location.path('/login');
                        break;
                    case 404:
                        $location.path('/404');
                        break;
                    default:
                        alert(response.data);
                    //$location.path('/error');
                }

                return $q.reject(response);
            }
        };
    }
]);

//App.factory("akFileUploaderService", ["$q", "$http",
//               function ($q, $http) {

//                   var getModelAsFormData = function (data) {
//                       var dataAsFormData = new FormData();
//                       angular.forEach(data, function (value, key) {
//                           dataAsFormData.append(key, value);
//                       });
//                       return dataAsFormData;
//                   };

//                   var saveModel = function (data, url) {
//                       var deferred = $q.defer();
//                       $http({
//                           url: url,
//                           method: "POST",
//                           data: getModelAsFormData(data),
//                           transformRequest: angular.identity,
//                           headers: { 'Content-Type': undefined }
//                       }).success(function (result) {
//                           deferred.resolve(result);
//                       }).error(function (result, status) {
//                           deferred.reject(status);
//                       });
//                       return deferred.promise;
//                   };

//                   return {
//                       saveModel: saveModel
//                   }
//               }]);

//App.directive("akFileModel", ["$parse",
//                function ($parse) {
//                    return {
//                        restrict: "A",
//                        link: function (scope, element, attrs) {
//                            var model = $parse(attrs.akFileModel);
//                            var modelSetter = model.assign;
//                            element.bind("change", function () {
//                                scope.$apply(function () {
//                                    modelSetter(scope, element[0].files[0]);
//                                });
//                            });
//                        }
//                    };
//                }]);

App.factory('Excel', function ($window) {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) { return $window.btoa(unescape(encodeURIComponent(s))); },
        format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    return {
        tableToExcel: function (tableId, worksheetName) {
            var table = $(tableId),
                ctx = { worksheet: worksheetName, table: table.html() },
                href = uri + base64(format(template, ctx));
            return href;
        }
    };
});



//App.service('yearsService', function ($http, $rootScope) {
App.service('yearsService', function ($http, $rootScope) {
    return {
        yearsFunc: function () {
            $rootScope.loading = true;
            var req = {
                method: 'GET',
                //url: url
                url: '/api/yearperiod/years'
            };

            return $http(req).then(function (result) {
                return result.data;
            });
        }
    };
});


    App.factory('uploadManager', function ($rootScope) {
        var _files = [];
        return {
            add: function (file) {
                _files.push(file);
                $rootScope.$broadcast('fileAdded', file.files[0].name);
            },
            clear: function () {
                _files = [];
            },
            files: function () {
                var fileNames = [];
                $.each(_files, function (index, file) {
                    fileNames.push(file.files[0].name);
                });
                return fileNames;
            },
            upload: function () {
                $.each(_files, function (index, file) {
                    file.submit();
                });
                this.clear();
            },
            setProgress: function (percentage) {
                $rootScope.$broadcast('uploadProgress', percentage);
            }
        };
    });


    App.directive('upload', ['uploadManager', function factory(uploadManager) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                $(element).fileupload({
                    dataType: 'text',
                    add: function (e, data) {
                        uploadManager.add(data);
                    },
                    progressall: function (e, data) {
                        var progress = parseInt(data.loaded / data.total * 100, 10);
                        uploadManager.setProgress(progress);
                    },
                    done: function (e, data) {
                        uploadManager.setProgress(0);
                    }
                });
            }
        };
    }]);

//App.service('fileUploadService', ['$http', function ($http, $q) {
//    this.uploadFileToUrl = function (file, uploadUrl) {
//            var fd = new FormData();
//            for (var key in data)
//                fd.append(key, data[key]);
//            $http.post(uploadUrl, fd, {
//                transformRequest: angular.identity,
//                //headers: { 'Content-Type': undefined }
//                headers: { 'Content-Type': 'application/json' }
//            });
//        };
//    }]);

App.service('fileUploadService', function ($http, $q) {

    this.uploadFileToUrl = function (file, uploadUrl) {
        //FormData, object of key/value pair for form fields and values
        var fileFormData = new FormData();
        fileFormData.append('csvfile', file);

        var deffered = $q.defer();
        $http.post(uploadUrl, fileFormData, {
            transformRequest: angular.identity,
            headers: {
                //'Content-Type': 'application/json',
                //'Content-Type': 'undefined',
                contentType: false,
                processData: false}


        }).success(function (response) {
            deffered.resolve(response);

        }).error(function (response) {
            deffered.reject(response);
        });

        return deffered.promise;
    };
});

App.directive('demoFileModel', function ($parse) {
    return {
        restrict: 'A', //the directive can be used as an attribute only
        link: function (scope, element, attrs) {
            var model = $parse(attrs.demoFileModel),
                modelSetter = model.assign; //define a setter for demoFileModel

            //Bind change event on the element
            element.bind('change', function () {
                //Call apply on scope, it checks for value changes and reflect them on UI
                scope.$apply(function () {
                    //set the model value
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
});



App.directive('ngFiles', ['$parse', function ($parse) {

    function fn_link(scope, element, attrs) {
        var onChange = $parse(attrs.ngFiles);
        element.on('change', function (event) {
            onChange(scope, { $files: event.target.files });
        });
    }
    return {
        link: fn_link
    };
}]);



//App.directive('demoFileModel', ['$parse', function ($parse) {
//        return {
//            restrict: 'A',
//            link: function (scope, element, attrs) {
//                var model = $parse(attrs, fileModel);
//                var modelSetter = model.assign;

//                element.bind('change', function () {
//                    scope.$apply(function () {
//                        modelSetter(scope, element[0].files[0]);
//                    });
//                });//.$scope.customer.file;
                
//            }
//        };
//    }]);


