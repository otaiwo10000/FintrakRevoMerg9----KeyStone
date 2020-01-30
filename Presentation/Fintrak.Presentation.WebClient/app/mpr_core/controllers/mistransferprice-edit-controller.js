/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MISTransferPriceEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MISTransferPriceEditController]);

    function MISTransferPriceEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'mistransferprice-edit-view';
        vm.viewName = 'MIS Transfer Price';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.mistp = {};
        vm.acctText = '';

        //vm.value1 = 'Checked'
        //vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var mistpRules = [];
        vm.defcode = [];
        vm.miscode = [];
        //vm.currencyList = [];
        vm.SolutionId = '0';
        vm.CompanyCode = 'STB';

        vm.currencyList = [
            { CurrencyType: 1, CurrencyTypeName: 'LCY' },
            { CurrencyType: 2, CurrencyTypeName: 'FCY' }
        ];

        vm.bsCategoryList = [
            { BalanceSheetCategory: 2, BSCategoryName: 'Asset' },
            { BalanceSheetCategory: 3, BSCategoryName: 'Liability' }
        ];


        var setupRules = function () {   

            mistpRules.push(new validator.PropertyRule("DefinitionCode", {
                required: { message: "Definition Code is required" }
            }));

            mistpRules.push(new validator.PropertyRule("MisCode", {
                required: { message: "MisCode is required" }
            }));

            mistpRules.push(new validator.PropertyRule("Currency", {
                required: { message: "Currency is required" }
            }));

            mistpRules.push(new validator.PropertyRule("Rate", {
                required: { message: "Rate is required" }
            }));

            mistpRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

            mistpRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));          
        }

        var initialize = function () {
            if (vm.init === false) {

                
                getTeamDefinitions();
                
                if ($stateParams.mistransferpriceId !== 0) {

                    vm.acctText = 'A';
                    
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/mistransferprice/getmistransferprice/' + $stateParams.mistransferpriceId, null,
                   function (result) {

                       vm.mistp = result.data;
                       vm.teamstructurebydefcode(vm.mistp.DefinitionCode);
                       vm.aofficer(vm.mistp.DefinitionCode);
                       //vm.currencyType();
                       //vm.balancesheetcategory();

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    //vm.currencyType();
                    //vm.balancesheetcategory();

                    vm.mistp = {
                        DefinitionCode: '', MisCode: '', BalanceSheetCategory: '', CurrencyType: '',
                        Rate: '', Year: '', Period: '',
                        SolutionId: '0', CompanyCode: '', Active: true                       
                    };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {
            ////Validate
            validator.ValidateModel(vm.mistp, mistpRules);
            vm.viewModelHelper.modelIsValid = vm.mistp.isValid;
            vm.viewModelHelper.modelErrors = vm.mistp.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/mistransferprice/updatemistransferprice', vm.mistp,
               function (result) {

                   $state.go('mpr-mistransferprice-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.mistp.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/mistransferprice/deletemistransferprice', vm.mistp.mistransferpriceId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-mistransferprice-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-mistransferprice-list');
        }

        //var perspective = function () {
        //    vm.viewModelHelper.apiGet('api/scorecardperspective/getallscorecardperspective', null,
        //        function (result) {
        //            vm.perspectives = result.data;
        //        },
        //        function (result) {
        //            toastr.error('Fail to load users.', 'Fintrak');
        //        }, null);
        //}

        //var misco = function () {
        //    var code = 'ACCT';
        //    vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingdefcode/' + code, null,
        //        function (result) {
        //            vm.miscode = result.data;
        //        },
        //        function (result) {
        //            toastr.error('Fail to load users.', 'Fintrak');
        //        }, null);
        //}

        var getTeamDefinitions = function () {
            vm.viewModelHelper.apiGet('api/teamdefinition/availableteamdefinitions', null,
                function (result) {
                    vm.defcode = result.data;
                },
                function (result) {
                    toastr.error('Fail to load profit center definitions.', 'Fintrak');
                }, null);
        }

        //getteamstructureusingdefcode(code)
        vm.teamstructurebydefcode = function (code) {

                vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingdefcode/' + code, null,
                    //vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingsetup', null,
                    function (result) {
                        vm.miscode = result.data;
                    },
                    function (result) {
                        toastr.error('Fail to load team structure.', 'Fintrak');
                    }, null);
        }

        //vm.aofficer = function () {
        vm.aofficer = function (code) {

            //vm.c = 'ACCT';
            //vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingparamsANDsetup/' + vm.c + '/' + vm.acctText, null,
            vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingparamsANDsetup/' + code + '/' + vm.acctText, null,
                function (result) {
                    vm.miscode = result.data;
                },
                function (result) {
                    toastr.error('Fail to load account officer.', 'Fintrak');
                }, null);
        }

        
        //vm.currencyType = function () {
            
        //    vm.viewModelHelper.apiGet('api/enums/currencytype', null,
        //        function (result) {
        //            vm.currencyList = result.data;
        //        },
        //        function (result) {
        //            toastr.error('Fail to load currencies.', 'Fintrak');
        //        }, null);
        //}

        //vm.balancesheetcategory = function () {

        //    vm.viewModelHelper.apiGet('api/enums/balancesheetcategory', null,
        //        function (result) {
        //            vm.bsCategoryList = result.data;
        //        },
        //        function (result) {
        //            toastr.error('Fail to load currencies.', 'Fintrak');
        //        }, null);
        //}

        setupRules();
        initialize();


    }
}());
