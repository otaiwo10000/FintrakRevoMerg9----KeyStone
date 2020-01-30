/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LoanScheduleListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        LoanScheduleListController]);

    function LoanScheduleListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_Processed_Data';
        vm.view = 'loanschedule-list-view';
        vm.viewName = 'Loan Schedules';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.loanSchedules = [];

        vm.distinctRefNos = [];

        vm.RefNo = 'None';
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){

            if (vm.init === false) {

                intializeLookUp();
                //vm.viewModelHelper.apiGet('api/loanschedule/getloanschedule/' + vm.RefNo, null,
                //   function (result) {
                //       vm.loanSchedules = result.data;
                //       InitialView();
                //       //vm.init === true;
                       
                //   },
                // function (result) {
                //     toastr.error(result.data, 'Fintrak');
                // }, null);
                vm.init === true;
            }
        }

        var intializeLookUp = function () {
            getRefNos();
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                // data export
                if ($('#loanScheduleTable').length > 0) {
                    var exportTable = $('#loanScheduleTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" + "RC" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        },
                        "aoColumnDefs": [
                             //{ "bVisible": false, "aTargets": [0] }
                        ],
                        "colVis": {
                            buttonText: 'Show / Hide Columns',
                            restore: "Restore",
                            showAll: "Show all"
                        }
                    });
                }
            }, 50);
        }

        var getRefNos = function () {
            vm.viewModelHelper.apiGet('api/loanperiodicschedule/getrefnos', null,
                 function (result) {
                     vm.distinctRefNos = result.data;
                 },
                 function (result) {
                     toastr.error('Loan  Scedule.', 'Fintrak');
                 }, null);
        }
        vm.load = function () {
            var url = '';
            url = 'api/loanschedule/getloanschedule/' + vm.RefNo,

            vm.viewModelHelper.apiGet(url, null,
               function (result) {
                   vm.loanSchedules = result.data;

                   toastr.success('Data for the selected RefNo loaded.', 'Fintrak');
               },
               function (result) {
                   toastr.error('Fail to load data for the selected RefNo.', 'Fintrak');
               }, null);
        }

              vm.exportToExcel = function (tableId) { 
            $(tableId).tableExport({ type: 'excel', escape: 'false' });
        }

        initialize(); 
    }
}());
