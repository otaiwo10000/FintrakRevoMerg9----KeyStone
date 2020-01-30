/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TSEditController",
        ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator', 'yearsService',
                        TSEditController]);

    function TSEditController($scope, $window, $state, $stateParams, viewModelHelper, validator, yearsService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teamstructureALL-edit-view';
        vm.viewName = 'MPR Team Structure1';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ts = {};


        //vm.ts = {
        //    Accountofficer_Code: null, AccountofficerName: null, Team_Code: null, TeamName: null,
        //    Branch_Code: null, BranchName: null, Group_Code: null, GroupName: null, Zone_Code: null, ZoneName: null,
        //    Region_Code: null, RegionName: null, Division_Code: null, DivisionName: null,
        //    DIRECTORATECODE: null, DIRECTORATENAME: null, PPRCategory: null, Year: null,
        //    staff_id: null, unit_code: null, unitname: null, Period: 0,
        //    Active: true
        //};


         vm.yearList = [];
        vm.sh = false;

        //vm.value1 = 'Checked'
        //vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        //vm.yearList = [
        //    { value: "2025", name: "2025" },
        //    { value: "2024", name: "2024" },
        //    { value: "2023", name: "2023" },
        //    { value: "2022", name: "2022" },
        //    { value: "2021", name: "2021" },
        //    { value: "2020", name: "2020" },
        //    { value: "2019", name: "2019" },
        //    { value: "2018", name: "2018" },
        //    { value: "2017", name: "2017" },
        //    { value: "2016", name: "2016" },
        //    { value: "2015", name: "2015" },
        //    { value: "2014", name: "2014" }
        //];

        vm.periodList = [
            { id: 1, name: "January" },
            { id: 2, name: "February" },
            { id: 3, name: "March" },
            { id: 4, name: "April" },
            { id: 5, name: "May" },
            { id: 6, name: "June" },
            { id: 7, name: "July" },
            { id: 8, name: "August" },
            { id: 9, name: "September" },
            { id: 10, name: "October" },
            { id: 11, name: "November" },
            { id: 12, name: "December" }
        ];


        var tsRules = [];

        var setupRules = function () {

            //tsRules.push(new validator.PropertyRule("Accountofficer_Code", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("Team_Code", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("Branch_Code", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("Group_Code", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("Region_Code", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("Division_Code", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("DIRECTORATECODE", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("PPRCategory", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("staff_id", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("unit_code", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("Period", {
            //    required: { message: "Account Officer Code is required" }
            //}));

        };



        var initialize = function () {
            if (vm.init === false) {

                yearFunc();

                if ($stateParams.Team_StructureId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/teamstructureALL/getteamstructureALL/' + $stateParams.Team_StructureId, null,
                        function (result) {

                            vm.ts = result.data;

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else {
                    vm.sh = true;
                    vm.ts = {
                        Accountofficer_Code: null, AccountofficerName: null, Team_Code: null, TeamName: null,
                        Branch_Code: null, BranchName: null, Group_Code: null, GroupName: null, Zone_Code: null, ZoneName: null,
                        Region_Code: null, RegionName: null, Division_Code: null, DivisionName: null,
                        DIRECTORATECODE: null, DIRECTORATENAME: null, unit_code: null, unitname: null,
                        Segment_Code: null, SegmentName: null, SuperSegment_Code: null, SuperSegmentName: null,
                        PPRCategory: null, staff_id: null, Year: null, Period: 0,                         
                        Active: true
                    };
                }
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

                vm.viewModelHelper.apiPost('api/teamstructureALL/updateteamstructureALL', vm.ts,
                    function (result) {

                        $state.go('mpr-teamstructureALL-list');
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
                vm.viewModelHelper.apiPost('api/teamstructureALL/deleteteamstructureALL', vm.ts.Team_StructureId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-teamstructureALL-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };


        //vm.fillTheControlBoxes = function (brhcode, defcode, year) {
        //    if (vm.init === false) {

        //        var typedCode = null;
        //        var typedAcctOffCode = vm.ts.Accountofficer_Code;
        //        var typedAcctOffName = vm.ts.AccountofficerName;
        //        var typedStaffID = vm.ts.StaffID;
        //        var selectedYear = vm.ts.Year;

        //        //if (vm.ts.Team_Code === '' || vm.ts.Accountofficer_Code === '' || vm.ts.AccountofficerName === '' || vm.ts.StaffID === '' || vm.ts.Year === '') {
        //        //    $window.alert('AccountOfficer Name, AccountOfficer Code, StaffId, Year and BranchCode cannot be null');
        //        //}
        //        //else {
        //        if (defcode === 'TEM') { typedCode = vm.ts.Team_Code; } else if (defcode === 'BRH') { typedCode = vm.ts.Branch_Code; } else if (defcode === 'ZON') { typedCode = vm.ts.Zone_Code; }
        //            //var typedTeamCode = vm.ts.Team_Code;
        //            //var typedCode = vm.ts.Team_Code;
        //            //var typedAcctOffCode = vm.ts.Accountofficer_Code;
        //            //var typedAcctOffName = vm.ts.AccountofficerName;
        //            //var typedStaffID = vm.ts.StaffID;
        //            //var selectedYear = vm.ts.Year;

        //            vm.viewModelHelper.apiGet('api/teamstructure/getteamstructuretop1/' + brhcode + '/' + defcode + '/' + year, null,
        //                function (result) {

        //                    vm.ts = result.data;

        //                    //vm.ts.Team_Code = typedTeamCode;
        //                    vm.ts.Accountofficer_Code = typedAcctOffCode;
        //                    vm.ts.AccountofficerName = typedAcctOffName;
        //                    vm.ts.StaffID = typedStaffID;
        //                    vm.ts.Year = selectedYear;
        //                    if (defcode === 'TEM') { vm.ts.Team_Code = typedCode; } else if (defcode === 'BRH') { vm.ts.Branch_Code = typedCode; } else if (defcode === 'ZON') { vm.ts.Zone_Code = typedCode; }

        //                    initialView();
        //                    vm.init === true;

        //                },
        //                function (result) {
        //                    toastr.error(result.data, 'Fintrak');
        //                }, null);
        //        //}
        //    }
        //};

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
            $state.go('mpr-teamstructureALL-list');
        };


        //mprNumerator();
        //mprDenominator();
        setupRules();
        initialize();


    }
}());
