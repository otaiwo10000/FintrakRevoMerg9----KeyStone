/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCurrencyEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeCurrencyEditController]);

    function IncomeCurrencyEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomecurrency-edit-view';
        vm.viewName = 'Income Currency';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeCurrency = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeCurrencyRules = [];

        var setupRules = function () {

            //incomeMemorepRules.push(new validator.PropertyRule("OldMIS_Code", {
            //    required: { message: "Old Mis Code is required" }
            //}));

            //incomeMemorepRules.push(new validator.PropertyRule("NewMIS_Code", {
            //    required: { message: "New Mis Code is required" }
            //}));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomecurrency/getincomecurrency/' + $stateParams.ID, null,
                        function (result) {
                            vm.incomeCurrency = result.data;

                            //initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.incomeCurrency = { CurrencyCode: '', Rate: '', Description: '', CurrencyType: '', DateModified: '', Active: true };
            }
        };


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeCurrency, incomeCurrencyRules);
            vm.viewModelHelper.modelIsValid = vm.incomeCurrency.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeCurrency.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomecurrency/updateincomecurrency', vm.incomeCurrency,
                    function (result) {

                        $state.go('mpr-incomecurrency-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeCurrency.errors;

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
                vm.viewModelHelper.apiPost('api/incomecurrency/deleteincomecurrency', vm.incomeCurrency.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-incomecurrency-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-incomecurrency-list');
        };

        setupRules();
        initialize();
    }
}());
