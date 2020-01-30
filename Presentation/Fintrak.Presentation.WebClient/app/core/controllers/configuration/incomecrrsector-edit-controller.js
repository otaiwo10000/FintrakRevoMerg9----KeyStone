/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCRRSectorEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeCRRSectorEditController]);

    function IncomeCRRSectorEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'incomecrrsector-edit-view';
        vm.viewName = 'Cash Reserve by Sector - Update';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ics = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var icsRules = [];

        var setupRules = function () {

            //staffRules.push(new validator.PropertyRule("StaffCode", {
            //    required: { message: "StaffCode is required" }
            //}));

            //staffRules.push(new validator.PropertyRule("Name", {
            //    required: { message: "Name is required" }
            //}));

            //staffRules.push(new validator.PropertyRule("Email", {
            //    required: { message: "Email is required" }
            //}));

        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomecrrsector/getincomecrrsector/' + $stateParams.ID, null,
                        function (result) {
                            vm.ics = result.data;

                            initialView();
                            vm.init = true;
                            //
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.staff = { SECTOR_CODE: '', SECTOR: '', CRR: 0, Active: true };
            }
        };


        var intializeLookUp = function () {
            //getCompanies();
            //getSolutions();
        };

        var initialView = function () {

        };


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ics, icsRules);
            vm.viewModelHelper.modelIsValid = vm.ics.isValid;
            vm.viewModelHelper.modelErrors = vm.ics.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomecrrsector/updateincomecrrsector', vm.ics,
                    function (result) {

                        $state.go('core-incomecrrsector-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.staff.errors;

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
                vm.viewModelHelper.apiPost('api/incomecrrsector/deleteincomecrrsector', vm.ics.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('core-incomecrrsector-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('core-incomecrrsector-list');
        };

        setupRules();
        initialize(); 
    }
}());
