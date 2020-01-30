/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeSplitPoolsRatesAndBasisEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
            IncomeSplitPoolsRatesAndBasisEditController]);

    function IncomeSplitPoolsRatesAndBasisEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomesplitpoolsratesandbasis-edit-view';
        vm.viewName = 'Income Split Poolsrates and Basis';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.icsprb = {};
        vm.yearList = [];


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.periodList = [
            { id: 1, name: "January" },
            { id: 2, name: "February" },
            { id: 3, name: "March" },
            { id: 4, name: "April" },
            { id: 5, name: "May" },
            { id: 6, name: "June" },
            { id: 7, name: "July" },
            { id: 8, name: "August" },
            { id: 9, name: "September" },
            { id: 10, name: "October" },
            { id: 11, name: "November" },
            { id: 12, name: "December" }
        ];

        var icsprbRules = [];

        var setupRules = function () {

            //icsprbRules.push(new validator.PropertyRule("Metric", {
            //    required: { message: "Metric is required" }
            //}));

            //scmRules.push(new validator.PropertyRule("Period", {
            //    required: { message: "Period is required" }
            //}));

            //scmRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));          
        };

        var initialize = function () {

            yearFunc();

            if (vm.init === false) {

                if ($stateParams.Id !== 0) {

                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomesplitpoolsratesandbasis/getincomesplitpoolsratesandbasis/' + $stateParams.Id, null,
                        function (result) {

                            vm.icsprb = result.data;

                            initialView();
                            vm.init === true;
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.icsprb = {
                        PoolRateLCYAsset: '', PoolRateLCYLiability: '', PoolRateFCYAsset: '', PoolRateFCYLiability: '',
                        Year: 0, Period: 0, Active: true
                    };
            }
        };

        var initialView = function () {

        };

        vm.save = function () {

            ////Validate
            validator.ValidateModel(vm.icsprb, icsprbRules);
            vm.viewModelHelper.modelIsValid = vm.icsprb.isValid;
            vm.viewModelHelper.modelErrors = vm.icsprb.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomesplitpoolsratesandbasis/updateincomesplitpoolsratesandbasis', vm.icsprb,
                    function (result) {

                        $state.go('mpr-incomesplitpoolsratesandbasis-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.icsprb.errors;

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
                vm.viewModelHelper.apiPost('api/incomesplitpoolsratesandbasis/deleteincomesplitpoolsratesandbasis', vm.icsprb.Id,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-incomesplitpoolsratesandbasis-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-incomesplitpoolsratesandbasis-list');
        };

        var yearFunc = function () {
            yearsService.yearsFunc()
                .then(function (data) {
                    vm.yearList = data;
                    //alert(vm.yearList);               
                }).catch(function (result) {
                    alert("Got error");
                });
        };

       

        //setupRules();
        initialize();


    }
}());
