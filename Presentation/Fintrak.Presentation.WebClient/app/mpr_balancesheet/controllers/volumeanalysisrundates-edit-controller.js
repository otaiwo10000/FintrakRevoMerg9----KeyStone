/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("VolumeAnalysisRundatesEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        VolumeAnalysisRundatesEditController]);

    function VolumeAnalysisRundatesEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'volumeanalysisrundates-edit-view';
        vm.viewName = 'Volume Analysis Rundates';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.volumeanalysisrundates = {};
        vm.visibleList = [{ value: 'off', name: 'Off' }, { value: 'on', name: 'On'}];
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var volumeanalysisrundatesRules = [];

        var setupRules = function () {

            //volumeanalysisrundatesRules.push(new validator.PropertyRule("ProductCode", {
            //    required: { message: "Product is required" }
            //}));

            //volumeanalysisrundatesRules.push(new validator.PropertyRule("GLCodeGLCode", {
            //    required: { message: "Caption is required" }
            //}));
        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/volumeanalysisrundates/getvolumeanalysisrundates/' + $stateParams.Id, null,
                        function (result) {
                            vm.volumeanalysisrundates = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.volumeanalysisrundates = { rundate: '', visible: '',  Active: true };
            }
        };

        var intializeLookUp = function () {
        };

        var initialView = function () {

        };

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.volumeanalysisrundates, volumeanalysisrundatesRules);
            vm.viewModelHelper.modelIsValid = vm.volumeanalysisrundates.isValid;
            vm.viewModelHelper.modelErrors = vm.volumeanalysisrundates.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/volumeanalysisrundates/updatevolumeanalysisrundates', vm.volumeanalysisrundates,
                    function (result) {

                        $state.go('mpr-volumeanalysisrundates-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.volumeanalysisrundates.errors;

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
                vm.viewModelHelper.apiPost('api/volumeanalysisrundates/deletevolumeanalysisrundates', vm.volumeanalysisrundates.Id,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-volumeanalysisrundates-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-volumeanalysisrundates-list');
        };

       

        setupRules();
        initialize(); 
    }
}());
