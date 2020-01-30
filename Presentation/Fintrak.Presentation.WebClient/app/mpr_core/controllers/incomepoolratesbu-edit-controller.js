/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomePoolRateSbuEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomePoolRateSbuEditController]);

    function IncomePoolRateSbuEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomepoolratesbu-edit-view';
        vm.viewName = 'Income PoolRate Sbu';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomePoolRateSbu = {};
        vm.levelList = [{ value: 'Account', name:'Account' }, { value: 'Team', name:'Team' }];
        vm.currencyTypeList = [{ value: 'LCY', name: 'LCY' }, { value: 'FCY', name: 'FCY' }];
        vm.categoryList = [{ value: 'Asset', name: 'Asset' }, { value: 'Liability', name: 'Liability' }];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomePoolRateSbuRules = [];

        var setupRules = function () {

            incomePoolRateSbuRules.push(new validator.PropertyRule("Levels", {
                required: { message: "Level is required" }
            }));

            incomePoolRateSbuRules.push(new validator.PropertyRule("SBU", {
                required: { message: "SBU is required" }
            }));

            incomePoolRateSbuRules.push(new validator.PropertyRule("Rate", {
                required: { message: "Rate is required" }
            }));

            incomePoolRateSbuRules.push(new validator.PropertyRule("Category", {
                required: { message: "Category is required" }
            }));

            incomePoolRateSbuRules.push(new validator.PropertyRule("Currency_Type", {
                required: { message: "Currency_Type is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomepoolratesbu/getincomepoolratesbu/' + $stateParams.ID, null,
                   function (result) {
                       vm.incomePoolRateSbu = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomePoolRateSbu = { Levels: '', SBU: '', Rate: '', Category: '', Currency_Type: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomePoolRateSbu, incomePoolRateSbuRules);
            vm.viewModelHelper.modelIsValid = vm.incomePoolRateSbu.isValid;
            vm.viewModelHelper.modelErrors = vm.incomePoolRateSbu.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomepoolratesbu/updateincomepoolratesbu', vm.incomePoolRateSbu,
               function (result) {

                   $state.go('mpr-incomepoolratesbu-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomePoolRateSbu.errors;

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
                vm.viewModelHelper.apiPost('api/incomepoolratesbu/deleteincomepoolratesbu', vm.incomePoolRateSbu.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomepoolratesbu-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomepoolratesbu-list');
        };

        setupRules();
        initialize();
    }
}());
