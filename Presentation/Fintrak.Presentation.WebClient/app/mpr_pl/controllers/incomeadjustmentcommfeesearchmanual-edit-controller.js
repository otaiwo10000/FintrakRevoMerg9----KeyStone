/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeAdjustmentCommFeeSearchManualEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeAdjustmentCommFeeSearchManualEditController]);

    function IncomeAdjustmentCommFeeSearchManualEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'incomeadjustmentcommfeesearchmanual-edit-view';
        vm.viewName = 'Fee And Commission manual';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.cfObj = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var cfRules = [];

        var setupRules = function () {

            //cfRules.push(new validator.PropertyRule("Narrative", {
            //    required: { message: "Narration is required" }
            //}));

            //cfRules.push(new validator.PropertyRule("TeamCode", {
            //    required: { message: "Team Code is required" }
            //}));

            //cfRules.push(new validator.PropertyRule("AccountOfficerCode", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //cfRules.push(new validator.PropertyRule("RelatedAccount", {
            //    required: { message: "Related Account is required" }
            //}));

        };

        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.ID !== 0) {

                    vm.viewModelHelper.apiGet('api/incomeadjustmentcommfeesearchmanual/getcommfeemanual/' + $stateParams.ID, null,
                        function (result) {
                            vm.cfObj = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.cfObj = {
                        MIS_Code: '', BranchCode: '', CurrencyType: '', GL_Code: '',
                        Narrative: '', Period: '', Year: '', CustomerCode: '', CustomerName: '',
                        AccountOfficer_Code: '', Caption: '', GroupCaption: '', ProductCode: '',
                        Active: true
                    };
            }
        };

      
        var initialView = function () {

        };

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.cfObj, cfRules);
            vm.viewModelHelper.modelIsValid = vm.cfObj.isValid;
            vm.viewModelHelper.modelErrors = vm.cfObj.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomeadjustmentcommfeesearchmanual/updatecommfeemanual', vm.cfObj,
               function (result) {
                   
                   $state.go('mpr-incomeadjustmentcommfeesearchmanual-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.cfObj.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/incomeadjustmentcommfeesearchmanual/deletecommfeemanual', vm.cfObj.ID,//vm.revenue.revenueId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-incomeadjustmentcommfeesearchmanual-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-incomeadjustmentcommfeesearchmanual-list');
        };

      
        //setupRules();
        initialize();
    }
}());
