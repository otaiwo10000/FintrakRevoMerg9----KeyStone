/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AuditTrailListController", 
                    ['$scope', '$state', '$filter', 'viewModelHelper', 'validator',
                        AuditTrailListController]);

    function AuditTrailListController($scope, $state,$filter, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'audittrail-list-view';
        vm.viewName = 'Audit Trail';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.auditTrails = [];

        vm.actionsPerformed = 0


        //vm.startDate = '08-27-2015' ;
        //vm.endDate = '08-28-2015';

        vm.startDate = new Date() ;
        vm.endDate = new Date();
 
 

        vm.actions = [
            { Id: 1, Name: 'Create' },
            { Id: 2, Name: 'Update' },
            { Id: 3, Name: 'Delete' },
            { Id: 4, Name: 'Extraction' }
        ];


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                //vm.viewModelHelper.apiGet('api/auditTrail/availableaudittrail', null,
                 vm.viewModelHelper.apiGet('api/auditTrail/getaudittrailbyaction/' + vm.actionsPerformed + '/' + vm.startDate.toDateString() + '/' + vm.endDate.toDateString(),null,
                   function (result) {
                       vm.auditTrails = result.data;
                       InitialView();
                       vm.init === true;
                      
                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if (vm.init)
                {
                    //var exportTable = $('#auditTrailTable')
                    //exportTable.clearInputs(true);
                    //exportTable = null;
                    //if ($('#auditTrailTable').length > 0) {
                    //    var exportTable = $('#auditTrailTable').DataTable({
                    //        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                    //        sDom: "T<'clearfix'>" +
                    //            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                    //            "t" +
                    //            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                    //        "tableTools": {
                    //            "sSwfPath": "../app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                    //        }
                    //    });
                    //}
                }
                else {
                    
                    if ($('#auditTrailTable').length > 0) {
                        var exportTable = $('#auditTrailTable').DataTable({
                            "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                            sDom: "T<'clearfix'>" +
                                "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                                "t" +
                                "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                            "tableTools": {
                                "sSwfPath": "../app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                            }
                        });
                    }
                }
            }, 50);
        }
        

        vm.filterByAction = function (initialized) {

            //var startDate = $filter('date')(vm.startDate, 'MM-dd-yyyy'); 
            //var endDate = $filter('date')(vm.endDate, 'MM-dd-yyyy');

            //vm.viewModelHelper.apiGet('api/auditTrail/getaudittrailbyaction/' + vm.actionsPerformed + '/' + vm.startDate + '/' + vm.endDate, null,
          
             vm.viewModelHelper.apiGet('api/auditTrail/getaudittrailbyaction/' + vm.actionsPerformed + '/' + vm.startDate.toDateString() + '/' + vm.endDate.toDateString(),null,
                  function (result) {

                      vm.auditTrails = result.data;

                      if (vm.init) {
                          InitialView();
                          vm.init === true;
                      }

                      
                  },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }
   

        initialize();

        vm.openStartDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedStartDate = true;
        }

        vm.openEndDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedEndDate = true;
        }
    }
}());
