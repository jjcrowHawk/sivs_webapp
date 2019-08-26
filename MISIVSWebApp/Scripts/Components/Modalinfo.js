const modalInfo = {
    props: {
        inspeccion_id: String,
        id: Number,
        address: String,
        score: Number,
        score_weight: Number,
    },
    data() {
        return {
            /*id: "0000",
            address: "N/A",
            score: 0,
            score_weight: 0.00,*/
        }
    },
    methods: {
        redirectToPage() {
            console.log("closing_modal");
            $('#componente_modal_info').remove();
            $('#modal-template').remove();
            /*if (this.url_redirect != "") {
                location.href = this.url_redirect;
            }
            else {
                this.$parent.showModal = false;
            }*/
        },
        getVulnerabilityIndex() {
            $.get("/Home/GetViviendaVulnerabilityScore/" + this.id, function (data, status) {
                console.log("Got RESPONSE!!!");
            })
        }

    },
    mounted() {
        this.getVulnerabilityIndex()
    },
    template: `
    <!-- template for the modal component -->
    <div id="modal-template">
        <transition name="modal">
            <div class="modal-mask">
            <div class="modal-wrapper">
                <div class="modal-container">
                <div class="modal-header" style="text-align:center;background-color:#307FE2">
                   <h3 slot="header" style="margin:auto; color:white; font-weight: bolder;">
                     Vulnerability Report
                    </h3>
                </div>
                <div class="modal-body">
                   <h4>Vulnerability Analysis</h4>
                    <hr />
                    <table>
                        <tr>
                            <td class="td_label">
                                ID:
                            </td>
                            <td class="">
                                {{this.id}}
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label">
                                Address:
                            </td>
                            <td>
                                {{this.address}}
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label">
                                Vulnerability score:
                            </td>
                            <td>
                                {{this.score}}
                            </td>
                            <td class="td_label">
                                Vulnerability percentage:
                            </td>
                            <td>
                                {{this.score_weight}}
                            </td>
                        </tr>
                    </table> 
                </div>
                <div class="modal-footer">
                    <button class="modal-default-button btn btn-primary btn-sm" @click="redirectToPage">
                        Close
                    </button>
                </div>
                </div>
            </div>
            </div>
        </transition>
    </div>
    `
}

/* Variable contenedora de la instancia del componente telefono*/
var contenedorModalInfo = new Vue({
    el: '#componente_modal_info',
    data: {
        showModal: false,
        fichasData: []
    },
    components: {
        'modal-info': modalInfo
    },
    methods: {
        showVulnerabilityInfo(index) {
            console.log("on parent: " + index);
        }
    }
})