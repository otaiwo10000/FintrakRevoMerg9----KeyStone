/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCustomerPoolRateEditController",
            ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                IncomeCustomerPoolRateEditController]);

    function IncomeCustomerPoolRateEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomecustomerpoolrate-edit-view';
        vm.viewName = 'Income CustomerPoolRate';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeCustomerPoolRate = {};
        vm.customernumberdisabled = false;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeCustomerPoolRateRules = [];

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

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                    vm.customernumberdisabled = true;
                    vm.viewModelHelper.apiGet('api/incomecustomerpoolrate/getincomecustomerpoolrate/' + $stateParams.Id, null,
                        function (result) {
                            vm.incomeCustomerPoolRate = result.data;

                            //initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.incomeCustomerPoolRate = { CustomerNo: '', AccountClass: '', AccountClassName: '', PoolRate: 0, Year: 0, Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeCustomerPoolRate, incomeCustomerPoolRateRules);
            vm.viewModelHelper.modelIsValid = vm.incomeCustomerPoolRate.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeCustomerPoolRate.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomecustomerpoolrate/updateincomecustomerpoolrate', vm.incomeCustomerPoolRate,
                    function (result) {

                        $state.go('mpr-incomecustomerpoolrate-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeCustomerPoolRate.errors;

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
                vm.viewModelHelper.apiPost('api/incomecustomerpoolrate/deleteincomecustomerpoolrate', vm.incomeCustomerPoolRate.Id,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-incomecustomerpoolrate-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomecustomerpoolrate-list');
        };

        setupRules();
        initialize();
    }
}());
