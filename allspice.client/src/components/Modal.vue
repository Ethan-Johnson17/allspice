<template>
  <div class="modal" id="modal" tabindex="-1">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <!-- <button
          type="button"
          class="btn-close ms-auto mt-1"
          data-bs-dismiss="modal"
          aria-label="Close"
        ></button> -->
        <div class="modal-body">
          <slot name="modal-body" />
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>