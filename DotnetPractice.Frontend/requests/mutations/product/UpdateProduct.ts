/* eslint-disable prettier/prettier */
import axios from "axios";

import { siteConfig } from "@/config/site";

export const UpdateProduct = async ({
  name,
  description,
  price,
  availableCount,
  photo,
  id
}: {
  name: string;
  description: string;
  price: number;
  availableCount: number;
  photo: string | null;
  id: string
}) => {
  const form = new FormData();
  form.append("name", name);
  if (description.trim().length != 0) form.append("description", description);
  form.append("price", price.toString());
  form.append("availableCount", availableCount.toString());
  if (photo) form.append("photo", photo);
  return await axios({
    url: siteConfig.requestUrls.baseUrl + siteConfig.requestUrls.product(id),
    data: form,
    method: "put",
    headers: {
      Authorization: `Bearer ${localStorage.getItem("tk")}`,
    },
  });
};
