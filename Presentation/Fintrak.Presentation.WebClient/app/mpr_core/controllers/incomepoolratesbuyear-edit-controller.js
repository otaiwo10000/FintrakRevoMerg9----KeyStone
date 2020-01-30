/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomePoolRateSbuYearEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomePoolRateSbuYearEditController]);

    function IncomePoolRateSbuYearEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomepoolratesbuyear-edit-view';
        vm.viewName = 'Income PoolRate Sbu Year';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomePoolRateSbuYear = {};
        vm.levelList = [{ value: 'Account', name: 'Account' }, { value: 'Team', name: 'Team' }];
        vm.currencyTypeList = [{ value: 'LCY', name: 'LCY' }, { value: 'FCY', name: 'FCY' }];
        vm.categoryList = [{ value: 'Asset', name: 'Asset' }, { value: 'Liability', name: 'Liability' }];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomePoolRateSbuYearRules = [];

        var setupRules = function () {

            incomePoolRateSbuYearRules.push(new validator.PropertyRule("Levels", {
                required: { message: "Level is required" }
            }));

            incomePoolRateSbuYearRules.push(new validator.PropertyRule("SBU", {
                required: { message: "SBU is required" }
            }));

            incomePoolRateSbuYearRules.push(new validator.PropertyRule("Rate", {
                required: { message: "Rate is required" }
            }));

            incomePoolRateSbuYearRules.push(new validator.PropertyRule("Category", {
                required: { message: "Category is required" }
            }));

            incomePoolRateSbuYearRules.push(new validator.PropertyRule("Currency_Type", {
                required: { message: "Currency_Type is required" }
            }));

            incomePoolRateSbuYearRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

            incomePoolRateSbuYearRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }))
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomepoolratesbuyear/getincomepoolratesbuyear/' + $stateParams.ID, null,
                   function (result) {
                       vm.incomePoolRateSbuYear = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomePoolRateSbuYear = { Levels: '', SBU: '', Rate: '', Category: '', Currency_Type: '', Period: '', Year: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomePoolRateSbuYear, incomePoolRateSbuYearRules);
            vm.viewModelHelper.modelIsValid = vm.incomePoolRateSbuYear.isValid;
            vm.viewModelHelper.modelErrors = vm.incomePoolRateSbuYear.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomepoolratesbuyear/updateincomepoolratesbuyear', vm.incomePoolRateSbuYear,
               function (result) {

                   $state.go('mpr-incomepoolratesbuyear-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomePoolRateSbuYear.errors;

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
                vm.viewModelHelper.apiPost('api/incomepoolratesbuyear/deleteincomepoolratesbuyear', vm.incomePoolRateSbuYear.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomepoolratesbuyear-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomepoolratesbuyear-list');
        };

        setupRules();
        initialize();
    }
}());
