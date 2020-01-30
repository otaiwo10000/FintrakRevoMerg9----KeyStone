/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OneBankAOEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OneBankAOEditController]);

    function OneBankAOEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'onebankao-edit-view';
        vm.viewName = 'Onebank AO';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.onebankao = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var onebankaoRules = [];

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
                    vm.viewModelHelper.apiGet('api/onebankao/getonebankao/' + $stateParams.Id, null,
                        function (result) {
                            vm.onebankao = result.data;

                            //initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.onebankao = { AccountOfficerCode: '', GradeLevel: '', CASATarget: 0, Period: 0, Year: 0, Active: true };
            }
        };


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.onebankao, onebankaoRules);
            vm.viewModelHelper.modelIsValid = vm.onebankao.isValid;
            vm.viewModelHelper.modelErrors = vm.onebankao.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/onebankao/updateonebankao', vm.onebankao,
                    function (result) {

                        $state.go('mpr-onebankao-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.onebankao.errors;

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
                vm.viewModelHelper.apiPost('api/onebankao/deleteonebankao', vm.onebankao.Id,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-onebankao-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-onebankao-list');
        };

        //setupRules();
        initialize();
    }
}());
