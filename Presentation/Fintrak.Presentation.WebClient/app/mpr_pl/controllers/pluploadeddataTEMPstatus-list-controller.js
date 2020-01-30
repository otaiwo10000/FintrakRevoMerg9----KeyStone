/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PLUploadedDataTEMPStatusListController",
            ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                PLUploadedDataTEMPStatusListController]);

    function PLUploadedDataTEMPStatusListController($scope, $window, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'mpr';
        vm.view = 'pluploadeddataTEMPstatus-list-view';
        vm.viewName = 'PL Uploaded Data status';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.onboardingusers = [];
        vm.awaitingstatus = [];
        vm.approvedstatus = [];
        vm.declinedstatus = [];

        vm.searchvalue1 = '';
        vm.searchvalue2 = '';
        vm.searchvalue3 = '';


        //vm.accountMISs = [];
        vm.selectedId = '';
        // $scope.selectionoverride = [];
        vm.selectionoverride = [];

        vm.init = false;
        vm.init2 = false;
        vm.init3 = false;
        vm.showInstruction = false;
        vm.instruction = '';

        //=================================== awaiting approval starts =========================================================================

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/pluploadedataTEMPstatus/pluploadeddataAWAITING', null,
                    function (result) {
                        //vm.onboardingusers = result.data;
                        vm.awaitingstatus = result.data;
                        InitialView();
                        vm.init === true;
                        //$state.go('core-onboardinguser-list');
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

        vm.obuLoad = function () {

            //vm.icsprb.length = 0;
            //this.exportTable.clear();
            vm.awaitingstatus = [];
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/pluploadedataTEMPstatus/pluploadeddatausingparamsAWAITING/' + vm.searchvalue1, null,
                    function (result) {

                        vm.awaitingstatus = result.data;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.refreshOBU = function () {
            vm.awaitingstatus = [];
            vm.viewModelHelper.apiGet('api/pluploadedataTEMPstatus/pluploadeddataAWAITING', null,
                function (result) {

                    vm.awaitingstatus = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };


        //=================================== awaiting approval ends =========================================================================

        //=================================== approved starts =========================================================================

        var initialize2 = function () {

            if (vm.init2 === false) {
                vm.viewModelHelper.apiGet('api/pluploadedataTEMPstatus/pluploadeddataAPPROVED', null,
                    function (result) {
                        //vm.onboardingusers = result.data;
                        vm.approvedstatus = result.data;
                        InitialView2();
                        vm.init2 === true;
                        //$state.go('core-onboardinguser-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        var InitialView2 = function () {
            InitialGrid2();
        };

        var InitialGrid2 = function () {
            setTimeout(function () {

                // data export
                if ($('#obu2Table').length > 0) {
                    var exportTable = $('#obu2Table').DataTable({
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

        vm.obuLoad2 = function () {

            //vm.icsprb.length = 0;
            //this.exportTable.clear();
            vm.approvedstatus = [];
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/pluploadedataTEMPstatus/pluploadeddatausingparamsAPPROVED/' + vm.searchvalue2, null,
                    function (result) {

                        vm.approvedstatus = result.data;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.refreshOBU2 = function () {
            vm.approvedstatus = [];
            vm.viewModelHelper.apiGet('api/pluploadedataTEMPstatus/pluploadeddataAPPROVED', null,
                function (result) {

                    vm.approvedstatus = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        //=================================== approved ends =========================================================================

        //=================================== declined starts =========================================================================

        var initialize3 = function () {

            if (vm.init3 === false) {
                vm.viewModelHelper.apiGet('api/pluploadedataTEMPstatus/pluploadeddataDECLINED', null,
                    function (result) {
                        //vm.onboardingusers = result.data;
                        vm.declinedstatus = result.data;
                        InitialView3();
                        vm.init3 === true;
                        //$state.go('core-onboardinguser-list');
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        var InitialView3 = function () {
            InitialGrid3();
        };

        var InitialGrid3 = function () {
            setTimeout(function () {

                // data export
                if ($('#obu3Table').length > 0) {
                    var exportTable = $('#obu3Table').DataTable({
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

        vm.obuLoad3 = function () {

            //vm.icsprb.length = 0;
            //this.exportTable.clear();
            vm.declinedstatus = [];
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/pluploadedataTEMPstatus/pluploadeddatausingparamsDECLINED/' + vm.searchvalue3, null,
                    function (result) {

                        vm.declinedstatus = result.data;
                    },
                    function (result) {
                        toastr.error(result.data, 'Fintrak');
                    }, null);
            }
        };

        vm.refreshOBU3 = function () {
            vm.declinedstatus = [];
            vm.viewModelHelper.apiGet('api/pluploadedataTEMPstatus/pluploadeddataDECLINED', null,
                function (result) {

                    vm.declinedstatus = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        //=================================== declined ends =========================================================================

        // toggle selection for a given fruit by name
        //$scope.toggleSelectionOverride = function toggleSelectionOverride(Id) {
        vm.toggleSelectionOverride = function toggleSelectionOverride(ID) {
            //var idx = $scope.selectionoverride.indexOf(Id);
            var idx = vm.selectionoverride.indexOf(ID);

            // is currently selected
            if (idx > -1) {
                //$scope.selectionoverride.splice(idx, 1);
                vm.selectionoverride.splice(idx, 1);
                //vm.selectedId = $scope.selectionoverride.join(', ');
                vm.selectedId = vm.selectionoverride.join(', ');
                //  alert(vm.selectedId)
            }

            // is newly selected
            else {
                //$scope.selectionoverride.push(Id);
                vm.selectionoverride.push(ID);
                //vm.selectedId = $scope.selectionoverride.join(', ');
                vm.selectedId = vm.selectionoverride.join(', ');
                //  alert(vm.selectedId)
            }
        };

        vm.toggleTextAccountMIS = "check all";
        vm.checkallboxes = false;

        var toggleTextAccountMISFunc = function () {
            if (vm.checkallboxes === false) {
                vm.toggleTextAccountMIS = "uncheck all";
            }
            else if (vm.checkallboxes === true) {
                vm.toggleTextAccountMIS = "check all";
            }
        };

        //$scope.AccountMISOverrideAllCheckBoxes = function () {
        vm.AccountMISOverrideAllCheckBoxes = function () {

            toggleTextAccountMISFunc();
            vm.checkallboxes = !vm.checkallboxes;
            if (vm.checkallboxes === true) {
                angular.forEach(vm.awaitingstatus, function (a, b) {
                    //vm.selectedId = $scope.selectionoverride.push(a.Id);
                    vm.selectedId = vm.selectionoverride.push(a.ID);
                    //vm.selectedId = $scope.selectionoverride.join(', ');
                    vm.selectedId = vm.selectionoverride.join(', ');
                });
            }

            else if (vm.checkallboxes === false) {
                //$scope.selectionoverride = [];
                vm.selectionoverride = [];
                vm.selectedId = '';
            }
        };


        vm.approve = function () {

            var approveFlag = $window.confirm('Click OK to approve the selected Row(s)');
            if (approveFlag) {
                //var url = '';
                //url = 'api/incomeaccountMISoverrideTEMPstatus/incomeaccountMISoverrideapproval/' + vm.selectedId,
                    //vm.viewModelHelper.apiPost(url, null,
                vm.viewModelHelper.apiPost('api/pluploadedataTEMPstatus/pluploadeddataapproval', vm.selectionoverride,
                        function (result) {
                            toastr.success('Selected item approved.', 'Fintrak');
                            //$scope.selectionoverride = [];
                           vm.selectedId = '';
							vm.selectionoverride = [];
                            toastr.success('Selected item approved.', 'Fintrak');
							window.location.reload();
                           // $state.go('mpr-incomeaccountMISoverrideTEMPstatus-list');
                            
                        },
                        function (result) {
                            toastr.error('Fail to approve item.', 'Fintrak');
                        }, null);
            }
        };

        vm.decline = function () {

            var declinedFlag = $window.confirm('Click OK to decline the selected Row(s)');
            if (declinedFlag) {
                //var url = '';
               // url = 'api/incomeaccountMISoverrideTEMPstatus/incomeaccountMISoverridedecline/' + vm.selectedId,
                    //vm.viewModelHelper.apiPost(url, null,
                vm.viewModelHelper.apiPost('api/pluploadedataTEMPstatus/pluploadeddatadecline', vm.selectionoverride,
                        function (result) {
                            toastr.success('Selected item decline.', 'Fintrak');
                            //$scope.selectionoverride = [];
                           vm.selectedId = '';
							vm.selectionoverride = [];
                            toastr.success('Selected item approved.', 'Fintrak');
							window.location.reload();
                           // $state.go('mpr-incomeaccountMISoverrideTEMPstatus-list');
                           
                        },
                        function (result) {
                            toastr.error('Fail to decline item.', 'Fintrak');
                        }, null);
            }

        };


        //vm.refreshPage = function () {
        //    vm.viewModelHelper.apiGet('api/incomeaccountMISoverrideTEMPstatus/incomeaccountMISoverrideAWAITING', null,
        //        function (result) {
        //            vm.awaitingstatus = result.data;
        //        },
        //        function (result) {
        //            toastr.error(result.data, 'Fintrak');
        //        }, null);
        //};


        //==========================================================================================================

        vm.tToggle = false;
        vm.toggleText = "collapse awaiting list";
        var toggleTextFunc = function () {
            if (vm.tToggle === true) {
                vm.toggleText = "show awaiting list";
            }
            else if (vm.tToggle === false) {
                vm.toggleText = "collapse awaiting list";
            }
        };

        vm.toggleReportPage = function () {
            vm.tToggle = !vm.tToggle;
            document.getElementById("awaiting").hidden = vm.tToggle;
            toggleTextFunc();
        };

        //--------------------------------------------------------------------------------------------------------------------------------- 

        vm.tToggle2 = false;
        vm.toggleText2 = "collapse approved list";
        var toggleTextFunc2 = function () {
            if (vm.tToggle2 === true) {
                vm.toggleText2 = "show approved list";
            }
            else if (vm.tToggle2 === false) {
                vm.toggleText2 = "collapse approved list";
            }
        };

        vm.toggleReportPage2 = function () {
            vm.tToggle2 = !vm.tToggle2;
            document.getElementById("approved").hidden = vm.tToggle2;
            toggleTextFunc2();
        };

        //--------------------------------------------------------------------------------------------------------------------------------- 

        vm.tToggle3 = false;
        vm.toggleText3 = "collapse declined list";
        var toggleTextFunc3 = function () {
            if (vm.tToggle3 === true) {
                vm.toggleText3 = "show declined list";
            }
            else if (vm.tToggle3 === false) {
                vm.toggleText3 = "collapse declined list";
            }
        };

        vm.toggleReportPage3 = function () {
            vm.tToggle3 = !vm.tToggle3;
            document.getElementById("declined").hidden = vm.tToggle3;
            toggleTextFunc3();
        };
        //==========================================================================================================


        vm.toggleReportPage();
        vm.toggleReportPage2();
        vm.toggleReportPage3();

        initialize();
        initialize2();
        initialize3();
    }
}());
