/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAccountsTreeMisCodesEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeAccountsTreeMisCodesEditController]);

    function IncomeAccountsTreeMisCodesEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeaccountstreemiscodes-edit-view';
        vm.viewName = 'Income AccountsTree MisCodes';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeAccountsTreeMisCodes = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeAccountsTreeMisCodesRules = [];

        var setupRules = function () {

            incomeAccountsTreeMisCodesRules.push(new validator.PropertyRule("AccountNumber", {
                required: { message: "AccountNumber is required" }
            }));

            incomeAccountsTreeMisCodesRules.push(new validator.PropertyRule("AccountOfficer_Code", {
                required: { message: "AccountOfficer_Code is required" }
            }));

            incomeAccountsTreeMisCodesRules.push(new validator.PropertyRule("AccountOfficerName", {
                required: { message: "AccountOfficerName is required" }
            }));

            incomeAccountsTreeMisCodesRules.push(new validator.PropertyRule("ShareRatio", {
                required: { message: "ShareRatio is required" }
            }));

            incomeAccountsTreeMisCodesRules.push(new validator.PropertyRule("Ratio", {
                required: { message: "Ratio is required" }
            }));

            incomeAccountsTreeMisCodesRules.push(new validator.PropertyRule("Team_Code", {
                required: { message: "Team_Code is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomeaccountstreemiscodes/getincomeaccountstreemiscodes/' + $stateParams.ID, null,
                   function (result) {
                       vm.incomeAccountsTreeMisCodes = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomeAccountsTreeMisCodes = { AccountNumber: '', AccountOfficer_Code: '', AccountOfficerName: '', ShareRatio: '', Ratio: '', Team_Code: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeAccountsTreeMisCodes, incomeAccountsTreeMisCodesRules);
            vm.viewModelHelper.modelIsValid = vm.incomeAccountsTreeMisCodes.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeAccountsTreeMisCodes.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomeaccountstreemiscodes/updateincomeaccountstreemiscodes', vm.incomeAccountsTreeMisCodes,
               function (result) {

                   $state.go('mpr-incomeaccountstreemiscodes-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeAccountsTreeMisCodes.errors;

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
                vm.viewModelHelper.apiPost('api/incomeaccountstreemiscodes/deleteincomeaccountstreemiscodes', vm.incomeAccountsTreeMisCodes.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomeaccountstreemiscodes-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomeaccountstreemiscodes-list');
        };

        setupRules();
        initialize();
    }
}());
