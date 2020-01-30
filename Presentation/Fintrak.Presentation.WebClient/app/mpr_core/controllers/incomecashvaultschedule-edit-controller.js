/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCashVaultScheduleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeCashVaultScheduleEditController]);

    function IncomeCashVaultScheduleEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomecashvaultschedule-edit-view';
        vm.viewName = 'Income Cash Vault Schedule';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ics = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var icsRules = [];

        var setupRules = function () {

            //icsRules.push(new validator.PropertyRule("Branch", {
            //    required: { message: "Branch is required" }
            //}));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomecashvaultschedule/getincomecashvaultschedule/' + $stateParams.ID, null,
                   function (result) {
                       vm.ics = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.ics = { AccountNumber: '', Branch: '', Originator: '', Ratio: 0, Period: 0, Year: 0, Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ics, icsRules);
            vm.viewModelHelper.modelIsValid = vm.ics.isValid;
            vm.viewModelHelper.modelErrors = vm.ics.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomecashvaultschedule/UpdateIncomeCashVaultSchedule', vm.ics,
               function (result) {

                   $state.go('mpr-incomecashvaultschedule-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.ics.errors;

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
                vm.viewModelHelper.apiPost('api/incomecashvaultschedule/DeleteIncomeCashVaultSchedule', vm.ics.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomecashvaultschedule-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomecashvaultschedule-list');
        };

        setupRules();
        initialize();
    }
}());
