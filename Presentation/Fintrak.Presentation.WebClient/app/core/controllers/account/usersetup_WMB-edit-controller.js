/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("UserSetupWMBEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        UserSetupWMBEditController]);

    function UserSetupWMBEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'usersetupWMB-edit-view';
        vm.viewName = 'User Setup';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.loginid = "";
        vm.userSetup = {};
        vm.userRoles = [];
        vm.userReportRoles = [];
        vm.userCompanies = [];
        vm.showRoles = true;
        vm.hideuserdetailTag = true;

        var date1 = new Date();
        date1.setFullYear(2011, 6, 1);

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var userSetupRules = [];

        var setupRules = function () {

            userSetupRules.push(new validator.PropertyRule("LoginID", {
                required: { message: "LoginID is required 1" }
            }));

            userSetupRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required 1" }
            }));
        };

        vm.getADUserDetail = function () {
            vm.hideuserdetailTag = false;

            vm.loginid_a = vm.loginid;
            vm.loginid_a = vm.loginid_a.replace(/\//g, 'FORWARDSLASHXTER'); // i.e replace the xter "/" with "FORWARDSLASHXTER"
            vm.loginid_a = vm.loginid_a.replace(/\./g, 'DOTXTER');   // i.e replace the xter "\" or "." with "FORWARDSLASHXTER"

            if (vm.init === false) {
                
                    vm.showPeriod = true;
                vm.viewModelHelper.apiGet('api/account/getactivedirectoryuserdetail/' + vm.loginid_a, null,
                        function (result) {
                            vm.userSetup = result.data;
                           
                            vm.init === true;
                            
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);             
            }
        };

        
     
        vm.save = function () {

                ////Validate
                validator.ValidateModel(vm.userSetup, userSetupRules);
                vm.viewModelHelper.modelIsValid = vm.userSetup.isValid;
                vm.viewModelHelper.modelErrors = vm.userSetup.errors;
                if (vm.viewModelHelper.modelIsValid) {

                    var userModel = { UserSetup: vm.userSetup, Roles: vm.userRoles, ReportRoles: vm.userReportRoles, UserCompanies: vm.userCompanies };

                    vm.viewModelHelper.apiPost('api/account/updateaccountdetail', userModel,
                        function (result) {

                            $state.go('core-usersetup-list');
                        },
                        function (result) {
                            toastr.error(result.data, 'Fintrak');
                        }, null);
                }           
        };

        
        vm.cancel = function () {
            $state.go('core-usersetup-list');
        };

       
        //setupRules();
        //initialize(); 
    }
}());
