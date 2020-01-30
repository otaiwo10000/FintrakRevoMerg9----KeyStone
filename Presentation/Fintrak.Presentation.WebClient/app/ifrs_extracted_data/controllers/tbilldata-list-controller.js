/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IFRSTbillListController",
                    ['$scope', '$state', 'viewModelHelper', 'validator',
                        IFRSTbillListController]);

    function IFRSTbillListController($scope,$state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_Tbill_Data';
        vm.view = 'tbilldata-list-view';
        vm.viewName = 'IFRS Treasury Bills Data';
       
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.classification = '';
        vm.MaturityDate = 'None';
        

           vm.classifications = [
          { Id: 1, Name: 'HTM' },
          { Id: 2, Name: 'HFT' },
          { Id: 3, Name: 'AFS' }
        ];
        
        vm.ifrsTbills = [];
        vm.distinctMaturityDate = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function(){
            //load lookups
            intializeLookUp();
            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/ifrstbill/availableifrstbill', null,
                   function (result) {
                       vm.ifrsTbills = result.data;
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
                if ($('#ifrsTbillTable').length > 0) {
                    var exportTable = $('#ifrsTbillTable').DataTable({
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

          vm.getClassification = function () {
              vm.viewModelHelper.apiGet('api/ifrstbill/getifrstbill/' + vm.classification, null,
                        function (result) {
                            vm.ifrsTbills = result.data;
                                          },
                       function (result) {
                           toastr.error(result, 'Fintrak Error');
                       }, null);
          }

          vm.getTbillbyMatDate = function () {
              vm.viewModelHelper.apiGet('api/ifrstbill/gettbillbymatdate/' + vm.MaturityDate.substr(0, 10), null,
                        function (result) {
                            vm.ifrsTbills = result.data;
                        },
                       function (result) {
                           toastr.error(result, 'Fintrak Error');
                       }, null);
          }
          var intializeLookUp = function () {
              distinctMaturityDate();
            
          }

          var distinctMaturityDate = function () {
              vm.viewModelHelper.apiGet('api/ifrstbill/getMaturityDate', null,
                   function (result) {
                       vm.distinctMaturityDate = result.data;
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
          }
          vm.updateTbillsbyMatDate = function () {
              var params = { Date: vm.MaturityDate, Amount: vm.ifrsTbill.CurrentMarketYield };
              vm.viewModelHelper.apiPost('api/ifrstbill/updatetbillbymatdate', params,
                        function (result) {
                            $state.go('ifrs-tbills-list');
                            vm.getUpdatedMarketYieldData();
                        },
                       function (result) {
                           toastr.error(result.data, 'Fintrak Error');
                       }, null);
          }
        
          vm.getUpdatedMarketYieldData = function () {
              vm.viewModelHelper.apiGet('api/ifrstbill/availableifrstbill', null,
                  function (result) {
                      vm.ifrsTbills = result.data;
                  },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
          }

          initialize();
    }
}());
