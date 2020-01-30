/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexGLMapEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexGLMapEditController]);

    function OpexGLMapEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'opexglmap-edit-view';
        vm.viewName = 'Opex GL Map';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.glMap = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

       
        var glmapRules = [];

        var setupRules = function () {

            //glmappingRules.push(new validator.PropertyRule("GLCode", {
            //    required: { message: "GLCode is required" }
            //}));

            //glmappingRules.push(new validator.PropertyRule("GLDescription", {
            //    required: { message: "Description is required" }
            //}));

            //glmappingRules.push(new validator.PropertyRule("Caption", {
            //    required: { message: "Caption is required" }
            //}));

            //glmappingRules.push(new validator.PropertyRule("SubCaption", {
            //    required: { message: "SubCaption is required" }
            //}));

            //glmappingRules.push(new validator.PropertyRule("SubPosition", {
            //    notZero: { message: "Sub position is required" }
            //}));

            //glmappingRules.push(new validator.PropertyRule("CompanyCode", {
            //    required: { message: "Company is required" }
            //}));


        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/opexglmap/getglmap/' + $stateParams.Id, null,
                        function (result) {
                            vm.glMap = result.data;
                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.glMap = { ACCOUNTNUMBER: '', ACCOUNT_TITLE: '', Active: true };
            }
        };

       
        var initialView = function () {

        };

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.glMap, glmapRules);
            vm.viewModelHelper.modelIsValid = vm.glMap.isValid;
            vm.viewModelHelper.modelErrors = vm.glMap.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexglmap/updateglMap', vm.glMap,
                    function (result) {

                        $state.go('opex-opexglmap-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.glMap.errors;

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
                vm.viewModelHelper.apiPost('api/opexglmap/deleteglmap', vm.glMap.GLMapId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('opex-opexglmap-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('opex-opexglmap-list');
        };

       

       
        setupRules();
        initialize(); 
    }
}());
