/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeFintrakAccountsSegmentListController",
                    ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                        IncomeFintrakAccountsSegmentListController]);

    function IncomeFintrakAccountsSegmentListController($scope, $window, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'incomefintrakaccountssegment-list-view';
        vm.viewName = 'Accounts Segments';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifas = [];
        vm.customerid = "";
        vm.bank = "";

       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/incomefintrakaccountssegment/getincomefintrakaccountssegmentbycustomercodeandbank/' + vm.customerid2 + '/' + vm.bank2, null,
                    function (result) {
                        vm.ifas = result.data;
                        InitialView();
                        vm.init === true;

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
                if ($('#ifasTable').length > 0) {
                    var exportTable = $('#ifasTable').DataTable({
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


        vm.ifasLoad = function () {
            vm.customerid2 = vm.customerid;
            vm.customerid2 = vm.customerid2.replace(/\//g, 'FORWARDSLASHXTER'); // i.e replace the xter "/" with "FORWARDSLASHXTER"
            vm.customerid2 = vm.customerid2.replace(/\./g, 'DOTXTER');   // i.e replace the xter "\" or "." with "FORWARDSLASHXTER"

            vm.bank2 = vm.bank;
            vm.bank2 = vm.bank2.replace(/\//g, 'FORWARDSLASHXTER'); // i.e replace the xter "/" with "FORWARDSLASHXTER"
            vm.bank2 = vm.bank2.replace(/\./g, 'DOTXTER');   // i.e replace the xter "\" or "." with "FORWARDSLASHXTER"

            vm.viewModelHelper.apiGet('api/incomefintrakaccountssegment/getincomefintrakaccountssegmentbycustomercodeandbank/' + vm.customerid2 + '/' + vm.bank2, null,

                function (result) {
                    vm.ifas = result.data;
                    InitialView();
                    toastr.success('data loaded.', 'Fintrak');
                },
                function (result) {
                    toastr.error('Fail to load data.', 'Fintrak');
                }, null);
        };


        vm.status = null;
        vm.populateIncomeFintrakAccountsSegmentTable = function () {
           
            vm.viewModelHelper.apiGet('api/incomefintrakaccountssegment/populateincomefintrakaccountssegment', null,

                function (result) {
                    vm.status = result.data;
                    toastr.success('data loaded.', 'Fintrak');
                    if (vm.status==="success") {
                        alert("operation success");
                    };
                    
                },
                function (result) {
                    toastr.error('Fail to load data.', 'Fintrak');
                }, null);
        };


        //initialize();
    }
}());
