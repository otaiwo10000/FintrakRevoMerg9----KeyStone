/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCaptionPoolRateEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeCaptionPoolRateEditController]);

    function IncomeCaptionPoolRateEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomecaptionpoolrate-edit-view';
        vm.viewName = 'Income CaptionPoolRate';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeCaptionPoolRate = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeCaptionPoolRateRules = [];
        vm.captionnumberdisabled = false;

        vm.ComputeInterestList = [{ name: 'True', value: true }, { name: 'False', value: false }];
        //vm.category = [
        //    { Id: 2, Name: 'Asset' },
        //    { Id: 3, Name: 'Liability' }
        //];

        var setupRules = function () {

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.captionnumberdisabled = true;
                    vm.viewModelHelper.apiGet('api/incomecaptionpoolrate/getincomecaptionpoolrate/' + $stateParams.ID, null,
                   function (result) {
                       vm.incomeCaptionPoolRate = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomeCaptionPoolRate = { Caption: '', Pool_rate: '', ComputeInterest: '', Year: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeCaptionPoolRate, incomeCaptionPoolRateRules);
            vm.viewModelHelper.modelIsValid = vm.incomeCaptionPoolRate.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeCaptionPoolRate.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomecaptionpoolrate/updateincomecaptionpoolrate', vm.incomeCaptionPoolRate,
               function (result) {

                   $state.go('mpr-incomecaptionpoolrate-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeCaptionPoolRate.errors;

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
                vm.viewModelHelper.apiPost('api/incomecaptionpoolrate/deleteincomecaptionpoolrate', vm.incomeCaptionPoolRate.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomecaptionpoolrate-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomecaptionpoolrate-list');
        };

        setupRules();
        initialize();
    }
}());
