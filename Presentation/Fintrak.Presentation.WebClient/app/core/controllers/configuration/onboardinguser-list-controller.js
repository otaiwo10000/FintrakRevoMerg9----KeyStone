/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OnBoardingUserListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        OnBoardingUserListController]);

    function OnBoardingUserListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'onboardinguser-list-view';
        vm.viewName = 'OnBoarding users';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.onboardingusers = [];

        //vm.accountMISs = [];
        vm.selectedId = '';
        $scope.selection = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/onboardinguser/availableonboardingusers', null,
                    function (result) {
                        //vm.onboardingusers = result.data;
                        vm.onboardingusers = result.data;
                        InitialView();
                        vm.init === true;
                        $state.go('core-onboardinguser-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        var InitialView = function () {
            InitialGrid();
        };

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#obuTable').length > 0) {
                    var exportTable = $('#obuTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        };


        // toggle selection for a given fruit by name
        $scope.toggleSelection = function toggleSelection(Id) {
            var idx = $scope.selection.indexOf(Id);


            // is currently selected
            if (idx > -1) {
                $scope.selection.splice(idx, 1);
                vm.selectedId = $scope.selection.join(', ');
                //  alert(vm.selectedId)
            }

            // is newly selected
            else {
                $scope.selection.push(Id);
                vm.selectedId = $scope.selection.join(', ');
                //  alert(vm.selectedId)
            }
        };

        
        
        //vm.onboardingusers_2 = [];
        //var onboardingusersFunc = function () {

        //    //angular.forEach(vm.onboardingusers, function (a, b) {
        //    //    var cb = document.getElementById("1");
        //    //    //if (cb.checked) {
        //    //    if (cb.isChecked) {
        //    //        vm.onboardingusers_2.push(a.Id);
        //    //        //vm.onboardingusers_2.push({ Id: a.Id, UserName: a.UserName });
        //    //        //document.getElementById("w20").disabled = true;
        //    //        //document.getElementById("w20").disabled = false;
        //    //    }
        //    //});


        //    vm.checkboxvalue = [];
        //    vm.checkboxvaluea = [];
        //    vm.result = $('input[type="checkbox"]:checked');   //put all the checkedboxes into var result
        //    vm.resultString = '';
        //    if (vm.result.length > 0) {                        // let you know if at least a checkbox is cheched
        //        vm.resultString = vm.result.length; //+ " checkboxes checked <br/>";   //gives the number of checkedboxes

        //        vm.result.each(function () {
                   
        //            vm.checkboxvaluea = $(this).val();
        //            vm.onboardingusers_2.push(vm.checkboxvaluea);
        //            //vm.onboardingusers_2 = vm.checkboxvalue;
        //            //checkboxvalue.push($(this).val().split(',,'));
        //            // checkboxvalue = $(this).val()
        //        });
        //    };


        //};

        ////vm.approveDeclineFunc = function () {

            
        ////};
       
        vm.approve = function () {
            //Validate
            //validator.ValidateModel(vm.abcRatio, abcratioRules);
            //vm.viewModelHelper.modelIsValid = vm.abcRatio.isValid;
            //vm.viewModelHelper.modelErrors = vm.abcRatio.errors;

            //onboardingusersFunc();

            vm.viewModelHelper.apiPost('api/onboardinguser/onboardingusersapprovalanddecline/' + vm.selectedId,
                function (result) {

                    //$state.go('core-onboardinguser-list');
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        //vm.templates = [];

        //vm.runExtractions = function () {

        //    var extractionIds = [];

        //    angular.forEach(vm.templates, function (extraction) {
        //        if (extraction.CanRun) {
        //            extractionIds.push(extraction.ExtractionId);
        //        }
        //    });
        //};



        initialize(); 
    }
}());
