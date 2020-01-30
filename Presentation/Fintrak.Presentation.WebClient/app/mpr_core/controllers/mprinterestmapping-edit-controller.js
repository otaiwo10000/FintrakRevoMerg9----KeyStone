/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MprInterestMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MprInterestMappingEditController]);

    function MprInterestMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'mprinterestmapping-edit-view';
        vm.viewName = 'Mpr Interest Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mprInterestMapping = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var mprInterestMappingRules = [];

        var setupRules = function () {

           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/mprinterestmapping/getmprinterestmapping/' + $stateParams.ID, null,
                   function (result) {
                       vm.mprInterestMapping = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.mprInterestMapping = { GLCode: '', GLName: '', GLClass: '', Ispartitioned: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.mprInterestMapping, mprInterestMappingRules);
            vm.viewModelHelper.modelIsValid = vm.mprInterestMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.mprInterestMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/mprinterestmapping/updatemprinterestmapping', vm.mprInterestMapping,
               function (result) {

                   $state.go('mpr-mprinterestmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.mprInterestMapping.errors;

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
                vm.viewModelHelper.apiPost('api/mprinterestmapping/deletemprinterestmapping', vm.mprInterestMapping.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-mprinterestmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-mprinterestmapping-list');
        };

        setupRules();
        initialize();
    }
}());
