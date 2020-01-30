/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAdjustmentVolumeSearchEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeAdjustmentVolumeSearchEditController]);

    function IncomeAdjustmentVolumeSearchEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'incomeadjustmentvolumesearch-edit-view';
        vm.viewName = 'Details Adjustment';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.vol = {};
        vm.balshow = false;
        vm.avgshow = false;
        vm.intshow = false;
        vm.capshow = false;
        vm.refnoshow = false;

        vm.categoryList = [{ value: 'Asset', name: 'Asset' }, { value: 'Liability', name: 'Liability' }];
        vm.currencyTypeList = [{ value: 'LCY', name: 'LCY' }, { value: 'FCY', name: 'FCY' }];

        vm.yearList = [{ value: 2016, name: '2016' },
        { value: 2017, name: '2017' },
        { value: 2018, name: '2018' },
        { value: 2019, name: '2019' },
        { value: 2020, name: '2020' },
        { value: 2021, name: '2021' },
        { value: 2022, name: '2022' },
        { value: 2023, name: '2023' }];

        vm.periodList = [{ value: 1, name: 'January' },
        { value: 2, name: 'Febuary' },
        { value: 3, name: 'Mar' },
        { value: 4, name: 'Apr' },
        { value: 5, name: 'May' },
        { value: 6, name: 'June' },
        { value: 7, name: 'July' },
        { value: 8, name: 'August' },
        { value: 9, name: 'September' },
        { value: 10, name: 'October' },
        { value: 11, name: 'November' },
        { value: 12, name: 'December' }];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var iavRules = [];

        var setupRules = function () {

            //incomecentralvaultscheduleRules.push(new validator.PropertyRule("BranchCode", {
            //    required: { message: "Branch Code is required" }
            //}));

            //incomecentralvaultscheduleRules.push(new validator.PropertyRule("Currency", {
            //    required: { message: "Currency is required" }
            //}));

            //incomecentralvaultscheduleRules.push(new validator.PropertyRule("Volume", {
            //    required: { message: "Volume is required" }
            //}));

            //incomecentralvaultscheduleRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));

            //incomecentralvaultscheduleRules.push(new validator.PropertyRule("Period", {
            //    required: { message: "Period is required" }
            //}));

        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.Id !== '0') {

                    vm.capshow = true;
                    vm.refnoshow = true;
                    vm.viewModelHelper.apiGet('api/incomeadjustmentvolumesearch/incomeadjustmentvolumesearchusingid/' + $stateParams.Id, null,
                        function (result) {
                            vm.vol = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else {
                    vm.balshow = true;
                    vm.avgshow = true;
                    vm.inteshow = true;
                   vm.vol = {
                       MIS_Code: '', accountofficercode: '', AccountNumber: '', customername: '', ActualBalance: '', AverageBalance: '', RevExp: '', ProductCode: '', Category: '', Currency_Type: '',
                       Active: true
                    };
                    //vm.vol = { MIS_Code: '', ACCTCODE: '', AccountNumber: '', customername: '', BALANCE: '', AVERAGE: '', INTEREST: '', ProductCode: '', Category: '', CURRENCY: '', Active: true };
                    //vm.vol = { MISCODE:'', ACCTCODE:'', ACCOUNTNUMBER:'', CUSTNAME:'', BALANCE:'', AVERAGE:'', INTEREST:'', PRODUCTCODE:'', CATEGORY:'', CURRENCY:'', Active: true };
                }
            }
        };
      
        var initialView = function () {

        };

        vm.save = function () {
            ////Validate
            //validator.ValidateModel(vm.vol, iavRules);
            vm.viewModelHelper.modelIsValid = vm.vol.isValid;
            vm.viewModelHelper.modelErrors = vm.vol.errors;


            //if (vm.viewModelHelper.modelIsValid) {

                if ($stateParams.Id === '0') {

                    //vm.vol = { MISCODE: vm.vol.MIS_Code, ACCTCODE: vm.vol.ACCTCODE, ACCOUNTNUMBER: vm.vol.AccountNumber, CUSTNAME: vm.vol.customername, BALANCE: vm.vol.BALANCE, AVERAGE: vm.vol.AVERAGE, INTEREST: vm.vol.INTEREST, ProductCode: vm.vol.ProductCode, Category: vm.vol.Category, CURRENCY: vm.vol.CURRENCY, Active: true };
                    vm.vol = { MISCODE: vm.vol.MIS_Code, ACCTCODE: vm.vol.accountofficercode, ACCOUNTNUMBER: vm.vol.AccountNumber, CUSTNAME: vm.vol.customername, BALANCE: vm.vol.ActualBalance, AVERAGE: vm.vol.AverageBalance, INTEREST: vm.vol.RevExp, ProductCode: vm.vol.ProductCode, Category: vm.vol.Category, CURRENCY: vm.vol.Currency_Type, Active: true };

                    //vm.viewModelHelper.apiPost('api/incomeadjustmentvolumesearch/addincomeadjustmentvol', vm.vol.MISCODE, vm.vol.ACCTCODE, vm.vol.ACCOUNTNUMBER, vm.vol.CUSTNAME, vm.vol.BALANCE, vm.vol.AVERAGE, vm.vol.INTEREST, vm.vol.PRODUCTCODE, vm.vol.CATEGORY, vm.vol.CURRENCY,
                    vm.viewModelHelper.apiPost('api/incomeadjustmentvolumesearch/addincomeadjustmentvol', vm.vol,

                    function (result) {

                        $state.go('mpr-incomeadjustmentvolumesearch-list');
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else {

                    vm.vol = { ID: vm.vol.ID, MISCODE: vm.vol.MIS_Code, ACCTCODE: vm.vol.accountofficercode, PERIOD: vm.vol.Period, YEAR: vm.vol.Year, ACCOUNTNUMBER: vm.vol.AccountNumber, ProductCode: vm.vol.ProductCode, Category: vm.vol.Category, CURRENCY: vm.vol.Currency_Type, CUSTNAME: vm.vol.customername, CAPTION: vm.vol.Caption, ACCOUNTNUMBER1: vm.vol.Refno, Active: true };

                    //vm.viewModelHelper.apiPost('api/incomeadjustmentvolumesearch/updateincomeadjustmentvol', vm.vol.MISCODE, vm.vol.ACCTCODE, vm.vol.PERIOD, vm.vol.YEAR, vm.vol.ACCOUNTNUMBER, vm.vol.PRODUCTCODE, vm.vol.CATEGORY, vm.vol.CURRENCY, vm.vol.CUSTNAME, vm.vol.CAPTION, vm.vol.ACCOUNTNUMBER1,
                    vm.viewModelHelper.apiPost('api/incomeadjustmentvolumesearch/updateincomeadjustmentvol', vm.vol,

                    function (result) {

                        $state.go('mpr-incomeadjustmentvolumesearch-list');
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }

            //}
            //else {
            //    vm.viewModelHelper.modelErrors = vm.vol.errors;

            //    var errorList = '';

            //    angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
            //        errorList += error + '<br>';
            //    });

            //    toastr.error(errorList, 'Fintrak');
            //}

        };

        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/incomeadjustmentvolumesearch/deleteincomeadjustmentvol', vm.vol.ID, vm.vol,//vm.incomecentralvaultschedule.incomecentralvaultscheduleId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-incomeadjustmentvolumesearch-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-incomeadjustmentvolumesearch-list');
        };

       

        
        //setupRules();
        initialize();
    }
}());
