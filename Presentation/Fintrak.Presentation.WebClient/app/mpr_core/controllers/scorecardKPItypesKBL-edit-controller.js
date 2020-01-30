/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ScoreCardKPITypesKBLEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
            ScoreCardKPITypesKBLEditController]);

    function ScoreCardKPITypesKBLEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'scorecardKPItypesKBL-edit-view';
        vm.viewName = 'ScoreCard KPI types';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.kpi = {};
        vm.scm = [];

        //vm.value1 = 'Checked'
        //vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var kpiRules = [];
      
        
        var setupRules = function () {

            kpiRules.push(new validator.PropertyRule("KPI_TYPE", {
                required: { message: "KPI TYPE Name is required" }
            }));

            kpiRules.push(new validator.PropertyRule("PERSPECTIVE", {
                required: { message: "PERSPECTIVE is required" }
            }));

            kpiRules.push(new validator.PropertyRule("KPI_METRIC", {
                required: { message: "KPI METRIC is required" }
            }));

            //scmRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));          
        }

        var initialize = function () {

            scmFunc();

            if (vm.init === false) {

                if ($stateParams.ID !== 0) {

                    //vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/scorecardKPItypesKBL/getscorecardKPItypesKBL/' + $stateParams.ID, null,
                   function (result) {

                       vm.kpi = result.data;                      

                       initialView();
                       vm.init === true;
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.kpi = {
                        KPI_TYPE: '', PERSPECTIVE: '', KPI_METRIC: '', KPI_WEIGHT: 0, Period: 0, Year: 0, Active: true                                             
                    };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {           

            ////Validate
            validator.ValidateModel(vm.kpi, kpiRules);
            vm.viewModelHelper.modelIsValid = vm.kpi.isValid;
            vm.viewModelHelper.modelErrors = vm.kpi.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/scorecardKPItypesKBL/updatescorecardKPItypesKBL', vm.kpi,
               function (result) {

                   $state.go('mpr-scorecardKPItypesKBL-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.kpi.errors;

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
                vm.viewModelHelper.apiPost('api/scorecardKPItypesKBL/deletescorecardKPItypesKBL', vm.kpi.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-scorecardKPItypesKBL-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-scorecardKPItypesKBL-list');
        }

        var scmFunc = function () {
            vm.viewModelHelper.apiGet('api/scorecardmetricsKBL/getallscorecardmetricsKBL', null,
                function (result) {
                    vm.scm = result.data;
                },
                function (result) {

                }, null);
        }

       

        setupRules();
        initialize();


    }
}());
