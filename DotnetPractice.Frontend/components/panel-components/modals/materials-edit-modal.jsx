import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  ModalContent,
  Divider,
  Input,
  Textarea,
} from "@heroui/react";
import { useRef, useState } from "react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

import { UpdateProduct } from "@/requests/mutations/product/UpdateProduct";

export default function ProductsEditModal({
  ProductsEditVisible,
  setProductsEditVisible,
  editingProduct,
}) {
  const queryClient = useQueryClient();
  const [photo, setPhoto] = useState(null);
  const [loading, setLoading] = useState(false);
  const name = useRef();
  const price = useRef();
  const availableCount = useRef();
  const description = useRef();
  const onClose = () => {
    setProductsEditVisible(false);
  };
  const mutation = useMutation({
    mutationFn: UpdateProduct,
    onSuccess: (_response) => {
      setLoading(false);
      toast("Edited Successfully", {
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
  const update = () => {
    setLoading(true);
    mutation.mutate({
      name: name.current.value,
      description: description.current.value,
      price: price.current.value,
      availableCount: availableCount.current.value,
      photo,
      id: editingProduct?.id,
    });
  };
  return (
    <>
      <Modal
        hideCloseButton
        isOpen={ProductsEditVisible}
        scrollBehavior="outside"
        size="4xl"
        onOpenChange={(value) => setProductsEditVisible(value)}
      >
        <ModalContent>
                    {(onClose) => (
                      <>
                        <ModalHeader className="flex flex-col gap-1">
                          Edit Product
                        </ModalHeader>
                        <Divider />
                        <ModalBody>
                          <div className="w-full grid grid-cols-12 gap-2 mt-5 justify-center py-2 items-center">
                            <div className="col-span-12 md:col-span-4 mb-1">
                              <Input
                                ref={name}
                                isRequired
                                required
                                defaultValue={editingProduct?.name}
                                label="Product Name"
                                type="text"
                              />
                            </div>
                            <div className="col-span-12 md:col-span-4 mb-1">
                              <Input
                                ref={price}
                                isRequired
                                required
                                defaultValue={editingProduct?.price}
                                label="Price (USD)"
                                type="number"
                              />
                            </div>
                            <div className="col-span-12 md:col-span-4 mb-1">
                              <Input
                                ref={availableCount}
                                isRequired
                                required
                                defaultValue={editingProduct?.availableCount}
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
                                defaultValue={editingProduct?.description}
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
                            onPress={update}
                          >
                            Submit
                          </Button>
                        </ModalFooter>
                      </>
                    )}
                  </ModalContent>
      </Modal>
    </>
  );
}
