/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("UserMISEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        UserMISEditController]);

    function UserMISEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'usermis-edit-view';
        vm.viewName = 'User MIS Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.userMIS = {};
        vm.segment_New = {};
        vm.users = [];
        vm.profitCenterDefinitions = [];
        vm.costCenterDefinitions = [];

        vm.userClassifications = [];
        vm.Output_Model = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var usermisRules = [];
        $scope.newArray = [];
        //$scope.newArrayPCMIS = [];
        $scope.newArrayPCMIS = {};

        $scope.Classification = [];

        $scope.usermisId = 0;
        vm.status = true;

        vm.bnk = [];
        vm.ts = [];
        vm.acctText = '';

        $scope.segments = [
       {
           Name: 'All',
           Code: '0'
       },
        {
            Name: 'Division',
            Code: '1'
        },
         {
             Name: 'Sub-Division',
             Code: '2'
         },
          {
              Name: 'Group',
              Code: '3'
          },
           {
               Name: 'SBU',
               Code: '4'
           },
            {
                Name: 'Segment',
                Code: '5'
            }
        ];

        //vm.bnk = [
        //    {
        //        Name: 'BNK',
        //        Code: 'BNK'
        //    }          
        //];

        //$scope.Classification = [{ UserClassificationMapId: '', LoginID: '', ClassificationCode: '', ClassificationName: '', Level: '', ClassificationTypeCode: '', ClassificationTypeName: '' }];


        //public int UserClassificationMapId { get; set; }
        //public string LoginID { get; set; }
        //public string ClassificationCode { get; set; }
        //public string ClassificationName { get; set; }
        //public int Level { get; set; }
        //public string ClassificationTypeCode { get; set; }
        //public string ClassificationTypeName { get; set; }


        // $scope.modernBrowsers = [
        //{
        //    icon: '<img src="https://cdn1.iconfinder.com/data/icons/fatcow/32/opera.png" />',
        //    name: 'Opera',
        //    maker: 'Opera Software',
        //    ticked: true
        //},
        //{
        //    icon: '<img  src="https://cdn1.iconfinder.com/data/icons/fatcow/32/internet_explorer.png" />',
        //    name: 'Internet Explorer',
        //    maker: 'Microsoft',
        //    ticked: false
        //},
        //{
        //    icon: '<img  src="https://cdn1.iconfinder.com/data/icons/humano2/32x32/apps/firefox-icon.png" />',
        //    name: 'Firefox',
        //    maker: 'Mozilla Foundation',
        //    ticked: true
        //},
        //{
        //    icon: '<img  src="https://cdn1.iconfinder.com/data/icons/fatcow/32x32/safari_browser.png" />',
        //    name: 'Safari',
        //    maker: 'Apple',
        //    ticked: false
        //},
        //{
        //    icon: '<img  src="https://cdn1.iconfinder.com/data/icons/google_jfk_icons_by_carlosjj/32/chrome.png" />',
        //    name: 'Chrome',
        //    maker: 'Google',
        //    ticked: true
        //}
        // ];



        $scope.getSelectAllPCMIS = function (data) {

            var outpNew = "";

            angular.forEach(data, function (value, key) {
                outpNew += value.Team_Code + ",";
            });

            //vm.userClassifications.ClassificationCode = outp;
            vm.userMIS.ProfitCenterMisCode = outpNew;
            //alert(outpNew);
        };

//===================== code that goes to mis code column starts =================================================

       //// vm.userMIS.ProfitCenterDefinitionCode

        $scope.getSelectAllTEM = function (data) {
            var outpNew = "";
            var n = 0;
            angular.forEach(data, function (value, key) {
                n = n + 1;
                if (n < data.length) { outpNew += value.Team_Code + ","; }
                else { outpNew += value.Team_Code; }    
            });
            vm.userMIS.ProfitCenterMisCode = outpNew;
        };

        $scope.getSelectAllACCT = function (data) {
            var outpNew = "";
            var n = 0;
            angular.forEach(data, function (value, key) {
                n = n + 1;
                if (n < data.length) { outpNew += value.Accountofficer_Code + ",";}
                else { outpNew += value.Accountofficer_Code; } 
            });
            vm.userMIS.ProfitCenterMisCode = outpNew;
        };

        $scope.getSelectAllBRH = function (data) {
            var outpNew = "";
            var n = 0;
            angular.forEach(data, function (value, key) {
                n = n + 1;
                if (n < data.length) { outpNew += value.Branch_Code + ","; }
                else { outpNew += value.Branch_Code; }
            });
            vm.userMIS.ProfitCenterMisCode = outpNew;
        };

        $scope.getSelectAllDIV = function (data) {
            var outpNew = "";
            var n = 0;
            angular.forEach(data, function (value, key) {
                n = n + 1;
                if (n < data.length) { outpNew += value.Division_Code + ","; }
                else { outpNew += value.Division_Code; }
            });
            vm.userMIS.ProfitCenterMisCode = outpNew;
        };


        $scope.getSelectAllGRP = function (data) {
            var outpNew = "";
            var n = 0;
            angular.forEach(data, function (value, key) {
                n = n + 1;
                if (n < data.length) { outpNew += value.Group_Code + ","; }
                else { outpNew += value.Group_Code; }
            });
            vm.userMIS.ProfitCenterMisCode = outpNew;
        };

        $scope.getSelectAllZON = function (data) {
            var outpNew = "";
            var n = 0;
            angular.forEach(data, function (value, key) {
                n = n + 1;
                if (n < data.length) { outpNew += value.Zone_Code + ","; }
                else { outpNew += value.Zone_Code; }
            });
            vm.userMIS.ProfitCenterMisCode = outpNew;
        };

        $scope.getSelectAllREG = function (data) {
            var outpNew = "";
            var n = 0;
            angular.forEach(data, function (value, key) {
                n = n + 1;
                if (n < data.length) { outpNew += value.Region_Code + ","; }
                else { outpNew += value.Region_Code; }
            });
            vm.userMIS.ProfitCenterMisCode = outpNew;
        };

        $scope.getSelectAllDIR = function (data) {
            var outpNew = "";
            var n = 0;
            angular.forEach(data, function (value, key) {
                n = n + 1;
                if (n < data.length) { outpNew += value.DIRECTORATECODE + ","; }
                else { outpNew += value.DIRECTORATECODE; }
            });
            vm.userMIS.ProfitCenterMisCode = outpNew;
        };

//===================== code that goes to mis code column ends ===================================================

        $scope.getSelectAll = function (data) {

            var outp_ClassificationCode = "";
            var outp_ClassificationTypeCode = "";
            var outp_Level = "";

            angular.forEach(data, function (value, key) {
                outp_ClassificationCode += value.Code + ",";

                //if (_outp_ClassificationTypeCode != value.ClassificationTypeCode) {
                //    _outp_ClassificationTypeCode = value.ClassificationTypeCode;
                //    //outp_ClassificationTypeCode += value.ClassificationTypeCode + ",";
                //}

                outp_ClassificationTypeCode = value.ClassificationTypeCode;
                outp_Level = value.Level;

                //outp_Level += value.Level + ",";

            });


            if (vm.segment_New.Code === '0') {

                outp_ClassificationCode = 'All';
                outp_ClassificationTypeCode = 'SEG';
                outp_Level = 0;

            }


            //$scope.Classification.ClassificationCode = outp_ClassificationCode;
            //$scope.Classification.ClassificationTypeCode = outp_ClassificationTypeCode;
            //$scope.Classifications.ClassificationCode = outp_ClassificationCode;

            $scope.Classification.push({ UserClassificationMapId: 0, LoginID: 0, ClassificationCode: outp_ClassificationCode, ClassificationName: '', Level: outp_Level, ClassificationTypeCode: outp_ClassificationTypeCode, ClassificationTypeName: '' });

            //$scope.surveyFormData.questions.push({
            //    Questions: $scope.currentQuestion.Questions,
            //    QuestionOptionTypeId: $scope.currentQuestion.QuestionOptionTypeId, Required: $scope.currentQuestion.Required, QuestionOrder: (_count + 1), Options: $scope.currentQuestion.Options
            //});

            //alert(outp);

            //$scope.newArray = data;
            //alert($scope.newArray);

            //outp = "";
            //for (m in $scope.marks) {
            //    if ($scope.marks[m].marked)
            //        outp += $scope.marks[m].name + ", ";
            //}
            //if (outp.length == 0) {
            //    return "(none)";
            //} else {
            //    return outp.substr(0, outp.length - 2);
            //}

            //  var outp = "";

            //  angular.forEach(data, function (value, key) {
            //      outp += data[m].Code + ", ";
            //      //myAccess.push(value.name);
            //  });



            //var  outp = "";
            //  for (m in data) {
            //          outp += data[m].Code + ", ";
            //  }
            //  if (outp.length == 0) {
            //      return "(none)";
            //  } else {
            //      return outp.substr(0, outp.length - 2);
            //  }

            //  alert(outp);

        };


        var setupRules = function () {

            usermisRules.push(new validator.PropertyRule("LoginID", {
                required: { message: "LoginID is required" }
            }));
        };



        var initialize = function () {
            if (vm.init === false) {


                //load lookups
                intializeLookUp();


                if ($stateParams.usermisId !== 0) {

                    vm.acctText = 'A';
                    vm.showChildren = true;
                    $scope.usermisId = $stateParams.usermisId;
                    vm.viewModelHelper.apiGet('api/usermis/getusermisdetail/' + $stateParams.usermisId, null,
                        function (result) {
                            vm.userMIS = result.data.UserMIS;//
                            vm.userClassifications = result.data.Classifications;
                            vm.teamstructurebydefcode(vm.userMIS.ProfitCenterDefinitionCode);
                            vm.aofficer(vm.userMIS.ProfitCenterDefinitionCode);

                            //vm.onProfitCenterDefinitionChanged(vm.userMIS.ProfitCenterDefinitionCode);
                            //getProfitCenterTeams(vm.userMIS.ProfitCenterDefinitionCode);
                            getCostCenterTeams(vm.userMIS.CostCenterDefinitionCode);

                            initialView();
                            vm.init === true;

                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }
                else
                    vm.userMIS = { LoginID: '', Active: true };
                getStaffsMonthly();
            }
        };

        var intializeLookUp = function () {
            getUsers();
            getProfitCenterDefinitions();
            getCostCenterDefinitions();
            //getStaffs();
        };

        var initialView = function () {

        };


        vm.getSegment_New = function (Code) {

            var LevelId = Code;

            vm.viewModelHelper.apiGet('api/usermis/getusermisdetail/' + $scope.usermisId + '/' + LevelId, null,
                function (result) {
                    //vm.userMIS = result.data.UserMIS;
                    vm.userClassifications = result.data.Classifications;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        vm.save = function () {


            var read = $scope.newArray;

            //Validate
            validator.ValidateModel(vm.userMIS, usermisRules);
            vm.viewModelHelper.modelIsValid = vm.userMIS.isValid;
            vm.viewModelHelper.modelErrors = vm.userMIS.errors;


            if (vm.segment_New.Code === 0) {

                outp_ClassificationCode = 'All';
                outp_ClassificationTypeCode = 'SEG';
                outp_Level = 0;
                $scope.Classification.push({ UserClassificationMapId: 0, LoginID: 0, ClassificationCode: outp_ClassificationCode, ClassificationName: '', Level: outp_Level, ClassificationTypeCode: outp_ClassificationTypeCode, ClassificationTypeName: '' });

            }

            if (vm.viewModelHelper.modelIsValid) {

                var model = { UserMIS: vm.userMIS, Classifications: $scope.Classification };
                vm.viewModelHelper.apiPost('api/usermis/updateusermisdetail', model,
                    function (result) {

                        $state.go('mpr-usermis-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.userMIS.errors;

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
                vm.viewModelHelper.apiPost('api/usermis/deleteusermis', vm.userMIS.UserMISId,
                    function (result) {
                        toastr.success('Selected item deleted.', 'Fintrak');
                        $state.go('mpr-usermis-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.cancel = function () {
            //$state.go('mpr-usermis-list');
            $state.go('core-usersetup-list');
        };

        //vm.onProfitCenterDefinitionChanged = function (definition) {
        //    vm.useStaffId = false;
        //    vm.viewModelHelper.apiGet('api/teamdefinition/getteamDefinitionByCode/' + definition, null,
        //        function (result) {
        //            if (result.data.Code === 'ACCT') {
        //                vm.useStaffId = true;
        //                getStaffs();
        //            }
        //            else {
        //                getProfitCenterTeams(definition);
        //            }
        //        },
        //        function (result) {
        //            toastr.error('Fail to load profit center definitions.', 'Fintrak');
        //        }, null);

        //}

        vm.onProfitCenterDefinitionChanged = function (definition) {
            vm.useStaffId = false;
            vm.viewModelHelper.apiGet('api/teamdefinition/getteamDefinitionByCode/' + definition, null,
                function (result) {
                    //if (result.data.Code === 'ACCT') {
                    vm.useStaffId = true;
                    //getStaffs(vm.userMIS.ProfitCenterDefinitionCode);
                    getStaffsMonthly();
                    // }
                    //else {
                    //    getProfitCenterTeams(definition);
                    //}
                },
                function (result) {
                    toastr.error('Fail to load profit center definitions.', 'Fintrak');
                }, null);

        };

        var getProfitCenterDefinitions = function () {
            vm.viewModelHelper.apiGet('api/teamdefinition/availableteamdefinitions', null,
                function (result) {
                    vm.profitCenterDefinitions = result.data;
                },
                function (result) {
                    toastr.error('Fail to load profit center definitions.', 'Fintrak');
                }, null);
        };

        var getProfitCenterTeams = function (definition) {
            vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + definition, null,
                function (result) {
                    vm.profitCenters = result.data;
                },
                function (result) {
                    toastr.error('Fail to load profit centers.', 'Fintrak');
                }, null);
        };

        vm.onCostCenterDefinitionChanged = function (definition) {
            getCostCenterTeams(definition);
        };

        var getCostCenterDefinitions = function () {
            vm.viewModelHelper.apiGet('api/costcentredefinition/availablecostCentreDefinitions', null,
                function (result) {
                    vm.costCenterDefinitions = result.data;
                },
                function (result) {
                    toastr.error('Fail to load cost center definitions.', 'Fintrak');
                }, null);
        };

        var getCostCenterTeams = function (definition) {
            vm.viewModelHelper.apiGet('api/costcentre/getcostcentrebydefinition/' + definition, null,
                function (result) {
                    vm.costCenters = result.data;
                },
                function (result) {
                    toastr.error('Fail to load cost centers.', 'Fintrak');
                }, null);
        };

        var getUsers = function () {
            vm.viewModelHelper.apiGet('api/account/getallaccount', null,
                function (result) {
                    vm.users = result.data;
                },
                function (result) {
                    toastr.error('Fail to load users.', 'Fintrak');
                }, null);
        };

        //var getStaffs = function () {
        //    vm.viewModelHelper.apiGet('api/staffs/availablestaff', null,
        //         function (result) {
        //             vm.staffs = result.data;
        //         },
        //         function (result) {
        //             toastr.error(result.data, 'Fintrak');
        //         }, null);
        //}

                                            //var getStaffs = function () {
                                            //    vm.viewModelHelper.apiGet('api/team/availableteams', null,
                                            //        function (result) {
                                            //            vm.teams = result.data;     //vm.teams declared
                                            //        },
                                            //        function (result) {
                                            //            toastr.error(result.data, 'Fintrak');
                                            //        }, null);
                                            //}

        //var getStaffs = function (definition) {
        //    //vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + definition, null,
        //    vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingsetup', null,
        //        function (result) {
        //            vm.teams = result.data;
        //        },
        //        function (result) {
        //            toastr.error('Fail to load profit centers.', 'Fintrak');
        //        }, null);
        //}

        

        var getStaffs = function () {
            ////vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + definition, null,
            //vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingsetup', null,
            vm.viewModelHelper.apiGet('api/teamstructureALL/getteamstructureALLusingsetup', null,
                function (result) {
                    vm.teams = result.data;
                },
                function (result) {
                    toastr.error('Fail to load profit centers.', 'Fintrak');
                }, null);
        };

        var getStaffsMonthly = function () {
            ////vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + definition, null,
            //vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingsetupmonthly', null,
            vm.viewModelHelper.apiGet('api/teamstructureALL/getteamstructureALLusingsetupmonthly', null,
                function (result) {
                    vm.teams = result.data;
                },
                function (result) {
                    toastr.error('Fail to load profit centers.', 'Fintrak');
                }, null);
        };

        //getteamstructureusingdefcode(code)
        vm.teamstructurebydefcode = function (code) {

            if (code === "BNK" || code === "SA") {
                vm.userMIS.ProfitCenterMisCode = "BNK";
            }

            else {
                //vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingdefcodemonthly/' + code, null,
                vm.viewModelHelper.apiGet('api/teamstructureALL/getteamstructureALLusingdefcodemonthly/' + code, null,
                    
                    function (result) {
                        vm.ts = result.data;
                    },
                    function (result) {
                        toastr.error('Fail to load profit centers.', 'Fintrak');
                    }, null);
            }
        };
           
        //vm.aofficer = function () {
        vm.aofficer = function (code) {

            //vm.c = 'ACCT';
            ////vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingparamsANDsetup/' + vm.c + '/' + vm.acctText, null,
            //vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingparamsANDsetupmonthly/' + code + '/' + vm.acctText, null,
             vm.viewModelHelper.apiGet('api/teamstructureALL/getteamstructureALLusingparamsANDsetupmonthly/' + code + '/' + vm.acctText, null,
                function (result) {
                    vm.ts = result.data;
                },
                function (result) {
                    toastr.error('Fail to load profit centers.', 'Fintrak');
                }, null);
        };

        //var getStaffs_2 = function () {
        //    //vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + definition, null,
        //    vm.viewModelHelper.apiGet('api/teamstructure/getteamstructureusingsetup', null,
        //        function (result) {
        //            vm.teams = result.data;
        //            teamStructureOnEdit(vm.teams);
        //        },
        //        function (result) {
        //            toastr.error('Fail to load profit centers.', 'Fintrak');
        //        }, null);
        //}


        //var teamStructureOnEdit = function (data) {

        //    var tcode;
        //    var tname;
        //    var ticked;
        //    angular.forEach(data, function (value, key) {

        //        if (value.Team_Code === vm.userMIS.ProfitCenterMisCode) {
        //            tcode = value.Team_Code;
        //            tname = value.teamName;
        //            ticked = true;
        //        }
        //        else {
        //            tcode = value.Team_Code;
        //            tname = value.teamName;
        //            ticked = false;
        //        }
                
        //        //$scope.tsDropdownOptions.push({ tcode: tcode, tcode: tname });
        //        vm.team.push({ tcode: tcode, tcode: tname, ticked: ticked});
        //    });

        //};


        setupRules();
        initialize();
    }
}());
