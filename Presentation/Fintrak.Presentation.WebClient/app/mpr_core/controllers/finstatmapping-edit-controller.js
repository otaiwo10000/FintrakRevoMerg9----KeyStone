/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FinstatMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        FinstatMappingEditController]);

    function FinstatMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'finstatmapping-edit-view';
        vm.viewName = 'FINSTAT MAPPING';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.finstatMappings = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var finstatmappingRules = [];

        var setupRules = function () {

            finstatmappingRules.push(new validator.PropertyRule("Description", {
                required: { message: "Description is required" }
            }));

            finstatmappingRules.push(new validator.PropertyRule("MainCaption", {
                required: { message: "MainCaption is required" }
            }));

            finstatmappingRules.push(new validator.PropertyRule("SubCaption", {
                required: { message: "SubCaption is required" }
            }));

            finstatmappingRules.push(new validator.PropertyRule("SubSubCaption", {
                required: { message: "SubSubCaption is required" }
            }));

            finstatmappingRules.push(new validator.PropertyRule("Class", {
                required: { message: "Class is required" }
            }));

            finstatmappingRules.push(new validator.PropertyRule("RefNote", {
                required: { message: "RefNote is required" }
            }));

            finstatmappingRules.push(new validator.PropertyRule("Position", {
                required: { message: "Position is required" }
            }));

            finstatmappingRules.push(new validator.PropertyRule("PARENTGL", {
                required: { message: "PARENTGL is required" }
            }));

            finstatmappingRules.push(new validator.PropertyRule("SubPosition", {
                required: { message: "SubPosition is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.finstatmappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/finstatmapping/getfinstatmapping/' + $stateParams.finstatmappingId, null,
                   function (result) {
                       vm.finstatMapping = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.finstatMapping = {
                        Description: '', MainCaption: '', SubCaption: '', SubSubCaption: '',
                        Class: 0, RefNote: '', Position: 0, PARENTGL: '', SubPosition: 0, Active: true
                    };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.finstatMapping, finstatmappingRules);
            vm.viewModelHelper.modelIsValid = vm.finstatMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.finstatMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/finstatmapping/updatefinstatmapping', vm.finstatMapping,
               function (result) {

                   $state.go('mpr-finstatmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.finstatMapping.errors;

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
                vm.viewModelHelper.apiPost('api/finstatmapping/deletefinstatmapping', vm.finstatMapping.finstatMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-finstatmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-finstatmapping-list');
        };

        setupRules();
        initialize();
    }
}());
