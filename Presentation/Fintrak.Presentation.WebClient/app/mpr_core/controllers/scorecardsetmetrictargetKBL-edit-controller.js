/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ScoreCardSetMetricTargetKBLEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$location',
            ScoreCardSetMetricTargetKBLEditController]);

    function ScoreCardSetMetricTargetKBLEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'scorecardsetmetrictargetKBL-edit-view';
        vm.viewName = 'ScoreCard Set Metric Target';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mTarget = {};
        vm.scm = [];

        //vm.value1 = 'Checked'

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var mTargetRules = [];
           
        var setupRules = function () {

        mTargetRules.push(new validator.PropertyRule("SetMetric", {
                required: { message: "Metric is required" }
            }));

        mTargetRules.push(new validator.PropertyRule("SetTarget", {
            required: { message: "Target is required" }
            }));      
    }

   

        var initialize = function () {

            scmFunc();
            if (vm.init === false) {

                if ($stateParams.ID !== 0) {

                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/scorecardsetmetrictargetKBL/getscorecardsetmetrictargetKBL/' + $stateParams.ID, null,
                   function (result) {

                       vm.mTarget = result.data;                      

                       initialView();
                       vm.init === true;
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.mTarget = {
                        SetMetric: '', SetTarget: 0, FullYear: 0, Period: 0, Year: 0, ProrateYTD: false, Active: true                                             
                    };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {           

            ////Validate
            validator.ValidateModel(vm.mTarget, mTargetRules);
            vm.viewModelHelper.modelIsValid = vm.mTarget.isValid;
            vm.viewModelHelper.modelErrors = vm.mTarget.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/scorecardsetmetrictargetKBL/updatescorecardsetmetrictargetKBL', vm.mTarget,
               function (result) {

                   $state.go('mpr-scorecardsetmetrictargetKBL-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.mTarget.errors;

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
                vm.viewModelHelper.apiPost('api/scorecardsetmetrictargetKBL/deletescorecardsetmetrictargetKBL', vm.mTarget.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-scorecardsetmetrictargetKBL-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-scorecardsetmetrictargetKBL-list');
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
