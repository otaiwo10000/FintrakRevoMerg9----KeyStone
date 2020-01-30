/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAccountsTreeAccountTEMPEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeAccountsTreeAccountTEMPEditController]);

    function IncomeAccountsTreeAccountTEMPEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeaccountstreeaccountTEMP-edit-view';
        vm.viewName = 'Income AccountsTree Account';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeAccountsTreeAccount = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeAccountsTreeAccountRules = [];

        var setupRules = function () {

            incomeAccountsTreeAccountRules.push(new validator.PropertyRule("AccountNumber", {
                required: { message: "AccountNumber is required" }
            }));

            incomeAccountsTreeAccountRules.push(new validator.PropertyRule("ShareReason", {
                required: { message: "ShareReason is required" }
            }));

            incomeAccountsTreeAccountRules.push(new validator.PropertyRule("ExpirationPeriod", {
                required: { message: "ExpirationPeriod is required" }
            }));

            incomeAccountsTreeAccountRules.push(new validator.PropertyRule("ExpirationYear", {
                required: { message: "ExpirationYear is required" }
            }));


        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomeaccountstreeaccounttemp/getincomeaccountstreeaccounttemp/' + $stateParams.ID, null,
                        function (result) {
                            vm.incomeAccountsTreeAccount = result.data;

                            //initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.incomeAccountsTreeAccount = { AccountNumber: '', ShareReason: '', ExpirationPeriod: '', ExpirationYear: '', Active: true };
            }
        };


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeAccountsTreeAccount, incomeAccountsTreeAccountRules);
            vm.viewModelHelper.modelIsValid = vm.incomeAccountsTreeAccount.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeAccountsTreeAccount.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomeaccountstreeaccounttemp/updateincomeaccountstreeaccounttemp', vm.incomeAccountsTreeAccount,
                    function (result) {

                        $state.go('mpr-incomeaccountstreeaccountTEMP-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeAccountsTreeAccount.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        };

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/incomeaccountstreeaccounttemp/deleteincomeaccountstreeaccounttemp', vm.incomeAccountsTreeAccount.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-incomeaccountstreeaccountTEMP-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-incomeaccountstreeaccountTEMP-list');
        };

        setupRules();
        initialize();
    }
}());
