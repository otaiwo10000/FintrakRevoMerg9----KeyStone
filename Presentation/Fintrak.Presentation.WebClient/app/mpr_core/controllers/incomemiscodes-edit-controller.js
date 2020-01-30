/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeMisCodesEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeMisCodesEditController]);

    function IncomeMisCodesEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomemiscodes-edit-view';
        vm.viewName = 'Income Mis Codes';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeMisCodes = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeMisCodesRules = [];

        var setupRules = function () {

            incomeMisCodesRules.push(new validator.PropertyRule("OldMIS_Code", {
                required: { message: "Old Mis Code is required" }
            }));

            incomeMisCodesRules.push(new validator.PropertyRule("NewMIS_Code", {
                required: { message: "New Mis Code is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomemiscodes/getincomemiscodes/' + $stateParams.ID, null,
                   function (result) {
                       vm.incomeMisCodes = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomeMisCodes = { OldMIS_Code: '', NewMIS_Code: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeMisCodes, incomeMisCodesRules);
            vm.viewModelHelper.modelIsValid = vm.incomeMisCodes.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeMisCodes.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomemiscodes/updateincomemiscodes', vm.incomeMisCodes,
               function (result) {

                   $state.go('mpr-incomemiscodes-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeMisCodes.errors;

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
                vm.viewModelHelper.apiPost('api/incomemiscodes/deleteincomemiscodes', vm.incomeMisCodes.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomemiscodes-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomemiscodes-list');
        };

        setupRules();
        initialize();
    }
}());
