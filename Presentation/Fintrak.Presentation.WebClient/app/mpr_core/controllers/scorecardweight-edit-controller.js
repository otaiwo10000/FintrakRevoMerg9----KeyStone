/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SCWEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SCWEditController]);

    function SCWEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) { 
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'scorecardweight-edit-view';
        vm.viewName = 'MPR ScoreCard Weight';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.scw = {};
        vm.metrics = [];

        //vm.value1 = 'Checked'
        //vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var scwRules = []; 

        var setupRules = function () {

            scwRules.push(new validator.PropertyRule("Metric_Code", {
                required: { message: "Metric Code is required" }
            }));

            scwRules.push(new validator.PropertyRule("Weight", {
                required: { message: "Weight is required" }
            }));

            scwRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

            scwRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));          
        }

        var initialize = function () {
            if (vm.init === false) {
                getMetrics();

                if ($stateParams.WeightId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/scorecardweight/getscorecardweight/' + $stateParams.WeightId, null,
                   function (result) {

                       vm.scw = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.scw = {
                        Metric_Code: '', Weight: '', Period: '', Year: '', Active: true 
                    };
            }
        }


        var initialView = function () {

        }

        var getMetrics = function () {
            vm.viewModelHelper.apiGet('api/scorecardmetrics/getallscorecardmetrics', null,
                function (result) {
                    vm.metrics = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.save = function () {
            ////Validate
            validator.ValidateModel(vm.scw, scwRules);
            vm.viewModelHelper.modelIsValid = vm.scw.isValid;
            vm.viewModelHelper.modelErrors = vm.scw.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/scorecardweight/updatescorecardweight', vm.scw,
               function (result) {

                   $state.go('mpr-scorecardweight-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.scw.errors;

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
                vm.viewModelHelper.apiPost('api/scorecardweight/deletescorecardweight', vm.scw.WeightId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-scorecardweight-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-scorecardweight-list');
        }


        //mprNumerator();
        //mprDenominator();
        setupRules();
        initialize();


    }
}());
