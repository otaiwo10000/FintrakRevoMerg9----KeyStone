/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamStructureKBLEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', '$rootScope', '$http', 'yearsService',
                        TeamStructureKBLEditController]);

    function TeamStructureKBLEditController($scope, $window, $state, $stateParams, viewModelHelper, validator, $rootScope, $http, yearsService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teamstructure_kbl-edit-view';
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

            tsRules.push(new validator.PropertyRule("Accountofficer_Code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Team_Code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Branch_Code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Group_Code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Region_Code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Division_Code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("DIRECTORATECODE", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("PPRCategory", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Year", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("staff_id", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("unit_code", {
                required: { message: "Account Officer Code is required" }
            }));

            tsRules.push(new validator.PropertyRule("Period", {
                required: { message: "Account Officer Code is required" }
            }));

            //tsRules.push(new validator.PropertyRule("Numerator", {
            //    required: { message: "Numerator is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("Denominator", {
            //    required: { message: "Denominator is required" }
            //}));

        };

        //var teamS = function () {
        //    vm.viewModelHelper.apiGet('api/teamstructure/GetAllData', null,
        //         function (result) {
        //             vm.teamstructure = result.data;
        //         },
        //         function (result) {
        //             toastr.error(result.data, 'Fintrak');
        //         }, null);
        //}

        //var mprDenominator = function () {
        //    vm.viewModelHelper.apiGet('api/ratiocaptionmapping/availableratioCaptionMappings', null,
        //         function (result) {
        //             vm.mprDenominators = result.data;
        //         },
        //         function (result) {
        //             toastr.error(result.data, 'Fintrak');
        //         }, null);
        //}



        var initialize = function () {
            if (vm.init === false) {

                yearFunc();
                

                if ($stateParams.teambankid !== 0 || $stateParams.teamgroupid !== 0) {                   

                    //vm.viewModelHelper.apiGet('api/usermis/getusermisdetail/' + $scope.usermisId + '/' + LevelId, null,
                    vm.viewModelHelper.apiGet('api/teamstructurekbl/getteamstructure/' + $stateParams.teambankid + '/' + $stateParams.teamgroupid, null,
                        function (result) {

                            vm.ts = result.data;
                          
                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }

                //else if ($stateParams.teambankid !== 0 || $stateParams.teamgroupid === 0) {

                //    //vm.viewModelHelper.apiGet('api/usermis/getusermisdetail/' + $scope.usermisId + '/' + LevelId, null,
                //    vm.viewModelHelper.apiGet('api/teamstructurekbl/getteamstructure/' + $stateParams.teambankid + '/' + $stateParams.teamgroupid, null,
                //        function (result) {

                //            vm.ts = result.data;

                //            initialView();
                //            vm.init === true;

                //        },
                //        function (result) {
                //            toastr.error(result.data, 'Fintrak');
                //        }, null);
                //}

                //else if ($stateParams.teambankid === 0 || $stateParams.teamgroupid !== 0) {

                //    //vm.viewModelHelper.apiGet('api/usermis/getusermisdetail/' + $scope.usermisId + '/' + LevelId, null,
                //    vm.viewModelHelper.apiGet('api/teamstructurekbl/getteamstructure/' + $stateParams.teambankid + '/' + $stateParams.teamgroupid, null,
                //        function (result) {

                //            vm.ts = result.data;

                //            initialView();
                //            vm.init === true;

                //        },
                //        function (result) {
                //            toastr.error(result.data, 'Fintrak');
                //        }, null);
                //}

                else

                    vm.sh = true;
                    vm.ts = {
                        AccountOfficer_Code: '', AccountOfficer_Name: '', Team_Code: '', TeamName: '',
                        Branch_Code: '', BranchName: '', Region_Code: '', RegionName: '',
                        Division_Code: '', DivisionName: '', Year: '', StaffID: '', Active: true
                    };                   
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

                vm.viewModelHelper.apiPost('api/teamstructurekbl/updateteamstructure', vm.ts,
                    function (result) {

                        $state.go('mpr-teamstructure_kbl-list');
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
                vm.viewModelHelper.apiPost('api/teamstructurekbl/deleteteamstructure/' + vm.ts.Year + '/' + vm.ts.TeamBankId + '/' + vm.ts.TeamGroupId, null,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-teamstructure_kbl-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.fillTheControlBoxes = function (brhcode, year) {
            if (vm.init === false) {

                if (vm.ts.Team_Code === '' || vm.ts.AccountOfficer_Code === '' || vm.ts.AccountOfficer_Name === '' || vm.ts.StaffID === '' || vm.ts.Year === '') {
                    $window.alert('AccountOfficer Name, AccountOfficer Code, StaffId, Year and BranchCode cannot be null');
                }
                else {
                    var typedTeamCode = vm.ts.Team_Code;
                    var typedAcctOffCode = vm.ts.AccountOfficer_Code;
                    var typedAcctOffName = vm.ts.AccountOfficer_Name;
                    var typedStaffID = vm.ts.StaffID;
                    var selectedYear = vm.ts.Year;

                    vm.viewModelHelper.apiGet('api/teamstructurekbl/getteamstructuretop1/' + brhcode + '/' + year, null,
                        function (result) {

                            vm.ts = result.data;

                            vm.ts.Team_Code = typedTeamCode;
                            vm.ts.AccountOfficer_Code = typedAcctOffCode;
                            vm.ts.AccountOfficer_Name = typedAcctOffName;
                            vm.ts.StaffID = typedStaffID;
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
            $state.go('mpr-teamstructure_kbl-list');
        };


        //mprNumerator();
        //mprDenominator();
        setupRules();
        initialize();


    }
}());
