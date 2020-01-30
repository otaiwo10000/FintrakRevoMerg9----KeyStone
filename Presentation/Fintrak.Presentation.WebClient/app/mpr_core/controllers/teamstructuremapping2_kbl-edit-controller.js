/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamStructureMappingKBLEditController",
        ['$scope', '$window', '$state', 'validator', '$stateParams',   'viewModelHelper', '$rootScope', '$http', 'yearsService',
            TeamStructureMappingKBLEditController]);

    function TeamStructureMappingKBLEditController($scope, $window, $state, validator, $stateParams, viewModelHelper, $rootScope, $http, yearsService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teamstructuremapping_kbl-edit-view';
        vm.viewName = 'MPR: Team Structure Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];


        vm.paramsObj = { lowerlevelcode: '', lowerlevelmiscode: '', higherlevelcode: '', higherlevelmiscode: '', higherlevelmisname: '', year: 0 };
        vm.tsLower = [];
        vm.tsHigher = [];
        //vm.selectedDefinitionCode = null;
        //vm.yearList = [];

        vm.yearList = [{value: 2018, name: '2018'}, { value: 2019, name: '2019' },        
        { value: 2020, name: '2020' },
        { value: 2021, name: '2021' },
        { value: 2022, name: '2022' },
        { value: 2023, name: '2023' }];

        ////vm.lowerlevelmiscode_brh = "";
        ////vm.lowerlevelmiscode_div = "";
        ////vm.lowerlevelmiscode_reg = "";

        //vm.selectedlowerlevelmiscode = null;
        //vm.selectedlowerlevelcode = null;

        //vm.selectedhigherlevelmiscode = null;        
        //vm.selectedhigherlevelcode = null;

        //vm.selectedhigherlevelmisname = null;

        vm.year = 0;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var tsRules = [];
      
        var setupRules = function () {

            //tsRules.push(new validator.PropertyRule("Accountofficer_Code", {
            //    required: { message: "Account Officer Code is required" }
            //}));

            //tsRules.push(new validator.PropertyRule("Division_Code", {
            //    required: { message: "Account Officer Code is required" }
            //}));

        };




        vm.save = function () {
            ////Validate
            //validator.ValidateModel(vm.ts, tsRules);
            // vm.viewModelHelper.modelIsValid = vm.ts.isValid;
            // vm.viewModelHelper.modelErrors = vm.ts.errors;
            // if (vm.viewModelHelper.modelIsValid) {

            if (vm.paramsObj.lowerlevelmiscode === '' || vm.paramsObj.higherlevelmiscode === '' || vm.paramsObj.year === '') {
                $window.alert('No box should be empty. Ensure you select every box');
            }

            else {
                vm.viewModelHelper.apiPost('api/teamstructurekbl/mappingofteamstructure', vm.paramsObj,
                    function (result) {

                        $state.go('mpr-teambank_kbl-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
                ////}
                ////else {
                //vm.viewModelHelper.modelErrors = vm.ts.errors;

                //var errorList = '';

                //angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                //    errorList += error + '<br>';
                //});

                //toastr.error(errorList, 'Fintrak');
                ////}
            }
        };


        vm.teamstructurebydefcode = function (code) {

            vm.tsLower = [];
            vm.paramsObj.lowerlevelcode = code;
            vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingdefcodeKBL/' + code, null,
                function (result) {
                    vm.tsLower = result.data;
                },
                function (result) {
                    toastr.error('Fail to load profit centers.', 'Fintrak');
                }, null);

            if (code === "BRH") {
                vm.tsHigher = [];
                //vm.paramsObj.higherlevelmiscode = vm.lowerlevelmiscode_brh;
                vm.paramsObj.higherlevelcode = 'DIV';     //in order to make the DIV higherlevel  tag show           
                vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingdefcodeKBL/' + vm.paramsObj.higherlevelcode, null,
                    function (result) {
                        vm.tsHigher = result.data;
                    },
                    function (result) {
                        toastr.error('Fail to load profit centers.', 'Fintrak');
                    }, null);
            }
            else if (code === "DIV") {
                vm.tsHigher = [];
                //vm.paramsObj.higherlevelmiscode = vm.lowerlevelmiscode_div;
                vm.paramsObj.higherlevelcode = 'REG';       //in order to make the REG higherlevel  tag show  
                vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingdefcodeKBL/' + vm.paramsObj.higherlevelcode, null,
                    function (result) {
                        vm.tsHigher = result.data;
                    },
                    function (result) {
                        toastr.error('Fail to load profit centers.', 'Fintrak');
                    }, null);
            }
            else if (code === "REG") {
                vm.tsHigher = [];
                //vm.paramsObj.higherlevelmiscode = vm.lowerlevelmiscode_dir;
                vm.paramsObj.higherlevelcode = 'DIR';       //in order to make the REG higherlevel  tag show  
                vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingdefcodeKBL/' + vm.paramsObj.higherlevelcode, null,
                    function (result) {
                        vm.tsHigher = result.data;
                    },
                    function (result) {
                        toastr.error('Fail to load profit centers.', 'Fintrak');
                    }, null);
            }

        };

        //vm.tsLowerOnSelectFunc = function (miscode, misname, levelcode) {
        //    vm.paramsObj.lowerlevelmiscode = miscode;
        //    vm.paramsObj.lowerlevelcode = levelcode;
        //};

        vm.tsHigherOnSelectFunc = function (miscode, misname, levelcode) {
            vm.paramsObj.higherlevelmiscode = miscode;
            vm.paramsObj.higherlevelmisname = misname;
            vm.paramsObj.higherlevelcode = levelcode;
        };


        //vm.teamDefList = [{ Code: 'ACCT', Name: 'AccountOfficer' },
        //                { Code: 'TEM', Name: 'Branch' },
        //                { Code: 'BRH', Name: 'Division' },
        //                { Code: 'REG', Name: 'Region' },
        //                { Code: 'DIV', Name: 'Directorate' }];

        vm.teamDefList = [{ Code: 'BRH', Name: 'Branch' },
                        { Code: 'DIV', Name: 'Division' },
                        { Code: 'REG', Name: 'Region' }];
                        //{ Code: 'DIR', Name: 'Directorate' }];


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


       
        //yearFunc();


    }
}());
