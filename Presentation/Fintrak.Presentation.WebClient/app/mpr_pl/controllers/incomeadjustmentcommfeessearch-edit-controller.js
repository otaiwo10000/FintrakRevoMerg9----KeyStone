/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAdjustmentCommFeesSearchEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeAdjustmentCommFeesSearchEditController]);

    function IncomeAdjustmentCommFeesSearchEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'incomeadjustmentcommfeessearch-edit-view';
        vm.viewName = 'Comm and Fees Raw Adjustment';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.cf = {};
        vm.partshow = true;
        vm.remshow = false;
       

       

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

                    vm.refnoshow = true;
                    vm.viewModelHelper.apiGet('api/incomeadjustmentcommfeessearch/incomeadjustmentcommfeessearchusingid/' + $stateParams.Id, null,
                        function (result) {
                            vm.cf = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else {  
                    vm.remshow = true;
                   vm.cf = {
                       MIS_Code: '', BranchCode: '', Inc_Exp: '', Amount: '', CurrencyType: '', GL_Code: '', Sub_Head_GL_Code: '', RelatedAccount: '',
                       Narrative: '', CustomerName: '', AccountOfficer_Code: '', Caption: '', username: '', Active: true
                    };
                }
            }
        };
      
        var initialView = function () {

        };

        vm.save = function () {
            ////Validate
            //validator.ValidateModel(vm.vol, iavRules);
            vm.viewModelHelper.modelIsValid = vm.cf.isValid;
            vm.viewModelHelper.modelErrors = vm.cf.errors;


            //if (vm.viewModelHelper.modelIsValid) {

                if ($stateParams.Id === '0') {
                   
                    vm.cf = {
                        MIS_Code: vm.cf.MIS_Code, BranchCode: vm.cf.BranchCode, Inc_Exp: vm.cf.Inc_Exp, Amount: vm.cf.Amount, CurrencyType: vm.cf.CurrencyType,
                        GL_Code: vm.cf.GL_Code, Sub_Head_GL_Code: vm.cf.Sub_Head_GL_Code, RelatedAccount: vm.cf.RelatedAccount, Narrative: vm.cf.Narrative,
                        CustomerName: vm.cf.CustomerName, AccountOfficer_Code: vm.cf.AccountOfficer_Code, Caption: vm.cf.Caption, username: vm.cf.username, Active: true
                    };

                    vm.viewModelHelper.apiPost('api/incomeadjustmentcommfeessearch/addincomeadjustmentcommfeessearch', vm.cf,

                    function (result) {

                        $state.go('mpr-incomeadjustmentcommfeessearch-list');
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else {
                    //vm.cf = {
                    //    MISCODE: vm.cf.MIS_Code, BranchCode: vm.cf.BranchCode, Inc_Exp: vm.cf.Inc_Exp, Amount: vm.cf.Amount, CurrencyType: vm.cf.CurrencyType,
                    //    GL_Code: vm.cf.GL_Code, Sub_Head_GL_Code: vm.cf.Sub_Head_GL_Code, RelatedAccount: vm.cf.RelatedAccount, Narrative: vm.cf.Narrative,
                    //    CustomerName: vm.cf.CustomerName, AccountOfficer_Code: vm.cf.AccountOfficer_Code, Caption: vm.cf.Caption, username: vm.cf.username, Active: true
                    //};
                    vm.cf = {
                        MIS_Code: vm.cf.MIS_Code, Amount: vm.cf.Amount, AccountOfficer_Code: vm.cf.AccountOfficer_Code, Narrative: vm.cf.Narrative,
                        RelatedAccount: vm.cf.RelatedAccount, Active: true
                    };
                    vm.viewModelHelper.apiPost('api/incomeadjustmentcommfeessearch/updateincomeadjustmentcommfeessearchACCESS', vm.cf,

                    function (result) {

                        $state.go('mpr-incomeadjustmentcommfeessearch-list');
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
                vm.viewModelHelper.apiPost('api/incomeadjustmentcommfeessearch/sth', vm.cf.ID, vm.cf,//vm.incomecentralvaultschedule.incomecentralvaultscheduleId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-incomeadjustmentcommfeessearch-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-incomeadjustmentcommfeessearch-list');
        };

       

        
        //setupRules();
        initialize();
    }
}());
