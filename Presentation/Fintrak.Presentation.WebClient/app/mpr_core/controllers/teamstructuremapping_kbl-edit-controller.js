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

        vm.year = 2019;

       // vm.paramsObj = { Team_Code: '', Branch_Code: '', BranchName: '', Region_Code: '', RegionName: '', Division_Code: '', DivisionName: '', Year: 0 };
       // vm.paramsObj = { Team_Code: '', Branch_Code: '', BranchName: '', Year: vm.year };
        vm.paramsObj = {
            Team_Code: '', Branch_Code: '', BranchName: '',
            Branch_Code2: '', Region_Code: '', RegionName: '',
            Region_Code2: '', Division_Code: '', DivisionName: '',
            Year: vm.year
        };

       

        vm.tsBranch = [];
        vm.tsDivision = [];
        //vm.tsRegion = [];
        //vm.tsDirectorate = [];

        vm.yearList = [
        { value: 2018, name: '2018' },
        { value: 2019, name: '2019' },        
        { value: 2020, name: '2020' },
        { value: 2021, name: '2021' },
        { value: 2022, name: '2022' },
        { value: 2023, name: '2023' }];

       
       

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




        //vm.save = function () {
        //    ////Validate
        //    //validator.ValidateModel(vm.ts, tsRules);
        //    // vm.viewModelHelper.modelIsValid = vm.ts.isValid;
        //    // vm.viewModelHelper.modelErrors = vm.ts.errors;
        //    // if (vm.viewModelHelper.modelIsValid) {

        //    //if (vm.paramsObj.lowerlevelmiscode === '' || vm.paramsObj.higherlevelmiscode === '' || vm.paramsObj.year === '') {
        //    //    $window.alert('No box should be empty. Ensure you select every box');
        //    //}

        //    //else {
        //    vm.viewModelHelper.apiPost('api/teamstructurekbl/mappingofteambank', vm.paramsObj,
        //            function (result) {

        //                $state.go('mpr-teambank_kbl-list');
        //            },
        //            function (result) {
        //                toastr.error(result.data, 'Fintrak');
        //            }, null);              
        //   // }
        //};


        vm.save_brhdiv = function () {  
            if (vm.paramsObj.Team_Code === '' || vm.paramsObj.Branch_Code === '') {
                $window.alert('Branch should not be empty!!!');
            }
            else {
                vm.viewModelHelper.apiPost('api/teamstructurekbl/mappingofteambank', vm.paramsObj,
                    function (result) {

                        $state.go('mpr-teambank_kbl-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        }; 

        vm.save_divreg = function () {
            if (vm.paramsObj.Branch_Code2 === '' || vm.paramsObj.Region_Code === '') {
                $window.alert('Division should not be empty!!!');
            }
            else {
                vm.viewModelHelper.apiPost('api/teamstructurekbl/mappingofteamgroupbrhreg', vm.paramsObj,
                    function (result) {

                        $state.go('mpr-teambank_kbl-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.save_regdir = function () {
            if (vm.paramsObj.Region_Code2 === '' || vm.paramsObj.Division_Code === '') {
                $window.alert('Region should not be empty!!!');
            }
            else {
                vm.viewModelHelper.apiPost('api/teamstructurekbl/mappingofteamgroupregdiv', vm.paramsObj,
                    function (result) {

                        $state.go('mpr-teambank_kbl-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };



        var teamstructure = function () {
           
            vm.tsBranch = [];
            vm.tsDivision = [];
            vm.tsRegion = [];
            vm.tsDirectorate = [];
           
            vm.viewModelHelper.apiGet('api/teamstructurekbl/teamstructurebranch/' + vm.year, null,
                    function (result) {
                        vm.tsBranch = result.data;
                    },
                    function (result) {
                        toastr.error('Fail to load profit centers.', 'Fintrak');
                }, null);

            vm.viewModelHelper.apiGet('api/teamstructurekbl/teamstructuredivision/' + vm.year, null,
                function (result) {
                    vm.tsDivision = result.data;
                },
                function (result) {
                    toastr.error('Fail to load profit centers.', 'Fintrak');
                }, null);

            vm.viewModelHelper.apiGet('api/teamstructurekbl/teamstructureregion/' + vm.year, null,
                function (result) {
                    vm.tsRegion = result.data;
                },
                function (result) {
                    toastr.error('Fail to load profit centers.', 'Fintrak');
                }, null);

            vm.viewModelHelper.apiGet('api/teamstructurekbl/teamstructuredirectorate/' + vm.year, null,
                function (result) {
                    vm.tsDirectorate = result.data;
                },
                function (result) {
                    toastr.error('Fail to load profit centers.', 'Fintrak');
                }, null);           

        };


        vm.tsOnSelectTeamFunc = function (miscode, misname, levelcode) {
            vm.paramsObj.Team_Code = miscode;            
        };

        vm.tsOnSelectBranchFunc = function (miscode, misname, levelcode) {
            vm.paramsObj.Branch_Code = miscode;
            vm.paramsObj.BranchName = misname;
        };

        vm.tsOnSelectBranch2Func = function (miscode, misname, levelcode) {
            vm.paramsObj.Branch_Code2 = miscode;
            //vm.paramsObj.BranchName2 = misname;
        };

        vm.tsOnSelectRegionFunc = function (miscode, misname, levelcode) {
            vm.paramsObj.Region_Code = miscode;
            vm.paramsObj.RegionName = misname;
        };

        vm.tsOnSelectRegion2Func = function (miscode, misname, levelcode) {
            vm.paramsObj.Region_Code2 = miscode;
            //vm.paramsObj.RegionName2 = misname;
        };

        vm.tsOnSelectDivisionFunc = function (miscode, misname, levelcode) {
            vm.paramsObj.Division_Code = miscode;
            vm.paramsObj.DivisionName = misname;
        };

        
        //vm.tsHigherOnSelectFunc = function (miscode, misname, levelcode) {
        //    vm.paramsObj.higherlevelmiscode = miscode;
        //    vm.paramsObj.higherlevelmisname = misname;
        //    vm.paramsObj.higherlevelcode = levelcode;
        //};


        ////vm.teamDefList = [{ Code: 'ACCT', Name: 'AccountOfficer' },
        ////                { Code: 'TEM', Name: 'Branch' },
        ////                { Code: 'BRH', Name: 'Division' },
        ////                { Code: 'REG', Name: 'Region' },
        ////                { Code: 'DIV', Name: 'Directorate' }];

        //vm.teamDefList = [{ Code: 'BRH', Name: 'Branch' },
        //                { Code: 'DIV', Name: 'Division' },
        //                { Code: 'REG', Name: 'Region' }];
        //                //{ Code: 'DIR', Name: 'Directorate' }];


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


        teamstructure();
        //yearFunc();


    }
}());
