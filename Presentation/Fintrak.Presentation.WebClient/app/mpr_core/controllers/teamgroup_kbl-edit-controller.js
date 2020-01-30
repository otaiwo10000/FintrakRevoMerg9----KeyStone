/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamGroupKBLEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$rootScope', '$http', 'yearsService',
            TeamGroupKBLEditController]);

    function TeamGroupKBLEditController($scope, $window, $state, $stateParams, viewModelHelper, validator, $rootScope, $http, yearsService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teamgroup_kbl-edit-view';
        vm.viewName = 'MPR Team Structure';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.ts = {};
        vm.ts = {};
        vm.yearList = [];
        vm.sh = false;

        //vm.value1 = 'Checked'
        //vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var tsRules = [];

        var setupRules = function () {

           
        };


        var initialize = function () {
            if (vm.init === false) {

                yearFunc();
                

                if ($stateParams.teamgroupid !== 0) {                   

                    //vm.viewModelHelper.apiGet('api/usermis/getusermisdetail/' + $scope.usermisId + '/' + LevelId, null,
                    vm.viewModelHelper.apiGet('api/teamstructurekbl/getteamgroup/' + $stateParams.teamgroupid, null,
                        function (result) {

                            vm.ts = result.data;
                          
                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }

                else

                    vm.sh = true;
                vm.ts = {
                    Branch_Code: '', BranchName: '', Region_Code: '', RegionName: '',
                    Division_Code: '', DivisionName: '', Year: '', Active: true
                };   
                    //vm.ts = {
                    //    AccountOfficer_Code: '', AccountOfficer_Name: '', Team_Code: '', TeamName: '',
                    //    Branch_Code: '', BranchName: '', Region_Code: '', RegionName: '',
                    //    Division_Code: '', DivisionName: '', Year: '', StaffID: '', Active: true
                    //};                   
            }
        };


        var initialView = function () {

        };

        vm.save = function () {
            ////Validate
            validator.ValidateModel(vm.ts, tsRules);
            vm.viewModelHelper.modelIsValid = vm.ts.isValid;
            vm.viewModelHelper.modelErrors = vm.ts.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/teamstructurekbl/updateteamgroup', vm.ts,
                    function (result) {

                        $state.go('mpr-teamgroup_kbl-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.ts.errors;

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
                vm.viewModelHelper.apiPost('api/teamstructurekbl/deleteteamgroup/' + vm.ts.TeamGroupId, null,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-teamgroup_kbl-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.fillTheControlBoxes = function (regcode, year) {
            if (vm.init === false) {

                if (vm.ts.Region_Code === '' || vm.ts.Branch_Code === '' || vm.ts.Branch_Name === '' || vm.ts.Year === '') {
                    $window.alert('Division Name, Division Code, Region Code and Year cannot be null');
                }
                else {
                    var typedRegionCode = vm.ts.Region_Code;
                    var typedBranchCode = vm.ts.Branch_Code;
                    var typedBranchName = vm.ts.BranchName;
                    var selectedYear = vm.ts.Year;

                    vm.viewModelHelper.apiGet('api/teamstructurekbl/getteamgrouptop1/' + regcode + '/' + year, null,
                        function (result) {

                            vm.ts = result.data;

                            vm.ts.Region_Code = typedRegionCode;
                            vm.ts.Branch_Code = typedBranchCode;
                            vm.ts.BranchName = typedBranchName;
                            vm.ts.Year = selectedYear;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                }       
        };

        var yearFunc = function () {
            yearsService.yearsFunc()
                .then(function (data) {
                    vm.yearList = data;
                    //alert(vm.yearList);               
                }).catch(function (result) {
                    alert("Got error");
                });
        };

        

        vm.cancel = function () {
            $state.go('mpr-teamgroup_kbl-list');
        };


        //mprNumerator();
        //mprDenominator();
        setupRules();
        initialize();


    }
}());
