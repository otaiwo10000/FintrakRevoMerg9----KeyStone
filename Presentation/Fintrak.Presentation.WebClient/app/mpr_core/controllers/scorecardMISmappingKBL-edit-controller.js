/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ScoreCardMISMappingKBLEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ScoreCardMISMappingKBLEditController]);

    function ScoreCardMISMappingKBLEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) { 
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'scorecardMISmapping-edit-view';
        vm.viewName = 'MPR ScoreCard MIS Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.scm = {};
        vm.metrics = [];
        vm.sccaptions = [];

        //vm.value1 = 'Checked'
        //vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var scmRules = []; 

        //var setupRules = function () {

        //    scmRules.push(new validator.PropertyRule("Metric_Code", {
        //        required: { message: "Metric Code is required" }
        //    }));

        //    scmRules.push(new validator.PropertyRule("Actual_Caption", {
        //        required: { message: "Actual Caption is required" }
        //    }));

        //    scmRules.push(new validator.PropertyRule("Budget_Caption", {
        //        required: { message: "Budget Caption is required" }
        //    }));      
        //}

        var initialize = function () {
            if (vm.init === false) {

                scKPITypeKBL();
                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/scorecardMISmappingKBL/getscorecardMISmappingKBL/' + $stateParams.ID, null,
                        function (result) {

                            vm.scm = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.scm = {
                        MIS: '', KPI_TYPE: '', Period: 0, Year: 0, Active: true
                    };
            }
        };


        var initialView = function () {

        };

       
        var scKPITypeKBL = function () {
            vm.viewModelHelper.apiGet('api/scorecardKPItypesKBL/getallscorecardKPItypesKBL', null,
                function (result) {
                    vm.sckpitype = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        vm.save = function () {
            ////Validate
            validator.ValidateModel(vm.scm, scmRules);
            vm.viewModelHelper.modelIsValid = vm.scm.isValid;
            vm.viewModelHelper.modelErrors = vm.scm.errors;
            if (vm.viewModelHelper.modelIsValid) {

               
                vm.viewModelHelper.apiPost('api/scorecardMISmappingKBL/updatescorecardMISmappingKBL', vm.scm,
                    function (result) {

                        $state.go('mpr-scorecardMISmappingKBL-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.scm.errors;

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
                vm.viewModelHelper.apiPost('api/scorecardMISmappingKBL/deletescorecardMISmappingKBL', vm.scm.ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-scorecardMISmappingKBL-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            $state.go('mpr-scorecardMISmappingKBL-list');
        };


        //mprNumerator();
        //mprDenominator();
        //setupRules();
        initialize();


    }
}());
