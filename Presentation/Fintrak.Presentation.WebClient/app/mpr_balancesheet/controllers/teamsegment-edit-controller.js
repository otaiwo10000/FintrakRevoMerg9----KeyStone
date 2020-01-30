/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamSegmentEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        TeamSegmentEditController]);

    function TeamSegmentEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teamsegment-edit-view';
        vm.viewName = 'Team Segment';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teamsegment = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var teamsegmentRules = [];
    
       
        var setupRules = function () {   

            //ctppRules.push(new validator.PropertyRule("DefinitionCode", {
            //    required: { message: "Definition Code is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("MisCode", {
            //    required: { message: "MisCode is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("Currency", {
            //    required: { message: "Currency is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("Rate", {
            //    required: { message: "Rate is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("Period", {
            //    required: { message: "Period is required" }
            //}));

            //ctppRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));          
        }

        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.Mpr_Team_Segment_ID !== 0) {
                    
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/teamsegment/getteamsegment/' + $stateParams.Mpr_Team_Segment_ID, null,
                   function (result) {

                       vm.teamsegment = result.data;
                      
                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    
                    vm.teamsegment = {
                        TargetCode: '', TargetSegment: '', CustomerTypeCode: '', CustomerType: '',
                        CustomerSegmentCode: '', CustomerSegment: '', Active: true                                              
                    };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {
            ////Validate
            validator.ValidateModel(vm.teamsegment, teamsegmentRules);
            vm.viewModelHelper.modelIsValid = vm.teamsegment.isValid;
            vm.viewModelHelper.modelErrors = vm.teamsegment.errors;
           if (vm.viewModelHelper.modelIsValid) {

               vm.viewModelHelper.apiPost('api/teamsegment/updateteamsegment', vm.teamsegment,
               function (result) {

                   $state.go('mpr-teamsegment-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
          }
           else {
               vm.viewModelHelper.modelErrors = vm.teamsegment.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            //var deleteFlag = $window.confirm(' Are you sure you want to delete');
            //if (confirm("Click OK to delete item ID " + appid + "  " + "whose name is:" + "  " + fname + "  " + lname + "?"))     //this is javascript function.
            if (confirm('Are you sure you want to delete the selected item?'))     //this is javascript function.
            {
                //if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/teamsegment/deleteteamsegment', vm.teamsegment.Mpr_Team_Segment_ID,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-teamsegment-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
                //}  //end deleteFlag
            }
        }

        vm.cancel = function () {
            $state.go('mpr-teamsegment-list');
        }


        setupRules();
        initialize();

    }
}());
