/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCommFeeLineCaptionEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
            IncomeCommFeeLineCaptionEditController]);

    function IncomeCommFeeLineCaptionEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomecommfeelinecaption-edit-view';
        vm.viewName = 'MPR Income CommFee Line Caption';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.icflObj = {};
        vm.incomeLineCaptonList = [];
        //vm.plcaption2List = [];
        //vm.pprcaptionList = [];

        //vm.currencyList = [{ Value: 'LCY', Name: 'LCY' }, { Value: 'FCY', Name: 'FCY' }]
        //vm.categoryList = [{ Value: 'ASSET', Name: 'ASSET' }, { Value: 'LIABILITY', Name: 'LIABILITY' }]
        //vm.categoryFilterList = [{ Value: 'ON', Name: 'ON' }, { Value: 'OFF', Name: 'OFF' }]


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var icflRules = [];

        var setupRules = function () {

            icflRules.push(new validator.PropertyRule("Metric", {
                required: { message: "Metric Name is required" }
            }));

            icflRules.push(new validator.PropertyRule("Metric_Code", {
                required: { message: "Metric Code is required" }
            }));

            icflRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

            icflRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {

                incomelineCaptonFunc();
                //plCaption2Func();
                //pprCaptionFunc();

                if ($stateParams.ICFLcaptionId !== 0) {

                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomecommfeelinecaption/getincomecommfeelinecaption/' + $stateParams.ICFLcaptionId, null,
                        function (result) {

                            vm.icflObj = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.icflObj = {
                        GLCode: '', IncomeLineCapton: '', Description: '', 
                        GroupCode: '', GroupName: '', SubGroupCode: '',
                        Active: true
                    };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {

            ////Validate
            validator.ValidateModel(vm.icflObj, icflRules);
            vm.viewModelHelper.modelIsValid = vm.icflObj.isValid;
            vm.viewModelHelper.modelErrors = vm.icflObj.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomecommfeelinecaption/updateincomecommfeelinecaption', vm.icflObj,
                    function (result) {

                        $state.go('mpr-incomecommfeelinecaption-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.icflObj.errors;

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
                vm.viewModelHelper.apiPost('api/incomecommfeelinecaption/deleteincomecommfeelinecaption', vm.icflObj.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-incomecommfeelinecaption-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomecommfeelinecaption-list');
        }

        var incomelineCaptonFunc = function () {
            vm.viewModelHelper.apiGet('api/incomelinecapton/getallincomelinecaptons', null,
                function (result) {
                    vm.incomeLineCaptonList = result.data;
                },
                function (result) {
                    toastr.error('Fail to load users.', 'Fintrak');
                }, null);
        }




        setupRules();
        initialize();


    }
}());
