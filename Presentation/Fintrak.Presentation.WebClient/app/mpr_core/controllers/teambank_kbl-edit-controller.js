/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamBankKBLEditController",
        ['$scope', '$window', '$state', 'validator', '$stateParams',   'viewModelHelper', '$rootScope', '$http', 'yearsService',
            TeamBankKBLEditController]);

    function TeamBankKBLEditController($scope, $window, $state, validator, $stateParams,  viewModelHelper, $rootScope, $http, yearsService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teambank_kbl-edit-view';
        vm.viewName = 'MPR: AccountOfficer';

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

                if ($stateParams.teambankid !== 0) {                   

                    //vm.viewModelHelper.apiGet('api/usermis/getusermisdetail/' + $scope.usermisId + '/' + LevelId, null,
                    vm.viewModelHelper.apiGet('api/teamstructurekbl/getteambank/' + $stateParams.teambankid, null,
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
                    AccountOfficer_Code: '', AccountOfficer_Name: '', Team_Code: '', TeamName: '',
                    Branch_Code: '', Year: 0, StaffID: '', Active: true
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

                vm.viewModelHelper.apiPost('api/teamstructurekbl/updateteambank', vm.ts,
                    function (result) {

                        $state.go('mpr-teambank_kbl-list');
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
                vm.viewModelHelper.apiPost('api/teamstructurekbl/deleteteambankid/' + vm.ts.TeamBankId, null,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-teambank_kbl-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.fillTheControlBoxes = function (brhcode, year) {
            //if (vm.init === false) {

                if (vm.ts.Team_Code === '' || vm.ts.AccountOfficer_Code === '' || vm.ts.AccountOfficer_Name === '' || vm.ts.StaffID === '' || vm.ts.Year === 0) {
                    alert('AccountOfficer Name, AccountOfficer Code, StaffId, Year and BranchCode cannot be null');
                }
                else {
                    var typedTeamCode = vm.ts.Team_Code;
                    var typedAcctOffCode = vm.ts.AccountOfficer_Code;
                    var typedAcctOffName = vm.ts.AccountOfficer_Name;
                    var typedStaffID = vm.ts.StaffID;
                    var selectedYear = vm.ts.Year;

                    vm.viewModelHelper.apiGet('api/teamstructurekbl/getteambanktop1/' + brhcode + '/' + year, null,
                        function (result) {

                            vm.ts = result.data;

                            vm.ts.Team_Code = typedTeamCode;
                            vm.ts.AccountOfficer_Code = typedAcctOffCode;
                            vm.ts.AccountOfficer_Name = typedAcctOffName;
                            vm.ts.StaffID = typedStaffID;
                            vm.ts.Year = selectedYear;

                            ////initialView();
                            ////vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                //}       
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
            $state.go('mpr-teambank_kbl-list');
        };


       
        setupRules();
        initialize();


    }
}());
