/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ScoreCardMetricsKBLEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
            ScoreCardMetricsKBLEditController]);

    function ScoreCardMetricsKBLEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'scorecardmetricsKBL-edit-view';
        vm.viewName = 'ScoreCard Metrics';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.scm = {};
        vm.bsotherinformation = [];

        //vm.value1 = 'Checked'
        //vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var scmRules = [];
      
        
        vm.YTDActionList = [
            { value: 'AVERAGE', name: 'AVERAGE' },
            { value: 'SPOT', name: 'SPOT' },
            { value: 'SUM', name: 'SUM' },
            { value: 'MAX', name: 'MAX' }
        ];

        vm.boolList = [
            { value: 0, name: 'False' },
            { value: 1, name: 'True' }
        ];

        var setupRules = function () {

            scmRules.push(new validator.PropertyRule("Metric", {
                required: { message: "Metric is required" }
            }));

            //scmRules.push(new validator.PropertyRule("Metric_Code", {
            //    required: { message: "Metric Code is required" }
            //}));

            //scmRules.push(new validator.PropertyRule("Period", {
            //    required: { message: "Period is required" }
            //}));

            //scmRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));          
        }

        var initialize = function () {

            getBSOtherInfo();
            if (vm.init === false) {

                if ($stateParams.MetricID !== 0) {

                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/scorecardmetricsKBL/getscorecardmetricsKBL/' + $stateParams.MetricID, null,
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
                        Metric_Description: '', Metric: '', Actual: '', Budget: '',
                        TargetIsPreviousYear: false, TargetOverActual: false, Divisior: '',
                        Position: '', Year: '', SetToZeroIfNoBudget: false, YTDAction: '',
                        SetToZeroIfNegativeActual: false, Active: true                                             
                    };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {           

            ////Validate
            validator.ValidateModel(vm.scm, scmRules);
            vm.viewModelHelper.modelIsValid = vm.scm.isValid;
            vm.viewModelHelper.modelErrors = vm.scm.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/scorecardmetricsKBL/updatescorecardmetricsKBL', vm.scm,
               function (result) {

                   $state.go('mpr-scorecardmetricsKBL-list');
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

        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/scorecardmetricsKBL/deleteupdatescorecardmetricsKBL', vm.scm.MetricID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-scorecardmetricsKBL-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-scorecardmetricsKBL-list');
        }

        var getBSOtherInfo = function () {
            vm.viewModelHelper.apiGet('api/scorecardmetricsKBL/maincaptionsfromotherinfo', null,
                function (result) {
                    vm.bsotherinformation = result.data;
                },
                function (result) {

                }, null);
        }

       

        //setupRules();
        initialize();


    }
}());
