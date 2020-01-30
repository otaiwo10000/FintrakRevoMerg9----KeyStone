/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeBranchesEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeBranchesEditController]);

    function IncomeBranchesEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Balancesheet';
        vm.view = 'incomebranches-edit-view';
        vm.viewName = 'Branch COdes';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.branchcodeObj = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
      
        var branchcodeRules = [];

        var setupRules = function () {

            //generaltransferpriceRules.push(new validator.PropertyRule("Category", {
            //    notZero: { message: "Category is required" }
            //}));

            //generaltransferpriceRules.push(new validator.PropertyRule("CurrencyType", {
            //    notZero: { message: "CurrencyType is required" }
            //}));

            //generaltransferpriceRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));

            //generaltransferpriceRules.push(new validator.PropertyRule("Period", {
            //    required: { message: "Period is required" }
            //}));          
        };


        var initialize = function () {
            if (vm.init === false) {
               
                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomebranches/getincomebranches/' + $stateParams.ID, null,
                        function (result) {
                            vm.branchcodeObj = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {

                        }, null);
                }
                else
                    vm.branchcodeObj = { BranchCode: '', Description: '', MIS_Code: '', State: '', Address: '', Active: true };
            }
        };

        var initialView = function () {

        };

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.branchcodeObj, branchcodeRules);
            vm.viewModelHelper.modelIsValid = vm.branchcodeObj.isValid;
            vm.viewModelHelper.modelErrors = vm.branchcodeObj.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomebranches/updateincomebranches', vm.branchcodeObj,
                    function (result) {
                        //
                        $state.go('mpr-incomebranches-list');
                    },
                    function (result) {

                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.branchcodeObj.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
            }

        };

        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/incomebranches/deleteincomebranches', vm.branchcodeObj.ID,
                    function (result) {
                        //
                        $state.go('mpr-incomebranches-list');
                    },
                    function (result) {

                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-incomebranches-list');
        };



        //setupRules();
        initialize();
    }
}());
