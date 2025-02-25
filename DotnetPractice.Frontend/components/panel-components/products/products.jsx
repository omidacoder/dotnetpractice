/* eslint-disable prettier/prettier */
import React, { useRef } from "react";
import { useState } from "react";
import {
  Input,
  Modal,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Button,
  useDisclosure,
  Textarea,
  Divider,
} from "@heroui/react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

import ProductsTable from "./products-table";

import ProductsEditModal from "@/components/panel-components/modals/materials-edit-modal";
import { CreateProduct } from "@/requests/mutations/product/CreateProduct";
export default function Products() {
  const queryClient = useQueryClient();
  const [ProductsEditVisible, setProductsEditVisible] = useState(false);
  const [photo, setPhoto] = useState(null);
  const [loading, setLoading] = useState(false);
  const { isOpen, onOpen, onOpenChange, onClose } = useDisclosure();
  const [editingProduct, setEdittingProduct] = useState(null);
  const name = useRef();
  const price = useRef();
  const availableCount = useRef();
  const description = useRef();
  const mutation = useMutation({
    mutationFn: CreateProduct,
    onSuccess: (_response) => {
      setLoading(false);
      toast("Created Successfully", {
        type: "success",
        position: "bottom-center",
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        autoClose: 3000,
      });
      onClose();
      queryClient.invalidateQueries({ queryKey: ["products"] });
    },
    onError: (_error) => {
      
      setLoading(false);
      toast("Something Went Wrong!", {
        type: "error",
        position: "bottom-center",
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        autoClose: 3000,
      });
    },
  });
  const create = () => {
    setLoading(true);
    mutation.mutate({
      name: name.current.value,
      description: description.current.value,
      price: price.current.value,
      availableCount: availableCount.current.value,
      photo,
    });
  };

  return (
    <>
      <div className="w-full gap-2 mt-5">
        <span className="primary_color">Products</span>
        <div className="border mt-2" />
      </div>
      <div className="w-full gap-2 mt-5">
        <div className="flex justify-center items-center">
          <Button
          color="primary"
            variant="flat"
            onPress={onOpen}
          >
            Create Product
          </Button>
        </div>
        <Modal
          hideCloseButton
          isOpen={isOpen}
          placement="top"
          scrollBehavior="outside"
          size="5xl"
          onOpenChange={onOpenChange}
        >
          <ModalContent>
            {(onClose) => (
              <>
                <ModalHeader className="flex flex-col gap-1">
                  Define New Product
                </ModalHeader>
                <Divider />
                <ModalBody>
                  <div className="w-full grid grid-cols-12 gap-2 mt-5 justify-center py-2 items-center">
                    <div className="col-span-12 md:col-span-4 mb-1">
                      <Input
                        ref={name}
                        isRequired
                        required
                        label="Product Name"
                        type="text"
                      />
                    </div>
                    <div className="col-span-12 md:col-span-4 mb-1">
                      <Input
                        ref={price}
                        isRequired
                        required
                        label="Price (USD)"
                        type="number"
                      />
                    </div>
                    <div className="col-span-12 md:col-span-4 mb-1">
                      <Input
                        ref={availableCount}
                        isRequired
                        required
                        label="Number of available"
                        type="number"
                      />
                    </div>
                    <div className="col-span-12 md:col-span-4">
                      <div className="mb-1">
                        <Input
                          label={"Picture"}
                          size="lg"
                          type="file"
                          onChange={(e) => {
                            setPhoto(e.target.files[0]);
                          }}
                        />
                      </div>
                    </div>
                  </div>
                  <div className="w-full grid grid-cols-12 gap-2 mt-5 justify-center py-2 items-center">
                    <div className="col-span-12 md:col-span-7">
                      <Textarea
                        ref={description}
                        className="col-span-12 md:col-span-6 mb-6 md:mb-0"
                        label="Product Descriptions"
                      />
                    </div>
                  </div>
                </ModalBody>
                <ModalFooter>
                  <Button
                    color="danger"
                    isDisabled={loading}
                    variant="light"
                    onPress={onClose}
                  >
                    Close
                  </Button>
                  <Button
                    color="primary"
                    isLoading={loading}
                    variant="flat"
                    onPress={create}
                  >
                    Submit
                  </Button>
                </ModalFooter>
              </>
            )}
          </ModalContent>
        </Modal>
      </div>

      <ProductsTable
        ProductsEditModal={setProductsEditVisible}
        setEdittingProduct={setEdittingProduct}
      />
      <div>
        <ProductsEditModal
          ProductsEditVisible={ProductsEditVisible}
          editingProduct={editingProduct}
          setEditingProduct={setEdittingProduct}
          setProductsEditVisible={setProductsEditVisible}
        />
      </div>
    </>
  );
}
