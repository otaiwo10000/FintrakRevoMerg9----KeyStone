/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCommFeeMisEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeCommFeeMisEditController]);

    function IncomeCommFeeMisEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomecommfeemis-edit-view';
        vm.viewName = 'Income CommFee Mis';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeCommFeeMis = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeCommFeeMisRules = [];

        var setupRules = function () {

            incomeCommFeeMisRules.push(new validator.PropertyRule("Account", {
                required: { message: "Account is required" }
            }));

            incomeCommFeeMisRules.push(new validator.PropertyRule("MISCode", {
                required: { message: "Mis Code is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomecommfeemis/getincomecommfeemis/' + $stateParams.ID, null,
                   function (result) {
                       vm.incomeCommFeeMis = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomeCommFeeMis = { Account: '', MISCode: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeCommFeeMis, incomeCommFeeMisRules);
            vm.viewModelHelper.modelIsValid = vm.incomeCommFeeMis.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeCommFeeMis.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomecommfeemis/updateincomecommfeemis', vm.incomeCommFeeMis,
               function (result) {

                   $state.go('mpr-incomecommfeemis-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeCommFeeMis.errors;

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
                vm.viewModelHelper.apiPost('api/incomecommfeemis/deleteincomecommfeemis', vm.incomeCommFeeMis.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomecommfeemis-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomecommfeemis-list');
        };

        setupRules();
        initialize();
    }
}());
