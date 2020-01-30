/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAccountsNplEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeAccountsNplEditController]);

    function IncomeAccountsNplEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeaccountsnpl-edit-view';
        vm.viewName = 'Non Performing Loans';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeAccountsNpl = {};
     

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeAccountsNplRules = [];

        var setupRules = function () {

            incomeAccountsNplRules.push(new validator.PropertyRule("AccountNumber", {
                required: { message: "AccountNumber is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomeaccountsnpl/getincomeaccountsnpl/' + $stateParams.ID, null,
                   function (result) {
                       vm.incomeAccountsNpl = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomeAccountsNpl = { AccountNumber: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeAccountsNpl, incomeAccountsNplRules);
            vm.viewModelHelper.modelIsValid = vm.incomeAccountsNpl.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeAccountsNpl.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomeaccountsnpl/updateincomeaccountsnpl', vm.incomeAccountsNpl,
               function (result) {

                   $state.go('mpr-incomeaccountsnpl-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeAccountsNpl.errors;

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
                vm.viewModelHelper.apiPost('api/incomeaccountsnpl/deleteincomeaccountsnpl', vm.incomeAccountsNpl.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomeaccountsnpl-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomeaccountsnpl-list');
        };

        setupRules();
        initialize();
    }
}());
