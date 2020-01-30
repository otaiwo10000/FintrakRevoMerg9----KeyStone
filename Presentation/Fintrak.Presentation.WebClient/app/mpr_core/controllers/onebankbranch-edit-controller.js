/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OneBankBranchEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OneBankBranchEditController]);

    function OneBankBranchEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'onebankbranch-edit-view';
        vm.viewName = 'Onebank Branch';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.onebankbranch = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var onebankbranchRules = [];

        var setupRules = function () {

            //abcratioRules.push(new validator.PropertyRule("Branch", {
            //    required: { message: "Branch is required" }
            //}));

            //abcratioRules.push(new validator.PropertyRule("Percentage", {
            //    required: { message: "Percentage is required" }
            //}));

        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/onebankbranch/getonebankbranch/' + $stateParams.Id, null,
                        function (result) {
                            vm.onebankbranch = result.data;

                            //initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.onebankbranch = { StaffName: '', BRANCH_CODE: '', GradeLevel: '', CASATarget: 0, Period: 0, Year: 0, Active: true };
            }
        };


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.onebankbranch, onebankbranchRules);
            vm.viewModelHelper.modelIsValid = vm.onebankbranch.isValid;
            vm.viewModelHelper.modelErrors = vm.onebankbranch.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/onebankbranch/updateonebankbranch', vm.onebankbranch,
                    function (result) {

                        $state.go('mpr-onebankbranch-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.onebankbranch.errors;

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
                vm.viewModelHelper.apiPost('api/onebankbranch/deleteonebankbranch', vm.onebankbranch.Id,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-onebankbranch-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-onebankbranch-list');
        };

        //setupRules();
        initialize();
    }
}());
