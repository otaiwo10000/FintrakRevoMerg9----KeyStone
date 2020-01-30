/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SCMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SCMappingEditController]);

    function SCMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) { 
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'scorecardmapping-edit-view';
        vm.viewName = 'MPR ScoreCard Mapping';

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
                getMetrics();
                getsccaptions();

                if ($stateParams.MappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/scorecardmapping/getscorecardmapping/' + $stateParams.MappingId, null,
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
                        Metric_Code: '', Actual_Caption: '', Budget_Caption: '', Period: '', Year: '', Mapping_code: '', Active: true 
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

        var getsccaptions = function () {           
            vm.viewModelHelper.apiGet('api/scorecard/getscorecardcaptions', null,
                function (result) {
                    vm.sccaptions = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.save = function () {
            ////Validate
            validator.ValidateModel(vm.scm, scmRules);
            vm.viewModelHelper.modelIsValid = vm.scm.isValid;
            vm.viewModelHelper.modelErrors = vm.scm.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.scm.Budget_Caption = vm.scm.Actual_Caption;
               vm.viewModelHelper.apiPost('api/scorecardmapping/updatescorecardmapping', vm.scm,
               function (result) {

                   $state.go('mpr-scorecardmapping-list');
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
                vm.viewModelHelper.apiPost('api/scorecardmapping/deletescorecardmapping', vm.scm.MappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-scorecardmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-scorecardmapping-list');
        }


        //mprNumerator();
        //mprDenominator();
        //setupRules();
        initialize();


    }
}());
