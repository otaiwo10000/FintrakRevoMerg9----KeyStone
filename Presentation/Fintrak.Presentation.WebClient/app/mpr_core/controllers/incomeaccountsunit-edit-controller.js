/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAccountsUnitEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeAccountsUnitEditController]);

    function IncomeAccountsUnitEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomeaccountsunit-edit-view';
        vm.viewName = 'Special Unit Account';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeAccountsUnit = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeAccountsUnitRules = [];

        var setupRules = function () {

            //incomeMemorepRules.push(new validator.PropertyRule("OldMIS_Code", {
            //    required: { message: "Old Mis Code is required" }
            //}));

            //incomeMemorepRules.push(new validator.PropertyRule("NewMIS_Code", {
            //    required: { message: "New Mis Code is required" }
            //}));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomeaccountsunit/getincomeaccountsunit/' + $stateParams.ID, null,
                   function (result) {
                       vm.incomeAccountsUnit = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomeAccountsUnit = { AccountNumber: '', Sector_Code: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeAccountsUnit, incomeAccountsUnitRules);
            vm.viewModelHelper.modelIsValid = vm.incomeAccountsUnit.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeAccountsUnit.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomeaccountsunit/updateincomeaccountsunit', vm.incomeAccountsUnit,
               function (result) {

                   $state.go('mpr-incomeaccountsunit-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeAccountsUnit.errors;

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
                vm.viewModelHelper.apiPost('api/incomeaccountsunit/deleteincomeaccountsunit', vm.incomeAccountsUnit.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomeaccountsunit-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomeaccountsunit-list');
        };

        setupRules();
        initialize();
    }
}());
