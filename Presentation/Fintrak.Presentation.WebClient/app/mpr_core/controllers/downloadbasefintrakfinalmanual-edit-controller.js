/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DownloadBaseFintrakFinalManualEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        DownloadBaseFintrakFinalManualEditController]);

    function DownloadBaseFintrakFinalManualEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'downloadbasefintrakfinalmanual-edit-view';
        vm.viewName = 'Download Base Final Manual';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ddbffm = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ddbffmRules = [];

        var setupRules = function () {

            //ddbffmRules.push(new validator.PropertyRule("Branch", {
            //    required: { message: "Branch is required" }
            //}));

            //ddbffmRules.push(new validator.PropertyRule("Percentage", {
            //    required: { message: "Percentage is required" }
            //}));

        };

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/downloadbasefintrakfinalmanual/getddbaseffm/' + $stateParams.ID, null,
                        function (result) {
                            vm.ddbffm = result.data;

                            //initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.ddbffm = {
                        AccountNumber: '', customername: '', sbuCode: '', MIS_Code: '', accountofficercode: '', accountofficer: '',
                        ActualBalance: 0, AverageBalance: 0, RevExp: 0, interestRate: 0, ProductCode: '', Category: '',
                        Currency_Type: '', postedDate: '', Period: 0, Year: 0, EntryStatus: '', GL_Sub: '',
                        Refno: '', PoolRate: 0, BankMaxRate: 0, CustomerRating: '', EffYield: 0, PenalRate: 0,
                        PenalCharge: 0, ExpRev: 0, Caption: '', Category_Filter: '', Branch: '', Share_Ratio: 0,
                        Indicator: '', Entry_Date: '', Currency_Code: '', 
                        Active: true
                    };
            }
        };


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ddbffm, ddbffmRules);
            vm.viewModelHelper.modelIsValid = vm.ddbffm.isValid;
            vm.viewModelHelper.modelErrors = vm.ddbffm.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/downloadbasefintrakfinalmanual/updateddbaseffm', vm.ddbffm,
                    function (result) {

                        $state.go('mpr-downloadbasefintrakfinalmanual-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.abcRatio.errors;

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
                vm.viewModelHelper.apiPost('api/downloadbasefintrakfinalmanual/deleteddbaseffm', vm.ddbffm.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-downloadbasefintrakfinalmanual-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-downloadbasefintrakfinalmanual-list');
        };

        setupRules();
        initialize();
    }
}());
